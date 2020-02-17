using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherWeb.Models.Utility
{
    public class WebRequestHelper
    {
        #region -Singleton-
        private static readonly WebRequestHelper instance = new WebRequestHelper();

        static WebRequestHelper()
        {
        }
        private WebRequestHelper()
        {
        }
        public static WebRequestHelper Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        /// <summary>[GET]送出API請求</summary>
        public static string GetApi(string url, string contentType = "application/json", Dictionary<string, string> optionHeader = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string ret = string.Empty;
                request.ContentType = contentType;
                request.Method = "GET";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                if (optionHeader != null)
                {
                    foreach (string key in optionHeader.Keys)
                    {
                        request.Headers.Add(key, optionHeader[key]);
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    StreamReader tReader = new StreamReader(response.GetResponseStream());
                    ret = tReader.ReadToEnd();
                    tReader.Close();
                }
                return ret;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>[POST]送出API請求</summary>
        public static string PostApi(string url, string postdata, string contentType = "application/json", Dictionary<string, string> optionHeader = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = contentType;
                request.Method = "POST";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                byte[] bs = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = bs.Length;
                request.Timeout = 30000; //單位：毫秒，目前設定為30秒

                if (optionHeader != null)
                {
                    foreach (string key in optionHeader.Keys)
                    {
                        request.Headers.Add(key, optionHeader[key]);
                    }
                }

                using (Stream req = request.GetRequestStream())
                {
                    req.Write(bs, 0, bs.Length);
                }
                string ret = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader tReader = new StreamReader(response.GetResponseStream()))
                    {
                        ret = tReader.ReadToEnd();
                    }
                }
                return ret;
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    using (StreamReader tReader = new StreamReader(response.GetResponseStream()))
                    {
                        var errorMsg = tReader.ReadToEnd();
                        return errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>[POST]送出API請求</summary>
        public static void PostApiWithoutResponse(string url, string postdata, string contentType = "application/json", Dictionary<string, string> optionHeader = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = contentType;
                request.Method = "POST";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = bs.Length;
                request.Timeout = 300000;

                if (optionHeader != null)
                {
                    foreach (string key in optionHeader.Keys)
                    {
                        request.Headers.Add(key, optionHeader[key]);
                    }
                }

                using (Stream req = request.GetRequestStream())
                {
                    req.Write(bs, 0, bs.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    /// discard response
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    /// discard response
                }
            }
            catch
            { }
        }

        private static string EncryptHelperMD5(string origin)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();//建立一個MD5
            byte[] source = Encoding.Default.GetBytes(origin);//將字串轉為Byte[]
            byte[] data = md5.ComputeHash(source);//進行MD5加密

            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public class Response<T>
        {
            public string rDesc { get; set; }
            public string rCode { get; set; }
            public string TokenExpires { get; set; }
            public T Data { get; set; }
        }
    }
}
