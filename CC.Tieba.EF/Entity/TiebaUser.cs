using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    /// <summary>
    /// 贴吧用户信息
    /// </summary>
    public class TiebaUser : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string U_Name { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string U_Nick { get; set; }

        /// <summary>
        /// 用户简介
        /// </summary>
        public string U_Desc { get; set; }

        /// <summary>
        /// 吧龄
        /// </summary>
        public string U_BaAge { get; set; }

        /// <summary>
        ///开帖数
        /// </summary>
        public int Post_Num { get; set; }

        /// <summary>
        /// 发帖数
        /// </summary>
        public int Posting_Num { get; set; }

        /// <summary>
        /// 关注的贴吧数
        /// </summary>
        public int PostBar_Num { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        public int Follow_Num { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int Fans_Num { get; set; }
    }
}
