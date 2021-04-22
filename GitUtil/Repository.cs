using System;
namespace Dsi.GitUtil {

    public class Repository {

        public string Name { get; set; } = string.Empty;
        public string Filepath { get; set; } = string.Empty;
        public bool Private { get; set; }
        public string? GitHubUrl { get; set; }
        public string? Html_url { get; set; }

        public Repository() {
        }
    }
}
