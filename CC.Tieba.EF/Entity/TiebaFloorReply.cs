using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    /// <summary>
    /// 帖子楼层回复
    /// </summary>
    public class TiebaFloorReply: Entity
    {

        /// <summary>
        /// 被回复的帖子ID
        /// </summary>
        [Required]
        public string ThreadID { get; set; }

        /// <summary>
        /// 被回复的楼层ID
        /// </summary>
        [Required]
        public string FloorID { get; set; }

        /// <summary>
        /// 回复人名称
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 回复人昵称
        /// </summary>
        [Required]
        public string UserNickName { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        [Required]
        public DateTime ReplyTime { get; set; }
    }
}
