using CC.Tieba.EF;
using CC.Tieba.EF.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace CC.Tieba.Application
{
    /// <summary>
    /// 用户应用
    /// </summary>
    public class UserServer : AbServer<TiebaUser>
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TiebaUser GetUser(string key)
        {
            return Entities.Where(p => p.Key == key).FirstOrDefault();
        }
    }
}
