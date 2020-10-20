using System;
using System.Diagnostics;

namespace GitUtil {
    class Program {
        static void Main(string[] args) {

            var git = new Git();
            Directory.RecurseDirectories(@"c:\repos\aoc\aoc-dfs1client");
            git.Status();

        }
    }
}
