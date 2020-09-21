using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    public abstract class HttpRepository<TObject> : AsyncRepositoryBase<TObject>
    {
        private HttpClient _httpClient;

        //TODO: make this virtual instead of abstract
        //TODO: rewrite docs
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
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or empty</exception>
        public override async Task CreateAsync(string uri, string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content), ExceptionMessages.ValueIsNull);

            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            var data = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _httpClient.PostAsync(uri, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
        }

        /// <summary>
        /// Sends an HTTP Delete request to the specified <paramref name="uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task DeleteAsync(string uri, int id)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            var response = await _httpClient.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task<TObject> GetAsync(string uri, int id)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            var response = await _httpClient.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TObject>(json);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="uri"/>
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="uri"/> is null or empty</exception>
        public override async Task<IEnumerable<TObject>> GetAllAsync(string uri)
        {
            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri), ExceptionMessages.ValueIsNull);

            var response = await _httpClient.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<TObject>>(json);
        }

        /// <summary>
        /// Sends an HTTP Post request to the specified <paramref name="uri"/> with the serialized <paramref name="content"/>
        /// </summary>
        /// <param name="uri">The full uri</param>
        /// <param name="content">Must be serialized to JSON</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or empty</exception>
        public override async Task ModifyAsync(string uri, string content)
        {
            if (string.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(uri)) throw new ArgumentNullException(nameof(uri));

            var data = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _httpClient.PutAsync(uri, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
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
