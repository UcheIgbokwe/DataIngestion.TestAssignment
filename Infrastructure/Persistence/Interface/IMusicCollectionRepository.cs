using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Infrastructure.Persistence.Interface
{
    public interface IMusicCollectionRepository
    {
        Task<bool> Artist(Artist artist);
        Task<bool> ArtistCollection(ArtistCollection artistCollection);
        Task<bool> Collection(Collection collection);
        Task<bool> CollectionMatch(CollectionMatch collectionMatch);
        Task<List<Album>> GetAlbums(int size, int skip);
        List<Artistt> GetArtistts(long Id);
    }
}