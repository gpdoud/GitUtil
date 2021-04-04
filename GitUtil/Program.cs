using System;
using System.Diagnostics;

namespace GitUtil {
    class Program {
        static void Main(string[] args) {

            var gh = new GitHub();
            var repos = Directory.GetDirectories(@"/Users/gpdoud/Repos");
            repos.ForEach(dir => {
                var git = new Git(dir.Filepath);
                Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}, has remote: {git.RepoHasRemote()}");
                if(!git.RepoIsClean()) {
                    git.StageAllFiles();
                    git.RepoCommit();
                    Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}");
                }
            });

        }
    }
}
