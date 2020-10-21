using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitUtil {
    
    public static class Directory {

        public static List<string> directories = new List<string>();

        public static void RecurseDirectories(string baseDirectory) {
            GetDirectories(baseDirectory);
        }
        public static string GetRepositoryName(string directory) {
            var pos = directory.LastIndexOf('\\');
            var repo = directory.Substring(pos + 1);
            return repo;
        }
        private static void GetDirectories(string directory) {
            var dirs = System.IO.Directory.EnumerateDirectories(directory).ToList();
            dirs.ForEach(dir => {
                if(IsGitRepository(dir)) {
                    var path = dir.Substring(0, dir.Length - 4);
                    directories.Add(path);
                }
            });
            foreach(var dir in dirs) {
                GetDirectories(dir);
            }
        }
        private static bool IsGitRepository(string directory) {
            return System.IO.Path.GetExtension(directory).Equals(".git");
        }
        public static string CurrentDirectory() {
            return System.IO.Directory.GetCurrentDirectory();
        }
        public static void ChangeDirectory(string newDirectory) {
            System.IO.Directory.SetCurrentDirectory(newDirectory);
        }
    }
}
