using CC.Tieba.Application;
using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CC.Tieba.Reptile.Pipeline
{
    /// <summary>
    /// 贴吧帖子信息数据存储
    /// </summary>
    public class TiebaPostPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            foreach (var resultItem in resultItems)
            {
                if (!resultItem.ContainsKey("TiebaPost"))
                    continue;
                List<TiebaPost> posts = resultItem["TiebaPost"] as List<TiebaPost>;
                foreach (var post in posts)
                {
                    //Console.WriteLine($"【{post.Title}】:{post.UserName} 帖子ID:{post.Key} 回复:{post.ReplyNum} 发帖时间:{post.StartTime}\r\n");
                }
                PostServer postServer = new PostServer();
                postServer.AddorUpdates(posts);
            }
        }
    }
}
