using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace CC.Tieba.Lib.Expand
{
    /// <summary>
    /// URI拓展类
    /// </summary>
    public static class UriExpand
    {
        /// <summary>
        /// URI是否存在此参数
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool IsGetParameter(this Uri uri, string parameter)
        {
            return GetParameter(uri, parameter) != null;
        }

        /// <summary>
        /// 获取URI的参数值
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetParameter(this Uri uri, string parameter)
        {
            string url = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(uri.AbsoluteUri));
            if (url.IndexOf('?') < 1)
            {
                return null;
            }
            //截取参数部分并分割
            string[] parameters = url.Substring(url.IndexOf('?') + 1).Split('&');
            List<string> vs = parameters.Where(p => p.IndexOf($"{parameter}=") == 0).ToList();
            if (vs.Count == 0)
            {
                return null;
            }
            return vs.FirstOrDefault().Substring(vs.FirstOrDefault().IndexOf('=') + 1);
        }

        public static Uri SetParameter(this Uri uri, string parameter, string value)
        {
            string url = uri.AbsoluteUri;
            if (url.IndexOf('?') < 1)
            {
                return new Uri($"{uri.AbsoluteUri}?{parameter}={value}");
            }
            if (!Regex.IsMatch(uri.AbsoluteUri, "parameter=[^&]+"))
            {
                return new Uri($"{uri.AbsoluteUri}&{parameter}={value}");
            }
            //截取参数部分并分割
            string newUrl = Regex.Replace(uri.AbsoluteUri, "parameter=[^&]+", $"{parameter}={value}");
            return new Uri(newUrl);
        }
    }
}
