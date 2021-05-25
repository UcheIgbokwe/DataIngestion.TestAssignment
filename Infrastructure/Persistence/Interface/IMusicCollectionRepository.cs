using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Infrastructure.Persistence.Interface
{
    public interface IMusicCollectionRepository
    {
        bool Artist(Artist artist);
        bool ArtistCollection(ArtistCollection artistCollection);
        bool Collection(Collection collection);
        bool CollectionMatch(CollectionMatch collectionMatch);
        List<Album> GetAlbums(int size, int skip);
        List<Artistt> GetArtistts(long Id);
    }
}