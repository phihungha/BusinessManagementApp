using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagementApp.Services
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var req = request;
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} -   Request]";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{msg}========Start==========");
            sb.AppendLine($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
            sb.AppendLine($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

            foreach (var header in req.Headers)
                sb.AppendLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (req.Content != null)
            {
                foreach (var header in req.Content.Headers)
                    sb.AppendLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (req.Content is StringContent || this.IsTextBasedContentType(req.Headers) || this.IsTextBasedContentType(req.Content.Headers))
                {
                    var result = await req.Content.ReadAsStringAsync();

                    sb.AppendLine($"{msg} From: {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
                    sb.AppendLine($"{msg} Content:");
                    sb.AppendLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");

                }
            }

            var start = DateTime.Now;

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var end = DateTime.Now;

            sb.AppendLine($"{msg} Duration: {end - start}");
            sb.AppendLine($"{msg}==========End==========");

            msg = $"[{id} - Response]";
            sb.AppendLine($"{msg}=========Start=========");

            var resp = response;

            sb.AppendLine($"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

            foreach (var header in resp.Headers)
                sb.AppendLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                    sb.AppendLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (resp.Content is StringContent || this.IsTextBasedContentType(resp.Headers) || this.IsTextBasedContentType(resp.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await resp.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    sb.AppendLine($"{msg} From: {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
                    sb.AppendLine($"{msg} Content:");
                    sb.AppendLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
                    sb.AppendLine($"{msg} Duration: {end - start}");
                }
            }

            sb.AppendLine($"{msg}==========End==========");
            Console.WriteLine(sb);
            return response;
        }

        readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string> values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }

    }
}
