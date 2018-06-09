using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public enum EnchantmentType
    {
        Melee = 1,
        Ranged = 2,
        Ammo = 3,
        Armor = 4,
        Shield = 5
    }
    public class Enchantment
    {
        [Key]
        public int EnchantmentID { get; set; }

        public string EnchantmentName { get; set; }

        public string EnchantmentDesc { get; set; }

        public EnchantmentType EnchantmentType { get; set; }

        public int EnchantmentBonus { get; set; }

        public bool IsEnhancement { get; set; }



        public virtual ICollection<ItemEnchantment> ItemEnchantments { get; set; }
    }
}