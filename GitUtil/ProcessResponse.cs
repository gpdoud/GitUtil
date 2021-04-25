using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsi.GitUtil {
    
    public record ProcessResponse {
        public string GitCommand { get; set; } = string.Empty;
        public int ExitCode { get; set; } = -1;
        public List<string> _StandardOutput = new List<string>();
        public List<string> StandardOutput { 
            get => _StandardOutput;
            private set { this._StandardOutput = value; } 
        }
        public List<string> _StandardError = new List<string>();
        public List<string> StandardError { 
            get => _StandardError; 
            private set { this._StandardError = value;  } 
        }

        public void SetStandardOutput(string StandardOutput) { 
            this.StandardOutput = new List<string>();
            if(StandardOutput.Equals(string.Empty))
                return;
            this.StandardOutput = new List<string>(StandardOutput.Split('\n')); 
        }
        public void SetStandardError(string StandardError) { 
            this.StandardError = new List<string>();
            if(StandardError.Equals(string.Empty))
                return;
            this.StandardError = new List<string>(StandardError.Split('\n'));
        }

    }
}
