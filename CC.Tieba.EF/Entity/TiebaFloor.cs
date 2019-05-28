using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    /// <summary>
    /// 帖子楼层信息
    /// </summary>
    public class TiebaFloor : Entity
    {
        /// <summary>
        /// 贴吧ID
        /// </summary>
        [Required]
        public string ForumID { get; set; }

        /// <summary>
        /// 被评论帖子ID
        /// </summary>
        [Required]
        public string ThreadID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        public string FloorBody { get; set; }

        /// <summary>
        /// 评论人ID
        /// </summary>
        [Required]
        public string UserID { get; set; }

        /// <summary>
        /// 评论人名称
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 评论人昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 楼层索引
        /// </summary>
        [Required]
        public int PostIndex { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [Required]
        public DateTime CommentTime { get; set; }
    }
}
