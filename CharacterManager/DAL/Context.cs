using CharacterManager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CharacterManager.DAL
{
    public class Context : DbContext
    {
        public Context() : base("CharacterManager")
        {
        }

        //NEW MODELS GO HERE
        //USE THIS FORMat
        //public DbSet<[[Model]]> [[Models]] { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<BaseItem> BaseItems { get; set; }
        public DbSet<ItemEnchantment> ItemEnchanments { get; set; }
        public DbSet<Enchantment> Enchantments { get; set; }
        public DbSet<Icon> Icons { get; set; }

        //NEW MODELS END HERE

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //public System.Data.Entity.DbSet<CharacterManager.Models.Character> Characters { get; set; }
    }
}