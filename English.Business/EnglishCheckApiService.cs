using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace English.Business
{
    public class EnglishCheckApiService : WebRequestBase
    {
        public static async Task<CheckEnglishSentenceResponse> CheckEnglishSentenceAsync(string queryText)
        {
            var requestUrl = GetRequestUrl(queryText);
            //try
            //{
            var result = await RequestUrlAsync(requestUrl);

            var response = JsonConvert.DeserializeObject<CheckEnglishSentenceResponse>(result);

            return response;
            //}
            //catch (Exception e)
            //{
            //result = "unable_to_connect_to_server: "
            //         + " URL: " + uri.ToString()
            //         + " RESULT: " + result
            //         + " EXCEPTION: " + e.ToString();
            //    return null;
            //}
        }

        private static string GetRequestUrl(string queryText, string language = "en-US")
        {
            var requestUrl = "http://localhost:8081/v2/check?" +
                             $"language={language}&text={WebUtility.UrlEncode(queryText)}";

            return requestUrl;
        }
    }

    [DataContract]
    public class CheckEnglishSentenceResponse
    {
        [DataMember(Name = "matches")]
        public List<EnglishSentenceCheckMatchInfo> MatchInfos { get; set; }

        public override string ToString()
        {
            string matchString = string.Empty;
            int index = 1;
            if (MatchInfos != null)
            {
                foreach (var checkMatchInfo in MatchInfos)
                {
                    var context = checkMatchInfo.CheckMatchContext;
                    var message = string.IsNullOrEmpty(checkMatchInfo.ShortMessage)? checkMatchInfo.Message: checkMatchInfo.ShortMessage;
                    matchString += $"{index++}. " + message + " :  " + context.Text.Substring(context.StartIndex, context.Length) + "\r\n";
                    if (checkMatchInfo.Replacements != null && checkMatchInfo.Replacements.Count > 0)
                    {
                        matchString += "Suggest :  " + checkMatchInfo.Replacements[0].Replacement + "\r\n\r\n";
                    }
                }
            }

            return matchString;
        }
    }
    [DataContract]
    public class EnglishSentenceCheckMatchInfo
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "shortMessage")]
        public string ShortMessage { get; set; }

        [DataMember(Name = "context")]
        public CheckMatchContext CheckMatchContext { get; set; }

        [DataMember(Name = "replacements")]
        public List<CheckMatchReplacement> Replacements { get; set; }
    }
    [DataContract]
    public class CheckMatchContext
    {
        [DataMember(Name = "offset")]
        public int StartIndex { get; set; }
        [DataMember(Name = "length")]
        public int Length { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

    [DataContract]
    public class CheckMatchReplacement
    {
        [DataMember(Name = "value")]
        public string Replacement { get; set; }
    }
}
