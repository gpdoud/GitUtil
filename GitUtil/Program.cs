using System;
using System.Diagnostics;

namespace GitUtil {
    class Program {
        static void Main(string[] args) {
            var repos = Directory.GetDirectories("/Users/gpdoud/Repos/testgitutil");
            repos.ForEach(dir => {
                var gh = new GitHub(dir.Filepath);
                var git = new Git(dir.Filepath);
                Console.WriteLine($"- dir: {dir.Filepath} ------------------------");
                Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}, has remote: {git.RepoHasRemote()}");
                if(!git.RepoHasRemote()) {
                    gh.RepoCreate(dir.Name);
                }
                if(!git.RepoIsClean()) {
                    git.StageAllFiles();
                    git.RepoCommit();
                    Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}");
                }
                Console.WriteLine("---------------------------");
            });

        }
    }
}
