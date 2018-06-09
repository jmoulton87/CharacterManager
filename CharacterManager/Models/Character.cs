using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public enum CharacterType
    {
        GM,
        Player
    }

    public class Character
    {
        [Key]
        public int CharacterID { get; set; }

        public string CharacterName { get; set; }

        public string UserID { get; set; }

        [ForeignKey("Campaign")]
        public int CampaignID { get; set; }

        public int CarryStrength { get; set; }

        public CharacterType CharacterType { get; set; }


        public virtual Campaign Campaign { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}