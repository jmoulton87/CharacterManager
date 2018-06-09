using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public class ItemEnchantment
    {
        [Key]
        public int ItemEnchantmentID { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }

        [ForeignKey("Enchantment")]
        public int EnchantmentID { get; set; }



        public virtual Item Item { get; set; }
        public virtual Enchantment Enchantment { get; set; }
    }
}