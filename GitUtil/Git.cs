using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GitUtil {

    public class Git {

        Process git = new Process();

        public bool RepoCommit() {
            git.StartInfo.Arguments = "commit -q -m \"Committed by GitUtil\"";
            git.Start();
            git.WaitForExit();
            string result = git.StandardOutput.ReadToEnd();
            Console.WriteLine($"[DEBUG] git commit response: {result}");
            return result.Trim().Length != 0;
        }

        public bool StageAllFiles() {
            git.StartInfo.Arguments = "add .";
            git.Start();
            git.WaitForExit();
            string result = git.StandardOutput.ReadToEnd();
            Console.WriteLine($"[DEBUG] git add response: {result}");
            return result.Trim().Length == 0;
        }

        public bool RepoHasRemote() {
            git.StartInfo.Arguments = "remote -v";
            git.Start();
            git.WaitForExit();
            string result = git.StandardOutput.ReadToEnd();
            return result.Trim().Length != 0;
        }

        public bool RepoIsClean() {
            git.StartInfo.Arguments = "status -s";
            git.Start();
            git.WaitForExit();
            string result = git.StandardOutput.ReadToEnd();
            return result.Trim().Length == 0;
        }

        private void SetGitProgramName() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                git.StartInfo.FileName = "Git.exe";
            } else { 
                git.StartInfo.FileName = "/usr/bin/git";
            }
        }

        public Git() {
            SetGitProgramName();
            git.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.RedirectStandardOutput = true;
            git.StartInfo.CreateNoWindow = true;
        }
    }
}
