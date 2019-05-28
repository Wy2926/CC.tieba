using CC.Tieba.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using CC.Tieba.Lib;
using System.Threading;
using System.Linq;
namespace CC.Tieba.Application
{
    public class DBHelp
    {
        static Dictionary<Thread, TiebaDbContext> KeyValuePairs = new Dictionary<Thread, TiebaDbContext>();
        /// <summary>
        /// 获取EF唯一上下文对象
        /// </summary>
        /// <returns></returns>
        public static TiebaDbContext GetEFCodeFirstDbContext()
        {
            if (!KeyValuePairs.ContainsKey(Thread.CurrentThread))
            {
                KeyValuePairs[Thread.CurrentThread] = new TiebaDbContext();
            }
            return KeyValuePairs[Thread.CurrentThread];
            TiebaDbContext dbContext = KeyValuePairs[Thread.CurrentThread];
            if (dbContext == null)
            {
                dbContext = new TiebaDbContext();
                CallContext.SetData("dbContext", dbContext);

            }
            return dbContext;
        }
    }
}
