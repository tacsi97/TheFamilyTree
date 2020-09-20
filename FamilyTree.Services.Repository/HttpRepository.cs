using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    public abstract class HttpRepository : AsyncRepositoryBase<HttpResponseMessage>
    {
        private HttpClient _httpClient;

        //TODO: make this virtual instead of abstract
        public abstract string RequestUriBase { get; }

        public abstract string PostUri { get; }

        public abstract string GetUri { get; }

        public abstract string PutUri { get; }

        public abstract string DeleteUri { get; }

        public abstract string GetAllUri { get; }

        public HttpRepository()
        {

        }

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        /// <summary>
        /// Sends an HTTP Post request to the specified <paramref name="uri"/> with the serialized <paramref name="content"/>
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="content">Must be serialized to JSON</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null</exception>
        public override async Task<HttpResponseMessage> CreateAsync(string uri, string content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content), ExceptionMessages.ValueIsNull);

            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            var data = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            return await _httpClient.PostAsync(uri, data);
        }

        /// <summary>
        /// Sends an HTTP Delete request to the specified <paramref name="uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> parameter is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task<HttpResponseMessage> DeleteAsync(string uri, int id)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            return await _httpClient.DeleteAsync(uri);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> parameter is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task<HttpResponseMessage> GetAsync(string uri, int id)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            return await _httpClient.GetAsync(uri);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="uri"/>
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> is null or empty</exception>
        public override async Task<HttpResponseMessage> GetAllAsync(string uri)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            return await _httpClient.GetAsync(uri);
        }

        /// <summary>
        /// Sends an HTTP Post request to the specified <paramref name="uri"/> with the serialized <paramref name="content"/>
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="content">Must be serialized to JSON</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null</exception>
        public override async Task<HttpResponseMessage> ModifyAsync(string uri, string content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri));

            var data = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            return await _httpClient.PutAsync(uri, data);
        }

        /// <summary>
        /// Return the HttpClient.
        /// </summary>
        /// <returns></returns>
        public HttpClient GetHttpClient() => _httpClient;

        /// <summary>
        /// Sets the HttpClient to the given <paramref name="httpClient"/>
        /// </summary>
        /// <param name="httpClient">The <paramref name="httpClient"/></param>
        public void SetHttpClient(HttpClient httpClient) => _httpClient = httpClient;
    }
}
