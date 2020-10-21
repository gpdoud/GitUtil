using System;
using System.Diagnostics;

namespace GitUtil {
    class Program {
        static void Main(string[] args) {

            var git = new Git();
            Directory.RecurseDirectories(@"c:\repos\bootcamp");
            Directory.GetRepositoryName(@"c:\repos\bootcamp");
            Directory.directories.ForEach(dir => {
                Directory.ChangeDirectory(dir);
                Console.WriteLine($"{dir} is clean: {git.RepoIsClean()}, has remote: {git.RepoHasRemote()}");
            });

        }
    }
}
