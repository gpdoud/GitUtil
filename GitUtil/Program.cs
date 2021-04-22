using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dsi.GitUtil {
    class Program {
        static async Task Main(string[] args) {
            var options = Dsi.Utility.ProcessArgs.Parse(args);

            var repos = Directory.GetDirectories(@"c:/repos/SqlServer2CSharp");
            foreach (var dir in repos) {
                var gh = new GitHub(dir.Filepath);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"- dir: {dir.Filepath} ------------------------");
                Console.WriteLine($"[DEBUG] {dir} is clean: {git.IsClean()}, has remote: {git.HasRemote()}");
                if (!git.HasRemote()) {
                    dir.GitHubUrl = await gh.CreateRepo(dir.Name);
                    Console.WriteLine($"Branch: {git.CurrentBranch()} created.");
                    git.AddRemote(dir.GitHubUrl);
                }
                if (!git.IsClean()) {
                    git.Stage();
                    git.Commit();
                    Console.WriteLine($"{dir} is clean: {git.IsClean()}");
                }
                git.Push();
                Console.WriteLine($"[DEBUG] Delete {dir.Filepath}");
                Directory.RemoveDirectory(dir.Filepath);
                Console.WriteLine("===========================");
              
            }
        }
    }
}
