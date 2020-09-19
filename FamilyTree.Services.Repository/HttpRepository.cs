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
    public abstract class HttpRepository<TObject> : AsyncRepositoryBase<HttpResponseMessage, TObject>
    {
        private HttpClient _httpClient;

        //TODO: make this virtual instead of abstract
        public abstract string RequestUriBase { get; }

        public abstract string PostUri { get; }

        public abstract string GetUri { get; }

        public abstract string PutUri { get; }

        public abstract string DeleteUri { get; }

        public abstract string GetAllUri { get; }

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        /// <summary>
        /// Creates a task, that sends a POST request with the serialized <paramref name="templateObject"/> as a JSON object to the corresponding address.
        /// </summary>
        /// <param name="templateObject">The object, that will be send</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="templateObject"/> is null</exception>
        /// <returns>The task, that is created</returns>
        public override Task<HttpResponseMessage> Create(TObject templateObject)
        {
            if (templateObject == null) throw new ArgumentNullException(nameof(templateObject));

            var json = JsonConvert.SerializeObject(templateObject);

            var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            return _httpClient.PostAsync(Path.Combine(RequestUriBase, PostUri), data);
        }

        /// <summary>
        /// Creates a task, that sends a DELETE request with the <paramref name="id"/> of the object to the corresponding address.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <returns>The task, that is created</returns>
        public override Task<HttpResponseMessage> Delete(int id)
        {
            return _httpClient.DeleteAsync(Path.Combine(RequestUriBase, string.Format(DeleteUri, id)));
        }

        /// <summary>
        /// Creates a task, that sends a GET request with the <paramref name="id"/> of the object to the corresponding address.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <returns>The task, that is created</returns>
        public override Task<HttpResponseMessage> Get(int id)
        {
            return _httpClient.GetAsync(Path.Combine(RequestUriBase, string.Format(GetUri, id)));
        }

        /// <summary>
        /// Creates a task, that sends a GET request to get all the objects from the corresponding address.
        /// </summary>
        /// <returns>The task, that is created</returns>
        public override Task<HttpResponseMessage> GetAll()
        {
            return _httpClient.GetAsync(Path.Combine(RequestUriBase, GetAllUri));
        }

        /// <summary>
        /// Creates a task, that sends a PUT request with the <paramref name="templateObject"/> of the object to the corresponding address.
        /// </summary>
        /// <param name="templateObject">The object, that will be send</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="templateObject"/> is null</exception>
        /// <returns>The task, that is created</returns>
        public override Task<HttpResponseMessage> Modify(TObject templateObject)
        {
            if (templateObject == null) throw new ArgumentNullException(nameof(templateObject));

            var json = JsonConvert.SerializeObject(templateObject);

            var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            return _httpClient.PutAsync(Path.Combine(RequestUriBase, PutUri), data);
        }
    }
}
