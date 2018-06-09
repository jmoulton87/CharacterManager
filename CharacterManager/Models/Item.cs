using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [ForeignKey("BaseItem")]
        public int BaseItemID { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double ItemValue { get; set; }

        public int Slot { get; set; }



        public virtual Location Location { get; set; }
        public virtual BaseItem BaseItem { get; set; }
        public virtual ICollection<ItemEnchantment> ItemEnchantments { get; set; }
        //public virtual ICollection<Location> Locations { get; set; }
    }
}