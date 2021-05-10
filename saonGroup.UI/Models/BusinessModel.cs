using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace saonGroup.UI.Models
{
    public class BusinessModel
    {
        private string apiKey = "";
        private string apiHost = "";
        public BusinessModel(string _apiHost, string _apiKey) {
            this.apiHost = _apiHost;
            this.apiKey = _apiKey;
        }
        public List<RegionModel> DummyDataRegion()
        {
            List<RegionModel> result = new List<RegionModel>();
            result.Add(new RegionModel { iso = "USA", confirmed = 100, deaths = 50, name = "United States", recovered = 1 });
            result.Add(new RegionModel { iso = "CHN", confirmed = 100, deaths = 50, name = "CHINA", recovered = 1 });
            result.Add(new RegionModel { iso = "GTM", confirmed = 100, deaths = 50, name = "GUATEMALA", recovered = 1 });
            result.Add(new RegionModel { iso = "ESL", confirmed = 100, deaths = 50, name = "El SALVADOR", recovered = 1 });
            return result;
        }

        public List<RegionModel> DataRegion(int top=10,string date = "",string ISO ="")
        {
            List<RegionModel> result = new List<RegionModel>(); 
            var taskGetData = Task.Run(async () => await this.getDataAsync(date, ISO));
            string body = taskGetData.Result;
                JObject allData = JObject.Parse(body); 
                JArray allRegions = (JArray)allData["data"];
                result = allRegions.Select(p => new  
                                                  {
                                                    confirmed = (int)p["confirmed"],
                                                    deaths = (int)p["deaths"],
                                                    iso = (string)p["region"]["iso"],
                                                    name = (string)p["region"]["name"],
                                                    recovered = (int)p["recovered"]
                                                })
                                    .GroupBy(x=> new { x.iso,x.name} )
                                    .Select(g => new RegionModel
                                                      { iso = g.Key.iso,
                                                        name = g.Key.name,
                                                        confirmed= g.Sum(a=> a.confirmed),
                                                        deaths= g.Sum(a=> a.deaths),
                                                        recovered=g.Sum(a=> a.recovered)
                                                      })
                                    .OrderByDescending(o => o.confirmed)
                                    .Take(top)
                                    .ToList();
            return result;
        }

        public List<ProvinceModel> DataProvince(string ISO,int top = 10, string date = "")
        {
            List<ProvinceModel> result = new List<ProvinceModel>();
            var taskGetData = Task.Run(async () => await this.getDataAsync(date, ISO));
            string body = taskGetData.Result;
            JObject allData = JObject.Parse(body);
            JArray province = (JArray)allData["data"];
            result = province.Select(p => new ProvinceModel
                                {
                                    confirmed = (int)p["confirmed"],
                                    deaths = (int)p["deaths"],
                                    iso = (string)p["region"]["iso"],
                                    name = (string)p["region"]["name"],
                                    recovered = (int)p["recovered"],
                                    province=(string)p["region"]["province"]
                                }) 
                                .OrderByDescending(o => o.confirmed)
                                .Take(top)
                                .ToList();
            return result;
        }

            #region RAPID_API
            /// <summary>
            /// get data from RAPID_API, endpoint reports
            /// </summary>
            /// <param name="date">The date of report in the format Y-m-d | empty default current date</param>
            /// <param name="ISO">Country ISO code</param>
            /// <returns></returns>
            private async Task<string> getDataAsync(string date, string ISO) {
            string result = string.Empty;
            string urlBase = "https://covid-19-statistics.p.rapidapi.com/reports";
            urlBase += string.IsNullOrEmpty(date) ? "" : "?date="+date;
            if (!string.IsNullOrEmpty(ISO)) {
                urlBase += string.IsNullOrEmpty(date) ? "?" : "&";
                urlBase += "iso="+ISO;
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlBase),
                Headers =
                    {
                        { "x-rapidapi-key",  this.apiKey},
                        { "x-rapidapi-host", this.apiHost},
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync(); 
            }
            return result;
        }
        #endregion
    }
}
