using CC.Tieba.EF.Entity;
using CC.Tieba.Reptile.PageProcessor;
using CC.Tieba.Reptile.Pipeline;
using DotnetSpider.Downloader;
using DotnetSpider.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CC.Tieba.Reptile.Spider
{
    /// <summary>
    /// 贴吧帖子爬取
    /// </summary>
    public class TiebaPostSpider : EntitySpider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsClass"></param>
        public TiebaPostSpider()
        {
            //后续通过配置文件加载配置
        }

        /// <summary>
        /// 通过添加贴吧名称来更新贴吧帖子数据
        /// </summary>
        /// <param name="name"></param>
        public void AddTiebaName(string name)
        {
            AddRequest(new Request($"https://tieba.baidu.com/f?kw={HttpUtility.UrlEncode(name)}")
            {
                UserAgent = "Mozilla / 5.0(Linux; Android 6.0; Nexus 5 Build / MRA58N) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 74.0.3729.157 Mobile Safari/ 537.36"
            });
        }

        /// <summary>
        /// 通过添加贴吧名称集合来更新贴吧数据
        /// </summary>
        /// <param name="names"></param>
        public void AddTiebaNames(IEnumerable<string> names)
        {
            var listRequest = names.Select(p => new Request($"https://tieba.baidu.com/f?kw={p}")
            {
                UserAgent = "Mozilla / 5.0(Linux; Android 6.0; Nexus 5 Build / MRA58N) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 74.0.3729.157 Mobile Safari/ 537.36"
            }).ToList();
            AddRequests(listRequest);
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="arguments"></param>
        protected override void OnInit(params string[] arguments)
        {
            AddPipeline(new TiebaPostPipeline());
            AddPageProcessor(new TiebaPostPageProcessor());
        }
    }
}
