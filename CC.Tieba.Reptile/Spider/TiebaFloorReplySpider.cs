using CC.Tieba.EF.Entity;
using CC.Tieba.Reptile.PageProcessor;
using CC.Tieba.Reptile.Pipeline;
using DotnetSpider.Downloader;
using DotnetSpider.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Tieba.Reptile.Spider
{
    /// <summary>
    /// 帖子楼层回复爬取
    /// </summary>
    public class TiebaFloorReplySpider : EntitySpider
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsClass"></param>
        public TiebaFloorReplySpider()
        {
            //后续通过配置文件加载配置
        }

        /// <summary>
        /// 通过添加帖子楼层来更新楼层回复数据
        /// </summary>
        /// <param name="postId"></param>
        public void AddFloorID(string floorId)
        {
            AddRequest(new Request($"https://tieba.baidu.com/t/p/{floorId}")
            {
                UserAgent = "Mozilla / 5.0(Linux; Android 6.0; Nexus 5 Build / MRA58N) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 74.0.3729.157 Mobile Safari/ 537.36"
            });
        }

        /// <summary>
        /// 通过添加帖子楼层来更新楼层回复数据
        /// </summary>
        /// <param name="floorIds"></param>
        public void AddFloorIDs(IEnumerable<string> floorIds)
        {
            var listRequest = floorIds.Select(p => new Request($"https://tieba.baidu.com/t/p/{p}")
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
            AddPipeline(new TiebaFloorReplyPipeline());
            AddPageProcessor(new TiebaFloorReplyPageProcessor());
        }
    }
}
