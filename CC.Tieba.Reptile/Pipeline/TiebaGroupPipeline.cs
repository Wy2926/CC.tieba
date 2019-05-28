using DotnetSpider.Core;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using DotnetSpider.Core.Pipeline;
using CC.Tieba.EF.Entity;
using CC.Tieba.Application;

namespace CC.Tieba.Reptile.Pipeline
{
    /// <summary>
    /// 贴吧信息数据存储
    /// </summary>
    public class TiebaGroupPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            foreach (var resultItem in resultItems.Where(p => p.ContainsKey("TiebaGroup")))
            {
                List<TiebaGroup> tiebas = resultItem["TiebaGroup"] as List<TiebaGroup>;
                foreach (var tieba in tiebas)
                {
                    //Console.WriteLine($"【{tieba.Ba_Name}】:{tieba.Ba_Desc} 关注:{tieba.Ba_M_Num} 发帖:{tieba.Ba_P_Num}\r\n");
                }
                GroupServer groupServer = new GroupServer();
                //添加到数据库
                groupServer.AddorUpdates(tiebas);
            }
        }
    }
}
