using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Extraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using CC.Tieba.Lib;

namespace CC.Tieba.Reptile.PageProcessor
{
    /// <summary>
    /// 楼层回复数据抽取
    /// </summary>
    public class TiebaFloorReplyPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            var results = new List<TiebaFloorReply>();
            if (Regex.IsMatch(page.Request.RequestUri.AbsoluteUri, "tieba.baidu.com/t/p/[0-9]*"))
            {
                var totalFloorReplyElements = page.Selectable().SelectList(Selectors.XPath(".//ul[@class='pb_lzl_content j_floor_panel']")).Nodes();
                foreach (var floorReplyElement in totalFloorReplyElements)
                {
                    TiebaFloorReply floorReply = new TiebaFloorReply();
                    JObject jObject = JObject.Parse(floorReplyElement.Select(Selectors.XPath("@data-info")).GetValue());
                    floorReply.Key = jObject["pid"].ToString();
                    floorReply.UserName = jObject["un"].ToString();
                    floorReply.UserNickName = floorReplyElement.Select(Selectors.XPath(".//a[@class='user_name ']")).GetValue(ValueOption.InnerText);
                    floorReply.ThreadID = Regex.Match(page.Selectable().Select(Selectors.XPath(".//div[@class='pb_lzl_header_bar']//a//@href")).GetValue(), "[0-9]+").Value;
                    floorReply.FloorID = Regex.Match(page.Request.Url, "[0-9]+").Value;
                    floorReply.Body = floorReplyElement.Select(Selectors.XPath(".//a[@class='lzl_content j_lzl_content ']")).GetValue(ValueOption.InnerHtml);
                    floorReply.ReplyTime = TiebaTime.Parse(floorReplyElement.Select(Selectors.XPath(".//div[@class='left ']//div//p")).GetValue().Trim());
                    floorReply.UpdateTime = DateTime.Now;
                    results.Add(floorReply);
                }
            }
            //将数据添加进去，使得数据存储类可以拿到数据
            if (results.Count > 0)
            page.AddResultItem("TiebaFloorReply", results);
        }
    }
}
