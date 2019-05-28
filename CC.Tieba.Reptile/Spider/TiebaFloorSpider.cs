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
    /// 帖子楼层爬取
    /// </summary>
    public class TiebaFloorSpider : EntitySpider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsClass"></param>
        public TiebaFloorSpider()
        {
            //后续通过配置文件加载配置
        }

        /// <summary>
        /// 通过添加帖子来更新帖子楼层数据
        /// </summary>
        /// <param name="postId"></param>
        public void AddPostID(string postId)
        {
            AddRequest(new Request($"https://tieba.baidu.com/p/{postId}"));
        }

        /// <summary>
        /// 通过添加帖子来更新帖子楼层数据
        /// </summary>
        /// <param name="postIds"></param>
        public void AddPostIDs(IEnumerable<string> postIds)
        {
            var listRequest = postIds.Select(p => new Request($"https://tieba.baidu.com/p/{p}")).ToList();
            AddRequests(listRequest);
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="arguments"></param>
        protected override void OnInit(params string[] arguments)
        {
            AddPipeline(new TiebaFloorPipeline());
            AddPageProcessor(new TiebaFloorPageProcessor());
        }
    }
}
