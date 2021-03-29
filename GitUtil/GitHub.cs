using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GitUtil {
    class GitHub {

        private ExternalProcess extproc;

        public void ViewRepo(string name) {
            var result = extproc.Call($"repo view ");
        }

        public GitHub() {
            extproc = new ExternalProcess(SourceControlProgram.GitHub);
        }
    }
}
