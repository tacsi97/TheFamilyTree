using FamilyTree.Business;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    public abstract class HttpRepositoryBase<T> : IAsyncRepository<T>
    {
        private HttpClient _httpClient;
        public Token Token { get; set; }
        public string Uri { get; set; }

        //TODO: make this virtual instead of abstract
        //TODO: rewrite docs
        public abstract string RequestUriBase { get; }

        public abstract string PostUri { get; }

        public abstract string GetUri { get; }

        public abstract string PutUri { get; }

        public abstract string DeleteUri { get; }

        public abstract string GetAllUri { get; }

        public HttpRepositoryBase(string uri, Token token, HttpClient httpClient)
        {
            Uri = uri;
            Token = token;
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
        public async Task<T> CreateAsync(T content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content), ExceptionMessages.ValueIsNull);

            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentNullException(nameof(Uri), ExceptionMessages.ValueIsNull);

            var data = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            // TODO: tranzakció vagy https://stackoverflow.com/questions/39190018/how-to-get-object-using-httpclient-with-response-ok-in-web-api
            var response = await _httpClient.PostAsync(Uri, data);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);

            var resultJSON = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(resultJSON);

            return result;
        }

        /// <summary>
        /// Sends an HTTP Delete request to the specified <paramref name="Uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="Uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="Uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentNullException(nameof(Uri), ExceptionMessages.ValueIsNull);

            var response = await _httpClient.DeleteAsync(Uri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(ExceptionMessages.HttpResponseException);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="Uri"/> with the <paramref name="id"/> of the object
        /// </summary>
        /// <param name="Uri">The full uri</param>
        /// <param name="id">The id of the object</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="Uri"/> parameter is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="id"/> is lesser than 0</exception>
        public async Task<T> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentNullException(nameof(Uri), ExceptionMessages.ValueIsNull);

            // hol van használva az ID???
            var response = await _httpClient.GetAsync(Uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Sends an HTTP Get request to the specified <paramref name="Uri"/>
        /// </summary>
        /// <param name="Uri">The full uri</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="Uri"/> is null or empty</exception>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentNullException(nameof(Uri), ExceptionMessages.ValueIsNull);

            var response = await _httpClient.GetAsync(Uri);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        /// <summary>
        /// Sends an HTTP Post request to the specified <paramref name="Uri"/> with the serialized <paramref name="content"/>
        /// </summary>
        /// <param name="Uri">The full uri</param>
        /// <param name="content">Must be serialized to JSON</param>
        /// <returns>The response of the request</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or empty</exception>
        public async Task ModifyAsync(T content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentNullException(nameof(Uri));

            var data = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            var response = await _httpClient.PutAsync(Uri, data);

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
