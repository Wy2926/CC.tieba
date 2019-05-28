using CC.Tieba.Application;
using CC.Tieba.EF.Entity;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CC.Tieba.Reptile.Pipeline
{
    /// <summary>
    /// 帖子楼层回复数据存储
    /// </summary>
    public class TiebaFloorReplyPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            foreach (var resultItem in resultItems.Where(p => p.ContainsKey("TiebaFloorReply")))
            {
                List<TiebaFloorReply> replys = resultItem["TiebaFloorReply"] as List<TiebaFloorReply>;
                foreach (var reply in replys)
                {
                    Console.WriteLine($"【回复楼:{reply.FloorID}】【{reply.UserNickName}】:{reply.Body} 回复时间:{reply.ReplyTime}\r\n");
                }
                FloorReplyServer floorReplyServer = new FloorReplyServer();
                //添加到数据库
                floorReplyServer.AddorUpdates(replys);
            }
        }
    }
}
