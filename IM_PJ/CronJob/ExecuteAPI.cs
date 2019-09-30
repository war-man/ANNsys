using IM_PJ.Models.Pages.ExecuteAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace IM_PJ.CronJob
{
    public class ExecuteAPI
    {
        private readonly HttpClient _client;

        public ExecuteAPI()
        {
            this._client = new HttpClient();
        }
        

        public ResponseProductStatusModel postSync(IM_PJ.Models.CronJobProductStatu data)
        {
            ResponseProductStatusModel result = null;

            try
            {
                this._client.BaseAddress = new Uri(data.Web);
                this._client.DefaultRequestHeaders.Accept.Clear();
                this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP POST
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("key", "828327"),
                    new KeyValuePair<string, string>("sku", data.SKU),
                    new KeyValuePair<string, string>("visibility", data.IsHidden ? "hidden" : "visible")
                });

                var response = this._client.PostAsync(data.API, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<ResponseProductStatusModel>().Result;
                }
                else
                {
                    result = new ResponseProductStatusModel() {
                        response = "error",
                        content = "HTTP status: " + response.StatusCode.ToString()
                    };
                }

            }
            catch (ThreadAbortException tb)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + tb.Message);
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                result = new ResponseProductStatusModel() {
                    response = "error",
                    content = ex.Message.Length > 255 ? ex.Message.Substring(0, 254) : ex.Message
                };
            }

            return result;
        }
    }
}