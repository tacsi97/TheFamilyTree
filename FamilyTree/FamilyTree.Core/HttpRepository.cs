using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core
{
    public abstract class HttpRepository<TObject> : AsyncRepositoryBase<HttpResponseMessage, TObject>
    {
        private HttpClient _httpClient;

        public abstract string RequestUriBase { get; set; }

        public abstract string PostUri { get; set; }

        public abstract string GetUri { get; set; }

        public abstract string PutUri { get; set; }

        public abstract string DeleteUri { get; set; }

        public abstract string GetAllUri { get; set; }

        public HttpRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override Task<HttpResponseMessage> Create(TObject templateObject)
        {
            var json = JsonConvert.SerializeObject(templateObject);

            var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            return _httpClient.PostAsync(Path.Combine(RequestUriBase, PostUri), data);
        }

        public override Task<HttpResponseMessage> Delete(int id)
        {
            return _httpClient.DeleteAsync(Path.Combine(RequestUriBase, string.Format(DeleteUri, id)));
        }

        public override Task<HttpResponseMessage> Get(int id)
        {
            return _httpClient.GetAsync(Path.Combine(RequestUriBase, string.Format(GetUri, id)));
        }

        public override Task<HttpResponseMessage> GetAll()
        {
            return _httpClient.GetAsync(Path.Combine(RequestUriBase, GetAllUri));
        }

        public override Task<HttpResponseMessage> Modify(TObject templateObject)
        {
            var json = JsonConvert.SerializeObject(templateObject);

            var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            return _httpClient.PutAsync(Path.Combine(RequestUriBase, PutUri), data);
        }
    }
}
