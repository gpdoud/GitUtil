using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Dsi.GitUtil {

    public class Git {

        Process proc = new Process();
        public string Path { get; set; }
        public ProcessResponse ProcessResponse = new ProcessResponse();

        private void Execute(string gitCommand, string args) {
            proc.StartInfo.Arguments = $"-C {Path} {gitCommand} {args}";
            proc.Start();
            string stdout = proc.StandardOutput.ReadToEnd();
            string stderr = proc.StandardError.ReadToEnd();
            proc.WaitForExit();
            SetProcessResponse(gitCommand, proc.ExitCode, stdout, stderr);
        }

        private void SetProcessResponse(string gitCommand, int exitCode, string stdout, string stderr) {
            ProcessResponse.GitCommand = gitCommand;
            ProcessResponse.ExitCode = exitCode;
            ProcessResponse.SetStandardOutput(stdout);
            ProcessResponse.SetStandardError(stderr);
        }

        public bool AddRemote(string GitHubRepo) {
            Execute("remote", $"add origin {GitHubRepo}");
            return ProcessResponse.ExitCode == 0;
        }

        public bool Pull() {
            var branch = CurrentBranch();
            Execute("pull", $"origin {branch}");
            return ProcessResponse.ExitCode == 0;
        }

        public bool Push() {
            var branch = CurrentBranch();
            Execute("push", $"origin {branch}");
            return ProcessResponse.ExitCode == 0;
        }

        public string CurrentBranch() {
            Execute("branch", "--show-current");
            Debug.Assert(!ProcessResponse.StandardOutput.Equals(string.Empty), "StandardOutput has no data!");
            return ProcessResponse.StandardOutput[0];
        }

        public bool Commit() {
            Execute("commit", "-q -m \"Committed by GitUtil\"");
            return ProcessResponse.ExitCode == 0;
        }

        public bool Stage() {
            Execute("add", ".");
            return ProcessResponse.ExitCode == 0;
        }

        public bool HasRemote() {
            Execute("remote", "-v");
            return ProcessResponse.ExitCode == 0
                && ProcessResponse.StandardOutput.Count > 0;
        }

        public bool IsClean() {
            Execute("status", "-s");
            return ProcessResponse.ExitCode == 0 && ProcessResponse.StandardOutput.Count == 0;
        }

        private void SetGitProgramName() {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                proc.StartInfo.FileName = "Git.exe";
            } else {
                proc.StartInfo.FileName = "/usr/bin/git";
            }
        }

        void Proc_Exited(object? sender, EventArgs args) {
            var proc = sender as Process;
            if(proc == null) return;
            if(proc.ProcessName.Contains("Exception")) {
                throw new Exception(proc.ProcessName);
            }
        }

        public Git(string path) {
            this.Path = path;
            SetGitProgramName();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            //proc.EnableRaisingEvents = true;
            //proc.Exited += Proc_Exited;
        }
    }
}
