using FormulaOneSite.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FormulaOneSite.Data.ApiAccess
{
    public class ResultsApiAccess : IResultsApiAccess
    {
        private readonly string baseUrl;
        public ResultsApiAccess()
        {
            baseUrl = @"http://ergast.com/api/f1";
        }

        public async Task<List<StandingsModel>> GetStandingsAsync(int year)
        {
            using (HttpClient client = new HttpClient())
            {
                using(HttpResponseMessage resp = await client.GetAsync($"{baseUrl}/{year}/driverStandings.json"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        using(HttpContent content = resp.Content)
                        {
                            var res = JsonConvert.DeserializeObject<List<StandingsModel>>(JObject.Parse(
                                await content.ReadAsStringAsync())["MRData"]["StandingsTable"]["StandingsLists"][0]["DriverStandings"]
                                .ToString());
                            return res;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
