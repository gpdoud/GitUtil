using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GitUtil {
    class Program {
        static async Task Main(string[] args) {
            var repos = Directory.GetDirectories("/Users/gpdoud/Repos/testgitutil");
            foreach (var dir in repos) {
                var gh = new GitHub(dir.Filepath);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"- dir: {dir.Filepath} ------------------------");
                Console.WriteLine($"{dir} is clean: {git.IsClean()}, has remote: {git.HasRemote()}");
                if (!git.HasRemote()) {
                    await gh.CreateRepo(dir.Name);
                    Console.WriteLine($"Branch: {git.CurrentBranch()} created.");
                }
                if (!git.IsClean()) {
                    git.Stage();
                    git.Commit();
                    Console.WriteLine($"{dir} is clean: {git.IsClean()}");
                    git.Push();
                }
                Console.WriteLine("---------------------------");
            }
        }
    }
}
