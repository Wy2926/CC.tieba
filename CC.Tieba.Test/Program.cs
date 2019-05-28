using CC.Tieba.Application;
using CC.Tieba.Reptile;
using CC.Tieba.Reptile.Spider;
using CC.Tieba.TaskDis;
using CC.Tieba.TaskDis.Job;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Tieba.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SchedulerTieba scheduler = new SchedulerTieba();
            scheduler.Start();
            Console.Read();
        }
    }
}
