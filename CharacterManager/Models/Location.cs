using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public enum LocationType
    {
        Equipment,
        Inventory,
        Trade,
        PartyLoot,
        Container,
        EDimContainer
    }
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        public string LocationName { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryID { get; set; }

        public LocationType LocationType { get; set; }

        //[ForeignKey("Item")]
        public int? ItemID { get; set; }



        public virtual Inventory Inventory { get; set; }
        //public virtual Item Item { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}