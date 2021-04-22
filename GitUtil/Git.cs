using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Dsi.GitUtil {

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

        public bool AddRemote(string GitHubRepo) {
            var result = Execute("remote", $"add origin {GitHubRepo}");
            return result.Trim().Length == 0;
        }

        public bool Push() {
            var branch = CurrentBranch();
            var result = Execute("push", $"origin {branch}");
            return result.Trim().Length != 0;
        }

        public string CurrentBranch() {
            var result = Execute("branch", "--show-current");
            return result.Trim();
        }

        public bool Commit() {
            var result = Execute("commit", "-q -m \"Committed by GitUtil\"");
            return result.Trim().Length != 0;
        }

        public bool Stage() {
            var result = Execute("add", ".");
            return result.Trim().Length == 0;
        }

        public bool HasRemote() {
            var result = Execute("remote", "-v");
            return result.Trim().Length != 0;
        }

        public bool IsClean() {
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
