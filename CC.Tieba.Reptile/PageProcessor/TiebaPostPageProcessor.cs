using CC.Tieba.Lib;
using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Downloader;
using DotnetSpider.Extraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Web;
namespace CC.Tieba.Reptile.PageProcessor
{
    /// <summary>
    /// 帖子信息数据抽取【帖子列表页为手机端，详情页为电脑端】
    /// </summary>
    public class TiebaPostPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            var results = new List<TiebaPost>();
            //如果是帖子列表页面，就获取所有帖子
            if (page.Request.RequestUri.AbsoluteUri.Contains("tieba.baidu.com/f?"))
            {
                var totalPostsElements = page.Selectable().SelectList(Selectors.XPath(".//li[@class='tl_shadow tl_shadow_new ']")).Nodes();
                foreach (var postElement in totalPostsElements)
                {
                    TiebaPost tiebaPost = new TiebaPost();
                    //tiebaPost.Title = classElement.Select(Select.Regex("(?<=title=\")[^ (\" target=\"_blank\")]*(?=(\" target = \"_blank\" class=\"j_th_tit \"))")).GetValue();
                    //tiebaPost.ReplyNum = Convert.ToInt32(classElement.Select(Selectors.Regex("(?<=(title=\"回复\">))[0-9]*")).GetValue());
                    //tiebaPost.UserName = classElement.Select(Selectors.Regex("(?<=(title=\"主题作者: ))[^ (\"\\s)]*")).GetValue();

                    tiebaPost.Key = postElement.Select(Selectors.XPath(".//a[@class='j_common ti_item ']/@data-tid")).GetValue();
                    tiebaPost.Title = postElement.Select(Selectors.XPath(".//div[@class='ti_title']")).GetValue(ValueOption.InnerText).Trim();
                    tiebaPost.ReplyNum = Convert.ToInt32(postElement.Select(Selectors.XPath(".//div[@class='ti_func_btn btn_reply']")).GetValue(ValueOption.InnerText));
                    tiebaPost.UserName = postElement.Select(Selectors.XPath(".//div[@class='ti_author_icons  clearfix']//span")).GetValue(ValueOption.InnerText).Trim();
                    string time = postElement.Select(Selectors.XPath(".//span[@class='ti_time']")).GetValue();
                    tiebaPost.StartTime = DateTime.Parse(time);
                    tiebaPost.UpdateTime = DateTime.Now;
                    //获取到数据添加到集合中
                    results.Add(tiebaPost);
                    //在列表中获取到帖子，把帖子详情链接添加到队列中
                    page.AddTargetRequest(new Request($"https://tieba.baidu.com/p/{tiebaPost.Key}"));
                }
            }
            //否则如果是帖子详情页，则更新帖子数据(相比较列表页，可以更新一些信息)
            else if (Regex.IsMatch(page.Request.RequestUri.AbsoluteUri, "tieba.baidu.com/p/[0-9]*"))
            {
                TiebaPost tiebaPost = new TiebaPost();
                try
                {
                    tiebaPost.Title = page.Selectable().Select(Selectors.XPath(".//h1")).GetValue().Trim();
                }
                catch (Exception)
                {
                    tiebaPost.Title = page.Selectable().Select(Selectors.XPath(".//h3")).GetValue().Trim();
                }
                tiebaPost.ReplyNum = Convert.ToInt32(page.Selectable().SelectList(Selectors.XPath(".//li[@class='l_reply_num']//span")).Nodes().First().GetValue());

                var postElements = page.Selectable().SelectList(Selectors.XPath(".//div[@id='j_p_postlist']//div")).Nodes();
                var postElement = page.Selectable().SelectList(Selectors.XPath(".//div[@id='j_p_postlist']//div")).Nodes().First();
                var json = postElement.Select(Selectors.XPath("@data-field")).GetValue();
                JObject jObject = JObject.Parse(HttpUtility.HtmlDecode(json));
                tiebaPost.Key = jObject["content"]["post_id"].ToString();
                if (jObject["content"].Contains("content"))
                    tiebaPost.Body = jObject["content"]["content"].ToString();
                tiebaPost.UserName = jObject["author"]["user_name"].ToString();
                tiebaPost.UserNickName = jObject["author"]["user_nickname"].ToString();
                tiebaPost.UserID = jObject["author"]["user_id"].ToString();
                string time;
                if (jObject["content"]["date"] != null)
                    time = jObject["content"]["date"].ToString();
                else
                {
                    var ls = postElement.Select(Selectors.XPath("//div[@class='post-tail-wrap']")).XPath(".//span").Nodes();
                    if (ls==null)
                    {
                        Console.WriteLine();
                    }
                    time = ls.Last().GetValue();
                }
                tiebaPost.StartTime = TiebaTime.Parse(time);
                tiebaPost.UpdateTime = DateTime.Now;
                //获取到数据添加到集合中
                results.Add(tiebaPost);
            }
            //将数据添加进去，使得数据存储类可以拿到数据
            if (results.Count > 0)
                page.AddResultItem("TiebaPost", results);
        }
    }
}
