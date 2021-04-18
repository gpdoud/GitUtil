using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitUtil {
    class GitHub {

        public string Path { get; set; }
        public string GitHubSecurityToken { get; init; }
        public HttpClient HttpClient { get; init; }

        public async Task GetRepo(string name) {
            var res = await GetAsync($"/repos/gpdoud/GitUtil");
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            var repo = JsonSerializer.Deserialize<Repository>(res, options);
        }

        private async Task<string?> GetAsync(string url) {
            return await HttpClient.GetStringAsync($"{url}");
        }

        public GitHub(string path) {
            Path = path;
            GitHubSecurityToken = Dsi.GitUtil.Security.Tokens.GitHub;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(@"https://api.github.com");
            //HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitUtil", "1.0"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", GitHubSecurityToken);
        }
    }
}
