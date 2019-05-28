using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    /// <summary>
    /// 贴吧帖子信息
    /// </summary>
    public class TiebaPost: Entity
    {
        /// <summary>
        /// 贴吧帖子标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 帖子正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 发帖人ID
        /// </summary>
        [Required]
        public string UserID { get; set; }

        /// <summary>
        /// 发帖人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发帖人昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        [Required]
        public int ReplyNum { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// 发帖时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }
    }
}
