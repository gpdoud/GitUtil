﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.IO.Path;
using static System.IO.Directory;

namespace Dsi.GitUtil {
    
    public static class Directory {

        public static void RemoveDirectory(string directory) {
            foreach(var dirname in System.IO.Directory.EnumerateDirectories(directory)) {
                RemoveDirectory(dirname);
            }
            foreach(var filename in System.IO.Directory.EnumerateFiles(directory)) {
                var file = new System.IO.FileInfo(filename);
                file.Attributes = FileAttributes.Normal;
                file.Delete();
            }
            var dir = new System.IO.DirectoryInfo(directory);
            dir.Attributes = FileAttributes.Normal;
            dir.Delete();
        }

        public static List<Repository> GetDirectories(string baseDirectory) {
            List<Repository> repositories = new();
            RecurseDirectories(baseDirectory, repositories);
            return repositories;
        }
        private static void RecurseDirectories(string directory, IList<Repository> repositories) {
            var dirs = EnumerateDirectories(directory).ToList();
            dirs.ForEach(dir => {
                if(IsGitRepository(dir)) {
                    var repo = new Repository {
                        Filepath = dir.Substring(0, dir.Length - 4),
                        Name = GetRepositoryName(dir)
                    };
                    
                    repositories.Add(repo);
                }
            });
            foreach(var dir in dirs) {
                RecurseDirectories(dir, repositories);
            }
        }
        public static string GetRepositoryName(string directory) {
            var dir = GetDirectoryName(directory);
            return GetFileName(dir) ?? string.Empty;
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
