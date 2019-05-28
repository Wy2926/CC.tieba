using CC.Tieba.Lib;
using CC.Tieba.Lib.Expand;
using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Downloader;
using DotnetSpider.Extraction;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CC.Tieba.Reptile.PageProcessor
{
    /// <summary>
    /// 帖子楼层数据抽取
    /// </summary>
    public class TiebaFloorPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            var results = new List<TiebaFloor>();
            if (Regex.IsMatch(page.Request.RequestUri.AbsoluteUri, "tieba.baidu.com/p/[0-9]*"))
            {
                var totalFloorElements = page.Selectable().SelectList(Selectors.XPath(".//div[@class='l_post l_post_bright j_l_post clearfix  ']")).Nodes();
                foreach (var floorElement in totalFloorElements)
                {
                    //遍历获取楼层数据
                    TiebaFloor tiebaFloor = new TiebaFloor();
                    var json = floorElement.Select(Selectors.XPath("@data-field")).GetValue();
                    JObject jObject = JObject.Parse(HttpUtility.HtmlDecode(json));
                    tiebaFloor.PostIndex = Convert.ToInt32(jObject["content"]["post_no"].ToString());
                    //如果楼层索引为0，那么这楼就是帖子正文，不用加到帖子楼层里面
                    if (tiebaFloor.PostIndex == 0)
                        continue;
                    tiebaFloor.Key = jObject["content"]["post_id"].ToString();
                    tiebaFloor.ForumID = jObject["content"]["forum_id"].ToString();
                    tiebaFloor.ThreadID = jObject["content"]["thread_id"].ToString();
                    tiebaFloor.FloorBody = jObject["content"]["content"].ToString();
                    tiebaFloor.UserName = jObject["author"]["user_name"].ToString();
                    tiebaFloor.UserNickName = jObject["author"]["user_nickname"].ToString();
                    tiebaFloor.UserID = jObject["author"]["user_id"].ToString();
                    string time = floorElement.SelectList(Selectors.XPath(".//span[@class='tail-info']")).Nodes().Last().GetValue();
                    tiebaFloor.CommentTime = TiebaTime.Parse(time);
                    tiebaFloor.UpdateTime = DateTime.Now;
                    //获取到数据添加到集合中
                    results.Add(tiebaFloor);
                }
                //获取帖子总页数
                int total = Convert.ToInt32(page.Selectable().SelectList(Selectors.XPath(".//li[@class='l_reply_num']//span[@class='red']")).Nodes().Last().GetValue());
                string currStr = page.Selectable().SelectList(Selectors.XPath(".//span[@class='tP']"))?.GetValue();
                //当前页索引
                int currIndex = 1;
                if (currStr != null)
                    currIndex = Convert.ToInt32(currStr);
                if (currIndex < total)
                    //往后翻页(此处不循环添加主要考虑帖子的数量可能很多，待爬取链接可能会撑爆内存)
                    page.AddTargetRequest(new Request(page.Request.RequestUri.SetParameter("pn", (currIndex + 1).ToString()).AbsoluteUri));
            }
            //将数据添加进去，使得数据存储类可以拿到数据
            if (results.Count > 0)
                page.AddResultItem("TiebaFloor", results);
        }
    }
}
