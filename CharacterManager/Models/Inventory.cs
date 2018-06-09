using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        public string InventoryName { get; set; }

        [ForeignKey("Character")]
        public int CharacterID { get; set; }



        public virtual Character Character { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}