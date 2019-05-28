using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    /// <summary>
    /// 贴吧信息实体
    /// </summary>
    public class TiebaGroup: Entity
    {
        /// <summary>
        /// 贴吧名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Ba_Name { get; set; }

        /// <summary>
        /// 贴吧简介
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Ba_Desc { get; set; }

        /// <summary>
        /// 贴吧关注数
        /// </summary>
        [Required]
        public int Ba_M_Num { get; set; }

        /// <summary>
        /// 贴吧发帖数
        /// </summary>
        [Required]
        public int Ba_P_Num { get; set; }

        /// <summary>
        /// 贴吧封面图
        /// </summary>
        public string Ba_Pic { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public string FirstClassIfication { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public string TwoClassIfication { get; set; }
    }
}
