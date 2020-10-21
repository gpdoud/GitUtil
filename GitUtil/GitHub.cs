using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitUtil {
    class GitHub {

        private static Process github = new Process();

        public static void CreateRepo(string name) {
            github.StartInfo.Arguments = $"repo create {name} --public --confirm";
            github.Start();
            github.WaitForExit();
            string result = github.StandardOutput.ReadToEnd();
        }

        public GitHub() {
            github.StartInfo.FileName = "Gh.exe";
            github.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            github.StartInfo.UseShellExecute = false;
            github.StartInfo.RedirectStandardOutput = true;
            github.StartInfo.CreateNoWindow = true;
        }
    }
}
