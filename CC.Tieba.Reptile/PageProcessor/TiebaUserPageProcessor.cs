using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Extraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using DotnetSpider.Downloader;

namespace CC.Tieba.Reptile.PageProcessor
{
    /// <summary>
    /// 贴吧用户数据抽取
    /// </summary>
    public class TiebaUserPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {

            var results = new List<TiebaUser>();
            if (page.Request.Url.Contains("https://tieba.baidu.com/home/main"))
            {
                if (page.Request.Properties.ContainsKey("页面类型"))
                {
                    string htmlType = page.Request.Properties["页面类型"];
                    if (htmlType.Equals("手机"))
                    {
                        TiebaUser tiebaUser = new TiebaUser();
                        var totalUserInfoElements = page.Selectable().SelectList(Selectors.XPath(".div//tab tab_holo home_tab j_home_tab']")).Nodes();
                        List<int> info = totalUserInfoElements.Select(p => p.Select(Selectors.XPath(".//span[@class='home_tab_item_num']")).GetValue()).Select(p => Convert.ToInt32(p)).ToList();
                        tiebaUser.Post_Num = info[0];
                        tiebaUser.PostBar_Num = info[1];
                        tiebaUser.Follow_Num = info[2];
                        tiebaUser.Fans_Num = info[3];
                        tiebaUser.Key = page.Selectable().Select(Selectors.Regex(@"(?<=(/i/\?portrait=))[0-9a-zA-Z]+")).GetValue();
                        tiebaUser.U_Nick = page.Selectable().Select(Selectors.XPath(".//a[class='home_card_uname_link']")).GetValue(ValueOption.InnerText);
                        results.Add(tiebaUser);
                        page.AddTargetRequest(new Request($"http://tieba.baidu.com/home/main/?un={tiebaUser.U_Nick}", new Dictionary<string, object>() { { "页面类型", "电脑" } }));
                    }
                    else if (htmlType.Equals("电脑"))
                    {
                        TiebaUser tiebaUser = new TiebaUser();
                        tiebaUser.U_Nick = page.Selectable().Select(Selectors.XPath(".//span[class='userinfo_username']")).GetValue(ValueOption.InnerText);
                        string userTitle = page.Selectable().Select(Selectors.XPath(".//span[class='user_name']")).GetValue(ValueOption.InnerText);
                        tiebaUser.U_Name = Regex.Match(userTitle, @"(?<=用户名:)[^"" <]+").Value;
                        tiebaUser.U_BaAge = Regex.Match(userTitle, @"(?<=吧龄:)\d+\.\d+").Value;
                        tiebaUser.Posting_Num = Convert.ToInt32(Regex.Match(userTitle, @"(?<=发帖:)\d+").Value);
                        results.Add(tiebaUser);
                    }
                }

            }
            //将数据添加进去，使得数据存储类可以拿到数据
            if (results.Count > 0)
            page.AddResultItem("TiebaUser", results);
        }
    }
}
