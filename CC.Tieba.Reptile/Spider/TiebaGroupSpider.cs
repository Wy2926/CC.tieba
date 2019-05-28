using CC.Tieba.EF.Entity;
using CC.Tieba.Reptile.PageProcessor;
using CC.Tieba.Reptile.Pipeline;
using DotnetSpider.Downloader;
using DotnetSpider.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Security.Policy;

namespace CC.Tieba.Reptile.Spider
{
    /// <summary>
    /// 贴吧信息爬取
    /// </summary>
    public class TiebaGroupSpider : EntitySpider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsClass">是否爬取分类</param>
        public TiebaGroupSpider(bool IsClass)
        {
            if (IsClass)
                AddRequest(new Request("http://tieba.baidu.com/f/index/forumclass"));
            //后续通过配置文件加载配置
            ThreadNum = 30;
        }

        /// <summary>
        /// 通过添加贴吧名称来更新贴吧数据
        /// </summary>
        /// <param name="name"></param>
        public void AddTiebaName(string name)
        {
            AddRequest(new Request($"https://tieba.baidu.com/f?kw={HttpUtility.UrlEncode(name)}"));
        }

        /// <summary>
        /// 通过添加贴吧名称集合来更新贴吧数据
        /// </summary>
        /// <param name="names"></param>
        public void AddTiebaNames(IEnumerable<string> names)
        {
            var listRequest = names.Select(p => new Request($"https://tieba.baidu.com/f?kw={p}")).ToList();
            AddRequests(listRequest);
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="arguments"></param>
        protected override void OnInit(params string[] arguments)
        {
            AddPipeline(new TiebaGroupPipeline());
            AddPageProcessor(new TiebaGroupPageProcessor());
        }
    }
}
