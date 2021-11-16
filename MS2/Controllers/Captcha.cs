using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Controllers
{
    public static class Captcha
    {
        public static ReCaptchaResponse VerifyCaptcha(string secret, string response)
        {
            using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
            {
                var values = new Dictionary<string,
                    string> {
                        {
                            "secret",
                            secret
                        },
                        {
                            "response",
                            response
                        }
                    };

                var content = new System.Net.Http.FormUrlEncodedContent(values);
                var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                var responseString = Response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    ReCaptchaResponse res = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                    return res;
                }
                else throw new System.Exception("RESPONSE WAS NULL");
            }
        }

        public class ReCaptchaResponse
        {
            public bool success
            {
                get;
                set;
            }
            public string challenge_ts
            {
                get;
                set;
            }
            public string hostname
            {
                get;
                set;
            }

            [JsonProperty(PropertyName = "error-codes")]
            public List<string> error_codes
            {
                get;
                set;
            }
        }
    }
}
