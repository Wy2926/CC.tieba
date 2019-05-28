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
    /// 帖子楼层数据存储
    /// </summary>
    public class TiebaFloorPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            foreach (var resultItem in resultItems)
            {
                if (!resultItem.ContainsKey("TiebaFloor"))
                    continue;
                List<TiebaFloor> floors = resultItem["TiebaFloor"] as List<TiebaFloor>;
                foreach (var floor in floors)
                {
                    Console.WriteLine($"【{floor.UserName}】:{floor.FloorBody} {floor.PostIndex + 1}楼 评论时间:{floor.CommentTime}\r\n");
                }
                FloorServer floorServer = new FloorServer();
                floorServer.AddorUpdates(floors);
            }
        }
    }
}
