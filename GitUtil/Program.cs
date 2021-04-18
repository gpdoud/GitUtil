using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GitUtil {
    class Program {
        async static Task Main(string[] args) {
            var repos = Directory.GetDirectories("/Users/gpdoud/Repos/testgitutil");
            foreach(var dir in repos) {
                var gh = new GitHub(dir.Filepath);
                await gh.GetRepo(dir.Name);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"- dir: {dir.Filepath} ------------------------");
                Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}, has remote: {git.RepoHasRemote()}");
                if(!git.RepoHasRemote()) {
                }
                if(!git.RepoIsClean()) {
                    git.StageAllFiles();
                    git.RepoCommit();
                    Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}");
                }
                Console.WriteLine("---------------------------");
            }
        }
    }
}
