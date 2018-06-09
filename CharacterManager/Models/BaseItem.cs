using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public enum ItemType
    {
        Currency,
        Container,
        EDimContainer,
        MeleeWeapon,
        RangedWeapon,
        Armor,
        Shield,
        Treasure
    }
    public class BaseItem
    {
        [Key]
        public int BaseItemID { get; set; }

        public string BaseItemName { get; set; }

        public ItemType BaseItemType { get; set; }

        public double BaseItemValue { get; set; }

        public double BaseItemWeight { get; set; }

        public double? Capacity { get; set; }



        public virtual ICollection<Item> Items { get; set; }
    }
}