using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.IO.Path;
using static System.IO.Directory;

namespace GitUtil {
    
    public static class Directory {

        public static List<string> directories = new List<string>();

        public static void RecurseDirectories(string baseDirectory) {
            GetDirectories(baseDirectory);
        }
        private static void GetDirectories(string directory) {
            var dirs = EnumerateDirectories(directory).ToList();
            dirs.ForEach(dir => {
                if(IsGitRepository(dir)) {
                    GetRepositoryName(dir);
                    var path = dir.Substring(0, dir.Length - 4);
                    directories.Add(path);
                }
            });
            foreach(var dir in dirs) {
                GetDirectories(dir);
            }
        }
        public static string? GetRepositoryName(string directory) {
            var dir = GetDirectoryName(directory);
            return GetFileName(dir);
        }
        private static bool IsGitRepository(string directory) {
            return GetExtension(directory).Equals(".git");
        }
        public static string CurrentDirectory() {
            return GetCurrentDirectory();
        }
        public static void ChangeDirectory(string newDirectory) {
            SetCurrentDirectory(newDirectory);
        }
    }
}
