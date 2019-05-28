using CC.Tieba.TaskDis.Job;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Tieba.TaskDis
{
    public class SchedulerTieba
    {
        //调度器工厂
        ISchedulerFactory factory = new StdSchedulerFactory();
        //调度器
        IScheduler scheduler;
        public SchedulerTieba()
        {
            //创建一个调度器
            scheduler = factory.GetScheduler().Result;

            //创建更新吧的任务
            IJobDetail job = JobBuilder.Create<TiebaGroupJob>().WithIdentity("TiebaGroupJob", "group1").Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("_TiebaGroupJob", "group1")
                .WithCronSchedule("0 0 0 * * ? *")     //每天0点0分0秒执行一次
                .Build();
            //将任务与触发器添加到调度器中
            scheduler.ScheduleJob(job, trigger);

            //创建更新吧的任务
            job = JobBuilder.Create<TiebaPostJob>().WithIdentity("TiebaPostJob", "group1").Build();
            trigger = TriggerBuilder.Create()
               .WithIdentity("_TiebaPostJob", "group1")
               .WithCronSchedule("* * 0/1 * * ? *")     //每小时执行一次
               .Build();
            //将任务与触发器添加到调度器中
            scheduler.ScheduleJob(job, trigger);

            //创建更新吧的任务
            job = JobBuilder.Create<TiebaFloorJob>().WithIdentity("TiebaFloorJob", "group1").Build();
            trigger = TriggerBuilder.Create()
               .WithIdentity("_TiebaFloorJob", "group1")
               .WithCronSchedule("* * 0/3 * * ? *")     //每3时执行一次
               .Build();
            //将任务与触发器添加到调度器中
            scheduler.ScheduleJob(job, trigger);

            //创建更新吧的任务
            job = JobBuilder.Create<TiebaFloorReplyJob>().WithIdentity("TiebaFloorReplyJob", "group1").Build();
            trigger = TriggerBuilder.Create()
               .WithIdentity("_TiebaFloorReplyJob", "group1")
               .WithCronSchedule("* * 0/6 * * ? *")     //每6时执行一次
               .Build();
            //将任务与触发器添加到调度器中
            scheduler.ScheduleJob(job, trigger);
        }

        public void Start()
        {
            scheduler.Start();
        }
    }
}
