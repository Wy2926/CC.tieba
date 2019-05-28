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
    /// 帖子楼层更新任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class TiebaFloorJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var spider = new TiebaFloorSpider();
            spider.ThreadNum = 20;
            var postServer = new PostServer();
            spider.AddPostIDs(postServer.Select(p => p.UpdateTime > DateTime.Now.AddHours(6), 1, 20000).Select(p => p.Key));
            return spider.RunAsync();
        }
    }
}
