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
    /// 更新贴吧的帖子信息（任务）
    /// </summary>
    [DisallowConcurrentExecution]
    public class TiebaPostJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var spider = new TiebaPostSpider();
            spider.ThreadNum = 20;
            var postServer = new GroupServer();
            spider.AddTiebaNames(postServer.Select(p => p.Ba_M_Num > 10000, 1, 20000).Select(p => p.Ba_Name));
            return spider.RunAsync();
        }
    }
}
