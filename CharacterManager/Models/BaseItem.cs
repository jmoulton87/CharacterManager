using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public enum ItemType
    {
        currency,
        container,
        containeredim,
        onehandedweapon,
        twohandedweapon,
        rangedweapon,
        armor,
        shield,
        treasure
    }
    public class BaseItem
    {
        [Key]
        public int BaseItemID { get; set; }

        [ForeignKey("Icon")]
        public int? IconID { get; set; }


        public string BaseItemName { get; set; }

        public ItemType BaseItemType { get; set; }

        public double BaseItemValue { get; set; }

        public double BaseItemWeight { get; set; }



        public bool? IsExtraDim { get; set; }

        public double? Capacity { get; set; }


        public virtual Icon Icon { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}