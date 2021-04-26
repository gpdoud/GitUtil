using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dsi.GitUtil {
    class Program {
        static async Task Main(string[] args) {
            var options = Dsi.Utility.ProcessArgs.Parse(args);

            if(!options.ContainsKey("--path"))
                throw new Exception("--path is required");
            var path = options["--path"];
            var repos = Directory.GetDirectories(path);

            foreach(var dir in repos) {
                var gh = new GitHub(dir.Filepath);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"Directory: {dir.Filepath} ...");
                if(!git.HasRemote()) {
                    dir.GitHubUrl = await gh.CreateRepo(dir.Name);
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
