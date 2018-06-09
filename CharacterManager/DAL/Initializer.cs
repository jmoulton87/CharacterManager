using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CharacterManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CharacterManager.DAL
{
    public class Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

            ApplicationDbContext context2 = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context2));
            var gamemaster = UserManager.FindByEmail("gamemaster@email.com").Id;
            var player1 = UserManager.FindByEmail("player1@email.com").Id;
            var player2 = UserManager.FindByEmail("player2@email.com").Id;
            var player3 = UserManager.FindByEmail("player3@email.com").Id;
            var player4 = UserManager.FindByEmail("player4@email.com").Id;



            var campaigns = new List<Campaign>
            {
                new Campaign{ CampaignID=1, CampaignName="The Dark Heart of Ustalav", UserID=gamemaster, JoinLink="ea68d57e" }
            };
            campaigns.ForEach(campaign => context.Campaigns.Add(campaign));
            context.SaveChanges();



            var characters = new List<Character>
            {
                new Character{ CharacterID = 1, CharacterName = "Game Master", UserID = gamemaster, CampaignID = 1, CarryStrength = 1, CharacterType = (CharacterType)0 },
                new Character{ CharacterID = 2, CharacterName = "Atlas", UserID = player1, CampaignID = 1, CarryStrength = 18, CharacterType = (CharacterType)1 },
                new Character{ CharacterID = 3, CharacterName = "Leala", UserID = player2, CampaignID = 1, CarryStrength = 15, CharacterType = (CharacterType)1 },
                new Character{ CharacterID = 4, CharacterName = "Varen", UserID = player2, CampaignID = 1, CarryStrength = 13, CharacterType = (CharacterType)1 },
                new Character{ CharacterID = 5, CharacterName = "Garrett", UserID = player4, CampaignID = 1, CarryStrength = 17, CharacterType = (CharacterType)1 }

            };
            characters.ForEach(character => context.Characters.Add(character));
            context.SaveChanges();



            var inventories = new List<Inventory>
            {
                new Inventory{ InventoryID=1, InventoryName="Dungeon Master's Inventory", CharacterID=1 },
                new Inventory{ InventoryID=2, InventoryName="Atlas's Inventory", CharacterID=2 },
                new Inventory{ InventoryID=3, InventoryName="Leala's Inventory", CharacterID=3 },
                new Inventory{ InventoryID=4, InventoryName="Varen's Inventory", CharacterID=4 },
                new Inventory{ InventoryID=5, InventoryName="Garrett's Inventory", CharacterID=5 }
            };
            inventories.ForEach(inventory => context.Inventories.Add(inventory));
            context.SaveChanges();



            var locations = new List<Location>
            {
                //game master locations
                new Location{ LocationID = 1, InventoryID = 1, LocationType = (LocationType)1, LocationName = "Inventory" },
                new Location{ LocationID = 2, InventoryID = 1, LocationType = (LocationType)2, LocationName = "Trade" },
                new Location{ LocationID = 3, InventoryID = 1, LocationType = (LocationType)3, LocationName = "Party Loot" },
                //player 1 locations
                new Location{ LocationID = 4, InventoryID = 2, LocationType = (LocationType)0, LocationName = "Equipment" },
                new Location{ LocationID = 5, InventoryID = 2, LocationType = (LocationType)1, LocationName = "Inventory" },
                new Location{ LocationID = 6, InventoryID = 2, LocationType = (LocationType)2, LocationName = "Trade" },
                new Location{ LocationID = 16, InventoryID = 2, LocationType = (LocationType)4, LocationName = "Backpack", ItemID = 9 },
                new Location{ LocationID = 17, InventoryID = 2, LocationType = (LocationType)5, LocationName = "Handy Haversack", ItemID = 14 },

                //player 2 locations
                new Location{ LocationID = 7, InventoryID = 3, LocationType = (LocationType)0, LocationName = "Equipment" },
                new Location{ LocationID = 8, InventoryID = 3, LocationType = (LocationType)1, LocationName = "Inventory" },
                new Location{ LocationID = 9, InventoryID = 3, LocationType = (LocationType)2, LocationName = "Trade" },
                //player 3 locations
                new Location{ LocationID = 10, InventoryID = 4, LocationType = (LocationType)0, LocationName = "Equipment" },
                new Location{ LocationID = 11, InventoryID = 4, LocationType = (LocationType)1, LocationName = "Inventory" },
                new Location{ LocationID = 12, InventoryID = 4, LocationType = (LocationType)2, LocationName = "Trade" },
                //player 4 locations
                new Location{ LocationID = 13, InventoryID = 5, LocationType = (LocationType)0, LocationName = "Equipment" },
                new Location{ LocationID = 14, InventoryID = 5, LocationType = (LocationType)1, LocationName = "Inventory" },
                new Location{ LocationID = 15, InventoryID = 5, LocationType = (LocationType)2, LocationName = "Trade" },
            };
            locations.ForEach(location => context.Locations.Add(location));
            context.SaveChanges();



            var baseItems = new List<BaseItem>
            {
                new BaseItem{ BaseItemID = 1, BaseItemName = "Copper Coin", BaseItemType = (ItemType)0, BaseItemValue = 0.01, BaseItemWeight = 0.02},
                new BaseItem{ BaseItemID = 2, BaseItemName = "Silver Coin", BaseItemType = (ItemType)0, BaseItemValue = 0.1, BaseItemWeight = 0.02},
                new BaseItem{ BaseItemID = 3, BaseItemName = "Gold Coin", BaseItemType = (ItemType)0, BaseItemValue = 1, BaseItemWeight = 0.02},
                new BaseItem{ BaseItemID = 4, BaseItemName = "Platinum Coin", BaseItemType = (ItemType)0, BaseItemValue = 10, BaseItemWeight = 0.02},

                new BaseItem{ BaseItemID = 5, BaseItemName = "Backpack", BaseItemType = (ItemType)1, BaseItemValue = 2, BaseItemWeight = 2 },
                new BaseItem{ BaseItemID = 6, BaseItemName = "Handy Haversack", BaseItemType = (ItemType)2, BaseItemValue = 2000, BaseItemWeight = 5, Capacity = 120},

                new BaseItem{ BaseItemID = 7, BaseItemName = "Longsword", BaseItemType = (ItemType)3, BaseItemValue = 15, BaseItemWeight = 4},
                new BaseItem{ BaseItemID = 8, BaseItemName = "Scale Mail", BaseItemType = (ItemType)5, BaseItemValue = 50, BaseItemWeight = 30},
                new BaseItem{ BaseItemID = 9, BaseItemName = "Heavy Wooden Shield", BaseItemType = (ItemType)6, BaseItemValue = 7, BaseItemWeight = 10},
                new BaseItem{ BaseItemID = 10, BaseItemName = "Ruby", BaseItemType = (ItemType)7, BaseItemValue = 500, BaseItemWeight = 0},



            };
            baseItems.ForEach(baseItem => context.BaseItems.Add(baseItem));
            context.SaveChanges();



            var items = new List<Item>
            {
                new Item{ ItemID = 1, BaseItemID = 1, LocationID = 3, ItemName = "Copper Coin(s)", Quantity = 100, ItemValue = 0.01, Slot=1},
                new Item{ ItemID = 2, BaseItemID = 2, LocationID = 3, ItemName = "Silver Coin(s)", Quantity = 100, ItemValue = 0.1, Slot=3},
                new Item{ ItemID = 3, BaseItemID = 3, LocationID = 3, ItemName = "Gold Coin(s)", Quantity = 100, ItemValue = 1, Slot=4},
                new Item{ ItemID = 4, BaseItemID = 4, LocationID = 3, ItemName = "Platinum Coin(s)", Quantity = 100, ItemValue = 100, Slot=7},

                new Item{ ItemID = 5, BaseItemID = 1, LocationID = 5, ItemName = "Copper Coin(s)", Quantity = 100, ItemValue = 0.01, Slot=1},
                new Item{ ItemID = 6, BaseItemID = 2, LocationID = 5, ItemName = "Silver Coin(s)", Quantity = 100, ItemValue = 0.1, Slot=3},
                new Item{ ItemID = 7, BaseItemID = 3, LocationID = 5, ItemName = "Gold Coin(s)", Quantity = 100, ItemValue = 1, Slot=4},
                new Item{ ItemID = 8, BaseItemID = 4, LocationID = 5, ItemName = "Platinum Coin(s)", Quantity = 100, ItemValue = 100, Slot=7},


                new Item{ ItemID = 9, BaseItemID = 5, LocationID = 5, ItemName = "Backpack", Quantity = 1, ItemValue = 2, Slot=2},

                new Item{ ItemID = 10, BaseItemID = 1, LocationID = 16, ItemName = "Copper Coin(s)", Quantity = 25, ItemValue = 0.01, Slot=1},
                new Item{ ItemID = 11, BaseItemID = 2, LocationID = 16, ItemName = "Silver Coin(s)", Quantity = 25, ItemValue = 0.1, Slot=2},
                new Item{ ItemID = 12, BaseItemID = 3, LocationID = 16, ItemName = "Gold Coin(s)", Quantity = 50, ItemValue = 1, Slot=4},
                new Item{ ItemID = 13, BaseItemID = 4, LocationID = 16, ItemName = "Platinum Coin(s)", Quantity = 1000, ItemValue = 100, Slot=6},

                new Item{ ItemID = 14, BaseItemID = 6, LocationID = 5, ItemName = "Handy Haversack", Quantity = 1, ItemValue = 2000, Slot = 8},

                new Item{ ItemID = 15, BaseItemID = 7, LocationID = 3, ItemName = "Longsword", Quantity = 1, ItemValue = 15, Slot = 10},
                new Item{ ItemID = 16, BaseItemID = 8, LocationID = 3, ItemName = "Scale Mail", Quantity = 1, ItemValue = 50, Slot = 11},
                new Item{ ItemID = 17, BaseItemID = 9, LocationID = 3, ItemName = "Heavy Wooden Shield", Quantity = 1, ItemValue = 7, Slot = 12},
                new Item{ ItemID = 18, BaseItemID = 10, LocationID = 3, ItemName = "Ruby", Quantity = 1, ItemValue = 500, Slot = 13},

            };
            items.ForEach(item => context.Items.Add(item));
            context.SaveChanges();

























            var enchantments = new List<Enchantment>
            {
                new Enchantment{ EnchantmentType=(EnchantmentType)1, EnchantmentName="+1 Enhancement", IsEnhancement=true, EnchantmentBonus=1, EnchantmentDesc="A melee weapon with a +1 Enhancement enchanment gains +1 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)1, EnchantmentName="+2 Enhancement", IsEnhancement=true, EnchantmentBonus=2, EnchantmentDesc="A melee weapon with a +2 Enhancement enchanment gains +2 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)1, EnchantmentName="+3 Enhancement", IsEnhancement=true, EnchantmentBonus=3, EnchantmentDesc="A melee weapon with a +3 Enhancement enchanment gains +3 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)1, EnchantmentName="+4 Enhancement", IsEnhancement=true, EnchantmentBonus=4, EnchantmentDesc="A melee weapon with a +4 Enhancement enchanment gains +4 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)1, EnchantmentName="+5 Enhancement", IsEnhancement=true, EnchantmentBonus=5, EnchantmentDesc="A melee weapon with a +5 Enhancement enchanment gains +5 to Attack and Damage rolls." },

                new Enchantment{ EnchantmentType=(EnchantmentType)2, EnchantmentName="+1 Enhancement", IsEnhancement=true, EnchantmentBonus=1, EnchantmentDesc="A ranged weapon with a +1 Enhancement enchanment gains +1 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)2, EnchantmentName="+2 Enhancement", IsEnhancement=true, EnchantmentBonus=2, EnchantmentDesc="A ranged weapon with a +2 Enhancement enchanment gains +2 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)2, EnchantmentName="+3 Enhancement", IsEnhancement=true, EnchantmentBonus=3, EnchantmentDesc="A ranged weapon with a +3 Enhancement enchanment gains +3 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)2, EnchantmentName="+4 Enhancement", IsEnhancement=true, EnchantmentBonus=4, EnchantmentDesc="A ranged weapon with a +4 Enhancement enchanment gains +4 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)2, EnchantmentName="+5 Enhancement", IsEnhancement=true, EnchantmentBonus=5, EnchantmentDesc="A ranged weapon with a +5 Enhancement enchanment gains +5 to Attack and Damage rolls." },

                new Enchantment{ EnchantmentType=(EnchantmentType)3, EnchantmentName="+1 Enhancement", IsEnhancement=true, EnchantmentBonus=1, EnchantmentDesc="Ammunition with a +1 Enhancement enchanment gains +1 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)3, EnchantmentName="+2 Enhancement", IsEnhancement=true, EnchantmentBonus=2, EnchantmentDesc="Ammunition with a +2 Enhancement enchanment gains +2 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)3, EnchantmentName="+3 Enhancement", IsEnhancement=true, EnchantmentBonus=3, EnchantmentDesc="Ammunition with a +3 Enhancement enchanment gains +3 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)3, EnchantmentName="+4 Enhancement", IsEnhancement=true, EnchantmentBonus=4, EnchantmentDesc="Ammunition with a +4 Enhancement enchanment gains +4 to Attack and Damage rolls." },
                new Enchantment{ EnchantmentType=(EnchantmentType)3, EnchantmentName="+5 Enhancement", IsEnhancement=true, EnchantmentBonus=5, EnchantmentDesc="Ammunition with a +5 Enhancement enchanment gains +5 to Attack and Damage rolls." },

                new Enchantment{ EnchantmentType=(EnchantmentType)4, EnchantmentName="+1 Enhancement", IsEnhancement=true, EnchantmentBonus=1, EnchantmentDesc="Armor with a +1 Enhancement enchanment gains +1 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)4, EnchantmentName="+2 Enhancement", IsEnhancement=true, EnchantmentBonus=2, EnchantmentDesc="Armor with a +2 Enhancement enchanment gains +2 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)4, EnchantmentName="+3 Enhancement", IsEnhancement=true, EnchantmentBonus=3, EnchantmentDesc="Armor with a +3 Enhancement enchanment gains +3 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)4, EnchantmentName="+4 Enhancement", IsEnhancement=true, EnchantmentBonus=4, EnchantmentDesc="Armor with a +4 Enhancement enchanment gains +4 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)4, EnchantmentName="+5 Enhancement", IsEnhancement=true, EnchantmentBonus=5, EnchantmentDesc="Armor with a +5 Enhancement enchanment gains +5 to AC." },

                new Enchantment{ EnchantmentType=(EnchantmentType)5, EnchantmentName="+1 Enhancement", IsEnhancement=true, EnchantmentBonus=1, EnchantmentDesc="A shield with a +1 Enhancement enchanment gains +1 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)5, EnchantmentName="+2 Enhancement", IsEnhancement=true, EnchantmentBonus=2, EnchantmentDesc="A shield with a +2 Enhancement enchanment gains +2 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)5, EnchantmentName="+3 Enhancement", IsEnhancement=true, EnchantmentBonus=3, EnchantmentDesc="A shield with a +3 Enhancement enchanment gains +3 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)5, EnchantmentName="+4 Enhancement", IsEnhancement=true, EnchantmentBonus=4, EnchantmentDesc="A shield with a +4 Enhancement enchanment gains +4 to AC." },
                new Enchantment{ EnchantmentType=(EnchantmentType)5, EnchantmentName="+5 Enhancement", IsEnhancement=true, EnchantmentBonus=5, EnchantmentDesc="A shield with a +5 Enhancement enchanment gains +5 to AC." }
            };
            enchantments.ForEach(enchantment => context.Enchantments.Add(enchantment));
            context.SaveChanges();

        }
    }
}