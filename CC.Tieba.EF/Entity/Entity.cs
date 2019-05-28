using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Tieba.EF.Entity
{
    public class Entity
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        [Key]
        public string Key { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        public DateTime UpdateTime { get; set; }
    }
}
