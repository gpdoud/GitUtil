using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dsi.GitUtil {
    // gitutil --path [folder] --extension [string] --delete-directory
    class Program {
        static async Task Main(string[] args) {
            var options = Dsi.Utility.ProcessArgs.Parse(args);

            if(options.Count == 0) {
                Console.WriteLine("GitUtil --path [directory] --extension [ext] --delete-directory");
                return;
            }

            if(!options.ContainsKey("--path"))
                throw new Exception("--path is required");
            var path = options["--path"];
            Console.WriteLine($"--path is {path}");
            if(options.ContainsKey("--delete-directory")) {
                Console.WriteLine("ALERT!: Repository will be deleted!");
            }
            // allow setting custom string to append to github repository
            var ext = (options.ContainsKey("--extension") ? options["--extension"] : null);
            var repos = Directory.GetDirectories(path);

            foreach(var dir in repos) {
                var gh = new GitHub(dir.Filepath);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"Directory: {dir.Filepath} ...");
                if(!git.HasRemote()) {
                    dir.GitHubUrl = await gh.CreateRepo(dir.Name, ext);
                    git.AddRemote(dir.GitHubUrl);
                }
                if(!git.IsClean()) {
                    git.Stage();
                    git.Commit();
                }
                git.Pull();
                git.Push();
                if(options.ContainsKey("--delete-directory")) {
                    Directory.RemoveDirectory(dir.Filepath);
                }
            }
            Console.WriteLine("Done ...");
            //Console.Read();
        }
    }
}
