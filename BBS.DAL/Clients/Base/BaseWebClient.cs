using BBS.DAL.Constants;
using BBS.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Clients.Base
{
    public abstract class BaseWebClient<T> : IBaseWebClient<T>
    {
        private readonly ILogger _logger;
        private readonly string _url;
        public BaseWebClient(ILogger logger, string route, IConfiguration configuration)
        {
            _logger = logger;
            _url = configuration.GetSection("API_URL").Value + route;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_url);

                if (response.IsSuccessStatusCode)
                {
                    string stringToDeserialize = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(stringToDeserialize);
                }
                else
                {
                    _logger.Warn($"{Messages.HttpBadStatusCode}: {response}");
                    throw new HttpRequestException();
                }
            }
        }

        public async Task<T> GetById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_url + $"/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{Messages.SuccessMessage}: {response}");
                    string stringToDeserialize = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(stringToDeserialize);
                }
                else
                {
                    _logger.Warn($"{Messages.HttpBadStatusCode}: {response}");
                    throw new HttpRequestException();
                }
            }
        }

        public async Task Create(T obj)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var serializeObject = JsonConvert.SerializeObject(obj);
                    var content = Encoding.UTF8.GetBytes(serializeObject);
                    var byteContent = new ByteArrayContent(content);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(_url, byteContent);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Info($"{Messages.SuccessMessage}: {response}");
                    }
                    else
                    {
                        _logger.Warn($"{Messages.HttpBadStatusCode}: {response}");
                        throw new HttpRequestException();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            
        }

    }
}
