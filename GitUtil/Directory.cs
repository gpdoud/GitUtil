using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitUtil {
    
    public static class Directory {

        private static List<string> directories = new List<string>();

        public static void RecurseDirectories(string baseDirectory) {
            GetDirectories(baseDirectory);
        }
        private static void GetDirectories(string directory) {
            ChangeDirectory(directory);
            var dirs = System.IO.Directory.EnumerateDirectories(CurrentDirectory());
            directories.AddRange(dirs);
            foreach(var dir in dirs) {
                GetDirectories(dir);
            }
        }
        public static string CurrentDirectory() {
            return System.IO.Directory.GetCurrentDirectory();
        }
        public static void ChangeDirectory(string newDirectory) {
            System.IO.Directory.SetCurrentDirectory(newDirectory);
        }
    }
}
