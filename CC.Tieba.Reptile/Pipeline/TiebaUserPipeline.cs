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
    /// 用户信息数据抽取
    /// </summary>
    public class TiebaUserPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            foreach (var resultItem in resultItems)
            {
                if (!resultItem.ContainsKey("TiebaUser"))
                    continue;
                List<TiebaUser> users = resultItem["TiebaUser"] as List<TiebaUser>;
                foreach (var user in users)
                {
                    Console.WriteLine($"【{user.U_Name}】:{user.U_Nick} 关注贴吧数:{user.PostBar_Num}\r\n");
                }
            }
        }
    }
}
