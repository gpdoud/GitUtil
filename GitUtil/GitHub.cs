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

namespace Dsi.GitUtil {
    class GitHub {

        public string Path { get; set; }
        public string GitHubSecurityToken { get; init; }
        public HttpClient HttpClient { get; init; }
        JsonSerializerOptions options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };


        public async Task<string> CreateRepo(string name, string? extension = null) {
            name = $"{name}-{(extension != null ? extension : DateTime.Now.Ticks)}";
            var repo = new { name };
            var json = JsonSerializer.Serialize(repo, options);
            var res = await Post($"/user/repos", json);
            var json2 = await res.Content.ReadAsStringAsync();
            var repo2 = JsonSerializer.Deserialize<Repository>(json2, options);
            if(repo2 == null)
                throw new Exception("Cannot deserialize CreateRepo() result!");
            return $"{repo2.Html_url}.git";
        }

        public async Task GetRepo(string name) {
            var res = await Get($"/repos/gpdoud/{name}");
            if (res == null)
                throw new Exception("Cannot deserialize GetRepo() result!");
            var repo = JsonSerializer.Deserialize<Repository>(res, options);
        }

        private async Task<string?> Get(string url) {
            return await HttpClient.GetStringAsync($"{url}");
        }
        private async Task<HttpResponseMessage> Post(string url, string body) {
            var content = new StringContent(body);
            return await HttpClient.PostAsync(url, content);
        }

        public GitHub(string path) {
            Path = path;
            GitHubSecurityToken = Dsi.GitUtil.Security.Tokens.GitHub;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(@"https://api.github.com");
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitUtil", "1.0"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", GitHubSecurityToken);
        }
    }
}
