using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CharacterManager.Models
{
    public class Campaign
    {
        [Key]
        public int CampaignID { get; set; }

        public string CampaignName { get; set; }

        public string JoinLink { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string UserID { get; set; }



        //public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}