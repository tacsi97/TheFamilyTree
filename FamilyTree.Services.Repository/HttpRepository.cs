using FamilyTree.Business;
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
    public abstract class HttpRepository<T> : AsyncRepositoryBase<T>
    {
        private HttpClient _httpClient;

        private string _uri;

        private Token _token;

        //TODO: make this virtual instead of abstract
        //TODO: rewrite docs
        public abstract string RequestUriBase { get; }

        public abstract string PostUri { get; }

        public abstract string GetUri { get; }

        public abstract string PutUri { get; }

        public abstract string DeleteUri { get; }

        public abstract string GetAllUri { get; }

        public HttpRepository(string uri, Token token, HttpClient httpClient):
            base(uri, token)
        {
            _uri = uri;
            _token = token;
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
        public override async Task CreateAsync(T content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content), ExceptionMessages.ValueIsNull);

            if (string.IsNullOrEmpty(_uri))
                throw new ArgumentNullException(nameof(_uri), ExceptionMessages.ValueIsNull);

            var data = new StringContent(
                JsonConvert.SerializeObject(content), 
                Encoding.UTF8, 
                MediaTypeNames.Application.Json);

            var response = await _httpClient.PostAsync(_uri, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
        }

        /// <summary>
        /// Sends an HTTP Delete request to the specified <paramref name="_uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="_uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="_uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task DeleteAsync(int id)
        {
            if (string.IsNullOrEmpty(_uri)) throw new ArgumentNullException(nameof(_uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            var response = await _httpClient.DeleteAsync(_uri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="_uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="_uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="_uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public override async Task<T> GetAsync(int id)
        {
            if (string.IsNullOrEmpty(_uri)) throw new ArgumentNullException(nameof(_uri), ExceptionMessages.ValueIsNull);

            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessages.MustBeEqualOrGreaterThanZero);

            var response = await _httpClient.GetAsync(_uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="_uri"/>
        /// </summary>
        /// <param name="_uri">The full uri</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="_uri"/> is null or empty</exception>
        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            if (string.IsNullOrEmpty(_uri)) throw new ArgumentNullException(nameof(_uri), ExceptionMessages.ValueIsNull);

            var response = await _httpClient.GetAsync(_uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        /// <summary>
        /// Sends an HTTP Post request to the specified <paramref name="_uri"/> with the serialized <paramref name="content"/>
        /// </summary>
        /// <param name="_uri">The full uri</param>
        /// <param name="content">Must be serialized to JSON</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or empty</exception>
        public override async Task ModifyAsync(T content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(_uri)) throw new ArgumentNullException(nameof(_uri));

            var data = new StringContent(
                JsonConvert.SerializeObject(content), 
                Encoding.UTF8, 
                MediaTypeNames.Application.Json);

            var response = await _httpClient.PutAsync(_uri, data);

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
