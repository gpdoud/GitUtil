using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GitUtil {

    public class Git {

        Process git = new Process();

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

        public Git() {
            git.StartInfo.FileName = "Git.exe";
            git.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.RedirectStandardOutput = true;
            git.StartInfo.CreateNoWindow = true;
        }
    }
}
