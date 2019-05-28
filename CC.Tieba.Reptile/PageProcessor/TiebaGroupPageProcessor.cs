using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Downloader;
using DotnetSpider.Extraction;
using System;
using System.Collections.Generic;
using System.Text;
using CC.Tieba.Lib.Expand;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;
namespace CC.Tieba.Reptile.PageProcessor
{
    /// <summary>
    /// 贴吧信息数据抽取
    /// </summary>
    public class TiebaGroupPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            //如果是分类页面，就获取所有分类
            if (page.Request.RequestUri.AbsoluteUri.Contains("tieba.baidu.com/f/index/forumclass"))
            {
                var listRequest = new List<Request>();
                var totalClassElements = page.Selectable().SelectList(Selectors.XPath(".//ul[@class='item-list-ul clearfix']")).Nodes();
                foreach (var classElement in totalClassElements)
                {
                    IEnumerable<string> hrefs = classElement.SelectList(Selectors.XPath(".//li")).Nodes().Select(p => p.XPath(".//a/@href").GetValue());
                    foreach (var href in hrefs)
                    {
                        listRequest.Add(new Request(href, new Dictionary<string, dynamic>() { { "pageSize", 30 } })
                        {
                            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36"
                        });
                    }
                }
                //把获取到的所有分类链接加入到队列中
                page.AddTargetRequests(listRequest);
            }
            List<TiebaGroup> results = new List<TiebaGroup>();
            //分类页面获取贴吧信息
            if (page.Request.RequestUri.AbsoluteUri.Contains("tieba.baidu.com/f/index/forumpark"))
            {
                var listRequest = new List<Request>();
                var totalTiebaElements = page.Selectable().SelectList(Selectors.XPath(".//div[@id='ba_list']/div")).Nodes();
                foreach (var tiebaElement in totalTiebaElements)
                {
                    //var tieba = new TiebaGroup();
                    //tieba.Key = tiebaElement.Select(Selectors.XPath(".//div[@class='ba_like ']//@data-fid")).GetValue();
                    //tieba.Ba_Name = tiebaElement.Select(Selectors.XPath(".//p[@class='ba_name']")).GetValue();
                    //tieba.Ba_Desc = tiebaElement.Select(Selectors.XPath(".//p[@class='ba_desc']")).GetValue();
                    //tieba.Ba_M_Num = Convert.ToInt32(tiebaElement.Select(Selectors.XPath(".//span[@class='ba_m_num']")).GetValue());
                    //tieba.Ba_P_Num = Convert.ToInt32(tiebaElement.Select(Selectors.XPath(".//span[@class='ba_p_num']")).GetValue());
                    //tieba.Ba_Pic = tiebaElement.Select(Selectors.XPath(".//img[@class='ba_pic']/@src")).GetValue();
                    //tieba.FirstClassIfication = page.Request.RequestUri.GetParameter("pcn");
                    //tieba.TwoClassIfication = page.Request.RequestUri.GetParameter("cn");
                    //tieba.UpdateTime = DateTime.Now;
                    //results.Add(tieba);
                    string baName = tiebaElement.Select(Selectors.XPath(".//p[@class='ba_name']")).GetValue();
                    listRequest.Add(new Request($"http://tieba.baidu.com/f?kw={baName.Substring(0, baName.Length - 1)}"));
                }
                page.AddTargetRequests(listRequest);
                //如果是分类页面则需要分页
                int pageIndex = Convert.ToInt32(page.Request.RequestUri.GetParameter("pn") ?? "1");
                if (page.Request.Properties.ContainsKey("pageSize"))
                {
                    int pageSize = page.Request.Properties["pageSize"];
                    for (int i = pageIndex + 1; i <= pageSize; i++)
                    {
                        //往后翻页
                        page.AddTargetRequest(new Request(page.Request.RequestUri.SetParameter("pn", i.ToString()).AbsoluteUri));
                    }
                }
            }
            //贴吧主页获取贴吧信息
            else if (page.Request.RequestUri.AbsoluteUri.Contains("tieba.baidu.com/f?") && page.Request.RequestUri.IsGetParameter("kw"))
            {
                var select = page.Selectable();
                var tieba = new TiebaGroup();
                tieba.Key = Regex.Match(select.GetValue(), @"(?<=(PageData.forum = {\s*'id': ))\d+").Value;
                string title = select.Select(Selectors.XPath("//title")).GetValue();
                tieba.Ba_Name = Regex.Match(title, "[^>]*(?=(-百度贴吧))").Value;
                tieba.Ba_Desc = Regex.Match(title, "(?<=(-百度贴吧--))[^<]*").Value;
                tieba.Ba_M_Num = Convert.ToInt32(select.Select(Selectors.Regex("(?<=card_menNum\">)[\\d,]+")).GetValue().Replace(",", ""));
                tieba.Ba_P_Num = Convert.ToInt32(select.Select(Selectors.Regex("(?<=card_infoNum\">)[\\d,]+")).GetValue().Replace(",", ""));
                tieba.Ba_Pic = HttpUtility.UrlDecode(select.Select(Selectors.Regex("(?<=(wh_rate=null&amp;src=))[^\"]*")).GetValue());
                Uri uri = new Uri(HttpUtility.UrlDecode("http://tieba.baidu.com" + select.Select(Selectors.Regex(@"(?<=(<span>目录：</span>\s*<a rel=""noreferrer""\s*href=""))[^""]*")).GetValue()));
                tieba.FirstClassIfication = uri.GetParameter("fd");
                tieba.TwoClassIfication = uri.GetParameter("sd");
                tieba.UpdateTime = DateTime.Now;
                results.Add(tieba);
            }
            // Save data object by key. 以自定义KEY存入page对象中供Pipeline调用
            if (results.Count > 0)
                page.AddResultItem("TiebaGroup", results);
        }
    }
}
