using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public  DbSet<Artist> Artists { get; set; }
		public  DbSet<ArtistCollection> ArtistCollections { get; set; }
		public  DbSet<Collection> Collections { get; set; }
		public  DbSet<CollectionMatch> CollectionMatches { get; set; }

    }
}