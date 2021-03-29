using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitUtil {

    public enum SourceControlProgram {  Git, GitHub };

    public class ExternalProcess {

        Process extproc = new Process();

        public string Call(string arguments) {
            extproc.StartInfo.Arguments = arguments;
            extproc.Start();
            extproc.WaitForExit();
            string result = extproc.StandardOutput.ReadToEnd();
            return result;
        }

        private void SetProgramName(SourceControlProgram scp) {
            if(scp == SourceControlProgram.GitHub) {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    extproc.StartInfo.FileName = "gh.exe";
                } else {
                    extproc.StartInfo.FileName = "/usr/bin/gh";
                }
            }
            if(scp == SourceControlProgram.Git) {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    extproc.StartInfo.FileName = "Git.exe";
                } else {
                    extproc.StartInfo.FileName = "/usr/bin/git";
                }
            }
        }

        public ExternalProcess(SourceControlProgram scp) {
            extproc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            extproc.StartInfo.UseShellExecute = false;
            extproc.StartInfo.RedirectStandardOutput = true;
            extproc.StartInfo.CreateNoWindow = true;
        }
    }
}
