using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GitUtil {

    public class Git {

        Process proc = new Process();
        public string Path { get; set; }

        private string Execute(string gitCommand, string args) {
            proc.StartInfo.Arguments = $"-C {Path} {gitCommand} {args}";
            proc.Start();
            proc.WaitForExit();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine($"[DEBUG] git command: {gitCommand} response: {result}");
            return result.Trim();
        }

        public bool RepoCommit() {
            var result = Execute("commit", "-q -m \"Committed by GitUtil\"");
            return result.Trim().Length != 0;
        }

        public bool StageAllFiles() {
            var result = Execute("add", ".");
            return result.Trim().Length == 0;
        }

        public bool RepoHasRemote() {
            var result = Execute("remote", "-v");
            return result.Trim().Length != 0;
        }

        public bool RepoIsClean() {
            var result = Execute("status", "-s");
            return result.Trim().Length == 0;
        }

        private void SetGitProgramName() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                proc.StartInfo.FileName = "Git.exe";
            } else { 
                proc.StartInfo.FileName = "/usr/bin/git";
            }
        }

        public Git(string path) {
            this.Path = path;
            SetGitProgramName();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
        }
    }
}
