using CC.Tieba.Application;
using CC.Tieba.Reptile.Pipeline;
using CC.Tieba.Reptile.Spider;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace CC.Tieba.TaskDis.Job
{
    /// <summary>
    /// 更新贴吧的吧信息（任务）
    /// </summary>
    [DisallowConcurrentExecution]
    public class TiebaGroupJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var spider = new TiebaGroupSpider(false);
            spider.ThreadNum = 20;
            var groupServer = new GroupServer();
            //获取数据库里所有的贴吧名称进行更新
            spider.AddTiebaNames(groupServer.SelectOrderByDescTime().Select(p => p.Ba_Name));
            return spider.RunAsync();
        }
    }
}
