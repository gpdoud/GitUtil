using System;
using System.Diagnostics;

namespace GitUtil {
    class Program {
        static void Main(string[] args) {

            var git = new Git();
            var gh = new GitHub();
            Directory.RecurseDirectories(@"/Users/gpdoud/Repos/test-gitutil");
            Directory.directories.ForEach(dir => {
                Directory.ChangeDirectory(dir);
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
