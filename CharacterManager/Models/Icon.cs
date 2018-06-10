using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public class Icon
    {
        [Key]
        public int IconID { get; set; }
        public string IconName { get; set; }
        public string IconSheet { get; set; }
        public string IconPosition { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<BaseItem> BaseItems { get; set; }
    }
}