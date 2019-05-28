using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Tieba.TaskDis.Job
{
    /// <summary>
    /// 用户信息更新任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class TiebaUserJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
