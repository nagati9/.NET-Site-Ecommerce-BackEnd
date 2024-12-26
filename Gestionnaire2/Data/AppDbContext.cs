using Gestionnaire2.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Ajoutez vos DbSet ici (exemple pour une table Users)
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Models.Test> Tests { get; set; }
        public DbSet<Models.MarqueParfum> MarqueParfums { get; set; }
        public DbSet<Models.MarqueSkincare> MarqueSkincares { get; set; }
        public DbSet<Models.MarqueVetement> MarqueVetements { get; set; }
        public DbSet<Models.TypeParfum> TypeParfums { get; set; }
        public DbSet<Models.TypeSkincare> TypeSkincare { get; set; }
        public DbSet<Models.TypeVetement> TypeVetements { get; set; }
        public DbSet<Models.Utilisateur> utilisateurs { get; set; }
        public DbSet<Models.Panier> paniers { get; set; }
        public DbSet<Models.PanierProduit> paniersproduits { get; set; }
      


    }
}
