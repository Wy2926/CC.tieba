using CC.Tieba.Application;
using CC.Tieba.Reptile.Spider;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Tieba.TaskDis.Job
{
    /// <summary>
    /// 帖子楼层回复更新任务
    /// </summary>
    [DisallowConcurrentExecution]
    class TiebaFloorReplyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var spider = new TiebaFloorReplySpider();
            spider.ThreadNum = 20;
            var postServer = new FloorServer();
            spider.AddFloorIDs(postServer.Select(p => p.UpdateTime > DateTime.Now.AddHours(-12), 1, 20000).Select(p => p.Key));
            return spider.RunAsync();
        }
    }
}
