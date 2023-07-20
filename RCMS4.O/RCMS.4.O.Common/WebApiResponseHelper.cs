using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RCMS._4.O.Entities;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using Newtonsoft.Json.Linq;

namespace RCMS._4.O.Common
{
    public class WebApiResponseHelper
    {
        public WebApiResponseHelper() { }

        // Credentails pass as a query string in URL
        public async static Task<ResponseInfo<T>> GetResponseFromWebAPI<T>(string baseUrl, string webApiMethod)
        {
            HttpResponseMessage response = null;
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(webApiMethod);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api
                            var webApiResponse = response.Content.ReadAsStringAsync().Result;
                            if (webApiResponse != null)
                            {
                                //Deserializing the response recieved from web api and storing into the Employee list
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);
                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");
                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
        public async static Task<ResponseInfo<T>> GetDataResponseFromWebAPI<T>(string baseUrl, string webApiMethod, string key, string token)
        {
            HttpResponseMessage response = null;
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("authToken", token);
                    client.DefaultRequestHeaders.Add("apiKey", key);
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(webApiMethod);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api
                            var webApiResponse = response.Content.ReadAsStringAsync().Result;
                            if (webApiResponse != null)
                            {
                                //Deserializing the response recieved from web api and storing into the Employee list
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);
                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");
                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
        // Parameter pass as a query string in URL
        public async static Task<ResponseInfo<T>> GetDataResponseFromWebAPIWithParms<T>(string baseUrl, string webApiMethod, string key, string token)
        {
            HttpResponseMessage response = null;
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("authToken", token);
                    client.DefaultRequestHeaders.Add("apiKey", key);
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(webApiMethod);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var webApiResponse = response.Content.ReadAsStringAsync().Result;
                            if (webApiResponse != null)
                            {
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);

                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");
                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
        public async static Task<ResponseInfo<T>> PostResponseFromWebAPI<T>(string baseUrl, string webApiMethod, string token, string key, T inputParams)
        {
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("authToken", token);
                    client.DefaultRequestHeaders.Add("apiKey", key);
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonstring = JsonConvert.SerializeObject(inputParams);
                    HttpContent inputContent = new StringContent(jsonstring.ToString(), System.Text.Encoding.UTF8, "application/json");
                    response = client.PostAsync(webApiMethod, inputContent).Result;
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var webApiResponse = await response.Content.ReadAsStringAsync();
                            if (webApiResponse != null)
                            {
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);
                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");

                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
        public async static Task<ResponseInfo<T>> PostResponseFromWebAPIFromBody<T>(string baseUrl, string webApiMethod, string token, string key, [FromBody] string inputParams)
        {
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("authToken", token);
                    client.DefaultRequestHeaders.Add("apiKey", key);
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonstring = JsonConvert.SerializeObject(inputParams);
                    HttpContent inputContent = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                    response = client.PostAsync(webApiMethod, inputContent).Result;
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var webApiResponse = await response.Content.ReadAsStringAsync();
                            if (webApiResponse != null)
                            {
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);
                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");

                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
        public async static Task<ResponseInfo<T>> PostResponseFromWebAPIJToken<T>(string baseUrl, string webApiMethod, string token, string key, JObject inputParams)
        {
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("authToken", token);
                    client.DefaultRequestHeaders.Add("apiKey", key);
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonstring = JsonConvert.SerializeObject(inputParams);
                    HttpContent inputContent = new StringContent(jsonstring.ToString(), System.Text.Encoding.UTF8, "application/json");
                    response = client.PostAsync(webApiMethod, inputContent).Result;
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var webApiResponse = await response.Content.ReadAsStringAsync();
                            if (webApiResponse != null)
                            {
                                responseInfo = JsonConvert.DeserializeObject<ResponseInfo<T>>(webApiResponse);
                            }
                            else
                            {
                                responseInfo = new ResponseInfo<T>();
                                responseInfo.ResultCode = (int)(response.StatusCode);
                                responseInfo.ResultType = Enums.ResultType.Info;
                                responseInfo.ResultDesc = response.ReasonPhrase;
                            }
                        }
                        else
                        {
                            responseInfo.ResultCode = (int)(response.StatusCode);
                            responseInfo.ResultType = Enums.ResultType.Error;
                            responseInfo.ResultDesc = string.Format("Server returned HTTP error {0}: {1}.", (int)response.StatusCode, response.ReasonPhrase);
                        }
                    }
                    else
                    {
                        responseInfo.ResultCode = (int)(response.StatusCode);
                        responseInfo.ResultType = Enums.ResultType.Error;
                        responseInfo.ResultDesc = string.Format("No Any Response Received from Server {0}: {1}.", -500, "No HTTP Response ");

                    }
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultCode = (int)(response.StatusCode);
                responseInfo.ResultType = Enums.ResultType.Error;
                responseInfo.ResultDesc = ex.Message.ToString(); ;
            }
            finally
            {
                response = null;
            }
            return responseInfo;
        }
    }
}