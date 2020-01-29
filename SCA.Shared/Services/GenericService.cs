using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SCA.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Shared.Services
{
    public class GenericService<T> : IGenericService<T>
    {
        private readonly ILogger _logger;
        protected readonly HttpClient _clientHttp;
        public string _url { get; set; }
        public string _token { get; set; }

        public GenericService(ILogger<GenericService<T>> logger)
        {
            _clientHttp = new HttpClient();
            this._logger = logger;
        }

        public void SetUrl(string url)
        {
            
            this._url = url;
        }

        public void SetToken(string token)
        {   
            if (this._token != token)
            {
                this._token = token;
                this._clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._token);
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync(string recurso = "")
        {            
            string url = String.IsNullOrEmpty(recurso) ? this._url : String.Concat(this._url, "/", recurso);

            this._logger.LogDebug("##### FindAllAsync #####");
            this._logger.LogDebug(url);

            var response  = await _clientHttp.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            IEnumerable<T> lista = JsonConvert.DeserializeObject<IEnumerable<T>>(responseBody);

            return lista;
        }

        public async Task<T> FindByIdAsync(int id, string recurso = "")
        {
            if (id == 0)
            {
                return default(T);
            }

            string url = String.IsNullOrEmpty(recurso) ? this._url : String.Concat(this._url, "/", recurso);

            this._logger.LogDebug("##### FindByIdAsync #####");
            this._logger.LogDebug(url);

            var response = await _clientHttp.GetAsync(string.Concat(url, $"/{id}"));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            T obj = JsonConvert.DeserializeObject<T>(responseBody);

            return obj;
        }

        public async Task<T> CompleteFindByIdAsync(int id)
        {
            if (id == 0)
            {
                return default(T);
            }

            string url = string.Concat(this._url, $"/completo/{id}");

            this._logger.LogDebug("##### CompleteFindByIdAsync #####");
            this._logger.LogDebug(url);

            var response = await _clientHttp.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            T obj = JsonConvert.DeserializeObject<T>(responseBody);

            return obj;
        }

        public async Task<int> InsertAsync(T obj, string recurso = "")
        {
            string url = String.IsNullOrEmpty(recurso) ? this._url : String.Concat(this._url, "/", recurso);

            this._logger.LogDebug("##### InsertAsync #####");
            this._logger.LogDebug(url);

            var jsonContent = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _clientHttp.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.Id;
                }
            }

            return 0;
        }

        public async Task<bool> UpdateAsync(int id, T obj, string recurso = "")
        {
            string url = String.IsNullOrEmpty(recurso) ? this._url : String.Concat(this._url, "/", recurso);

            this._logger.LogDebug("##### UpdateAsync #####");
            this._logger.LogDebug(url);

            var jsonContent = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _clientHttp.PutAsync(string.Concat(url, $"/{id}"), content);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.status;
                }
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id, string recurso = "")
        {
            if (id == 0)
            {
                return false;
            }

            string url = String.IsNullOrEmpty(recurso) ? this._url : String.Concat(this._url, "/", recurso);

            this._logger.LogDebug("##### DeleteAsync #####");
            this._logger.LogDebug(url);

            HttpResponseMessage response =  await _clientHttp.DeleteAsync(string.Concat(url, $"/{id}"));
            
            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.status;
                }
            }

            return false;
        }
    }
}
