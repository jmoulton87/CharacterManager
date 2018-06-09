using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterManager.DAL
{
    public class UnitOfWork : IDisposable
    {
        private Context context = new Context();

        //-------------------------------------------------------------
        //-------------------------------------------------------------
        //NEW MODELS GO HERE
        //-------------------------------------------------------------
        ////USE THIS FORMAT
        ////private Repository<[[Model]]> [[model]]Repository;
        //-------------------------------------------------------------

        private Repository<Campaign> campaignRepository;
        private Repository<Character> characterRepository;
        private Repository<Inventory> inventoryRepository;
        private Repository<Location> locationRepository;
        private Repository<Item> itemRepository;
        private Repository<BaseItem> baseItemRepository;
        private Repository<ItemEnchantment> itemEnchantmentRepository;
        private Repository<Enchantment> enchantmentRepository;

        //NEW MODELS END HERE
        //-------------------------------------------------------------
        //-------------------------------------------------------------


        //-------------------------------------------------------------
        //-------------------------------------------------------------
        //NEW MODELS GO HERE
        //-------------------------------------------------------------
        ////USE THIS FORMAT
        ////public Repository<[[Model]]> [[Model]]Repository
        ////{
        ////    get
        ////    {
        ////        if (this.[[model]]Repository == null)
        ////        {
        ////            this.[[model]]Repository = new Repository<[[Model]]>(context);
        ////        }
        ////        return [[model]]Repository;
        ////    }
        ////}
        //-------------------------------------------------------------

        public Repository<Campaign> CampaignRepository
        {
            get
            {
                if (this.campaignRepository == null)
                {
                    this.campaignRepository = new Repository<Campaign>(context);
                }
                return campaignRepository;
            }
        }

        public Repository<Character> CharacterRepository
        {
            get
            {
                if (this.characterRepository == null)
                {
                    this.characterRepository = new Repository<Character>(context);
                }
                return characterRepository;
            }
        }

        public Repository<Inventory> InventoryRepository
        {
            get
            {
                if (this.inventoryRepository == null)
                {
                    this.inventoryRepository = new Repository<Inventory>(context);
                }
                return inventoryRepository;
            }
        }

        public Repository<Location> LocationRepository
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new Repository<Location>(context);
                }
                return locationRepository;
            }
        }

        public Repository<Item> ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository = new Repository<Item>(context);
                }
                return itemRepository;
            }
        }

        public Repository<BaseItem> BaseItemRepository
        {
            get
            {
                if (this.baseItemRepository == null)
                {
                    this.baseItemRepository = new Repository<BaseItem>(context);
                }
                return baseItemRepository;
            }
        }

        public Repository<ItemEnchantment> ItemEnchantmentRepository
        {
            get
            {
                if (this.itemEnchantmentRepository == null)
                {
                    this.itemEnchantmentRepository = new Repository<ItemEnchantment>(context);
                }
                return itemEnchantmentRepository;
            }
        }

        public Repository<Enchantment> EnchantmentRepository
        {
            get
            {
                if (this.enchantmentRepository == null)
                {
                    this.enchantmentRepository = new Repository<Enchantment>(context);
                }
                return enchantmentRepository;
            }
        }

        //NEW MODELS END HERE
        //-------------------------------------------------------------
        //-------------------------------------------------------------

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}