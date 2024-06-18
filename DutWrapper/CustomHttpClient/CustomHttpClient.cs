using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static partial class CustomHttpClient
    {
        private static HttpClient CreateDefaultHttpClient()
        {
            var client = new HttpClient();
            return client;
        }

        public static async Task<Response> Get(Uri uri, List<Header>? headers = null)
        {
            try
            {
                var client = CreateDefaultHttpClient();
                foreach (Header header in headers ?? new List<Header>())
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = await client.GetAsync(uri);
                List<Header> headerResponse = response.Headers.Select(c => new Header(c.Key, response.Headers.GetValues(c.Key) == null ? "" : String.Join(";", response.Headers.GetValues(c.Key).ToArray()))).ToList();
                return new Response(
                    uri: uri,
                    requestType: RequestType.Get,
                    headers: headerResponse,
                    code: (int)response.StatusCode,
                    content: await response.Content.ReadAsStringAsync(),
                    ex: null
                    );
            }
            catch (Exception ex)
            {
                return new Response(
                    uri: uri,
                    requestType: RequestType.Get,
                    headers: null,
                    code: null,
                    content: null,
                    ex: ex
                    );
            }
        }

        public static async Task<Response> Post(Uri uri, FormUrlEncodedContent body, List<Header>? headers = null)
        {
            try
            {
                var client = CreateDefaultHttpClient();
                foreach (Header header in headers ?? new List<Header>())
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = await client.PostAsync(uri, body);
                List<Header> headerResponse = response.Headers.Select(c => new Header(c.Key, response.Headers.GetValues(c.Key) == null ? "" : String.Join(";", response.Headers.GetValues(c.Key).ToArray()))).ToList();
                return new Response(
                    uri: uri,
                    requestType: RequestType.Post,
                    headers: headerResponse,
                    code: (int)response.StatusCode,
                    content: await response.Content.ReadAsStringAsync(),
                    ex: null
                    );
            }
            catch (Exception ex)
            {
                return new Response(
                    uri: uri,
                    requestType: RequestType.Post,
                    headers: null,
                    code: null,
                    content: null,
                    ex: ex
                    );
            }
        }
    }
}
