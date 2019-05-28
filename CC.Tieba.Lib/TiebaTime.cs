using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CC.Tieba.Lib
{
    /// <summary>
    /// 贴吧时间转换
    /// </summary>
    public static class TiebaTime
    {
        /// <summary>
        /// 贴吧时间转换为DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Parse(string date)
        {
            if (date.Contains("今天"))
            {
                return DateTime.Parse(date.Replace("今天", "").Trim());
            }
            if (Regex.IsMatch(date, @"^\d+-\d+ \d+:\d+$"))
            {
                return DateTime.Parse($"{DateTime.Now.Year}-{date}");
            }
            return DateTime.Parse(date);
        }
    }
}
