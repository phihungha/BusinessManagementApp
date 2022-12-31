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
    /// <summary>
    /// Logs HTTP requests and responses.
    /// </summary>
    public class HttpLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();
            var sb = new StringBuilder();

            sb.Append(await LogRequest(request, id));

            var startTime = DateTime.Now;
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var endTime = DateTime.Now;
            sb.Append($"[{id}] Duration: {endTime - startTime}");

            sb.Append(await LogResponse(response, request, id));

            Console.WriteLine(sb.ToString());

            return response;
        }

        private async Task<string> LogRequest(HttpRequestMessage request, string id)
        {
            var sb = new StringBuilder();
            var label = $"[{id} -   Request]";

            sb.AppendLine($"{label}========Start==========");
            sb.AppendLine($"{label} {request.Method} " +
                          $"{request.RequestUri?.PathAndQuery} " +
                          $"{request.RequestUri?.Scheme}/" +
                          $"{request.Version}");
            sb.AppendLine($"{label} Host: {request.RequestUri?.Scheme}://{request.RequestUri?.Host}");

            foreach (var header in request.Headers)
            {
                sb.AppendLine($"{label} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                {
                    sb.AppendLine($"{label} {header.Key}: {string.Join(", ", header.Value)}");
                }

                if (request.Content is StringContent ||
                    IsTextBasedContentType(request.Headers) ||
                    IsTextBasedContentType(request.Content.Headers))
                {
                    var result = await request.Content.ReadAsStringAsync();

                    sb.AppendLine($"{label} From: {request.Method} " +
                                  $"{request.RequestUri?.PathAndQuery} " +
                                  $"{request.RequestUri?.Scheme}/" +
                                  $"{request.Version}");
                    sb.AppendLine($"{label} Content:");
                    sb.AppendLine($"{label} {string.Join("", Enumerable.Cast<char>(result).Take(255))}...");
                }
            }

            sb.AppendLine($"{label}==========End==========");
            return sb.ToString();
        }

        private async Task<string> LogResponse(HttpResponseMessage response, HttpRequestMessage request, string id)
        {
            var sb = new StringBuilder();
            var label = $"[{id} - Response]";

            sb.AppendLine($"{label}=========Start=========");
            sb.AppendLine($"{label} {request.RequestUri?.Scheme.ToUpper()}/{response.Version} " +
                          $"{(int)response.StatusCode} {response.ReasonPhrase}");

            foreach (var header in response.Headers)
            {
                sb.AppendLine($"{label} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (response.Content != null)
            {
                foreach (var header in response.Content.Headers)
                    sb.AppendLine($"{label} {header.Key}: {string.Join(", ", header.Value)}");

                if (response.Content is StringContent ||
                    IsTextBasedContentType(response.Headers) ||
                    IsTextBasedContentType(response.Content.Headers))
                {
                    var startTime = DateTime.Now;
                    var result = await response.Content.ReadAsStringAsync();
                    var endTime = DateTime.Now;

                    sb.AppendLine($"{label} From: {request.Method} " +
                                  $"{request.RequestUri?.PathAndQuery} " +
                                  $"{request.RequestUri?.Scheme}/{request.Version}");
                    sb.AppendLine($"{label} Content:");
                    sb.AppendLine($"{label} {string.Join("", result.Cast<char>().Take(255))}...");
                    sb.AppendLine($"{label} Duration: {endTime - startTime}");
                }
            }

            sb.AppendLine($"{label}==========End==========");
            return sb.ToString();
        }

        private readonly string[] contentTypes = new[]
        {
            "html",
            "text",
            "xml",
            "json",
            "txt",
            "x-www-form-urlencoded"
        };

        private bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string>? values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return contentTypes.Any(t => header.Contains(t));
        }
    }
}