using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GitUtil {
    
    public class Git {

        Process git = new Process();

        public void Status() {
            git.StartInfo.Arguments = "status -s";
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.RedirectStandardOutput = true;
            git.StartInfo.CreateNoWindow = true;
            git.Start();
            git.WaitForExit();
            string result = git.StandardOutput.ReadToEnd();
            Console.WriteLine($"{result}");
        }

        public Git() {
            git.StartInfo.FileName = "Git.exe";
            git.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        }
    }
}
