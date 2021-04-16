using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GitUtil {
    class GitHub {

        Process proc = new Process();
        public string Path { get; set; }

        public bool RepoCreate(string name) {
            //Console.WriteLine($"repo create {name} --public -y");
            string result = Execute("repo", $" create {name} --public -y");
            Console.WriteLine($"Result: {result}");
            return result.Trim().Length > 0;
        }

        public bool RepoView() {
            var result = Execute("repo", "view");
            return result.Trim().Length != 0;
        }

        private string Execute(string githubCommand, string args) {
            proc.StartInfo.Arguments = $"{githubCommand} {args}";
            proc.Start();
            proc.WaitForExit();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine($"[DEBUG] github command: {githubCommand} result: [{result}]");
            return result;
        }

        private void SetGitHubProgramName() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                proc.StartInfo.FileName = "gh.exe";
            } else {
                proc.StartInfo.FileName = "/usr/local/bin/gh";
            }
        }

        public GitHub(string path) {
            this.Path = path;
            SetGitHubProgramName();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
        }
    }
}
