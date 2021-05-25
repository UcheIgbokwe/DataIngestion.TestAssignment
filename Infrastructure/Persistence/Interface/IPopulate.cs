using Domain.Model;

namespace Infrastructure.Persistence.Interface
{
    public interface IPopulate
    {
        ArtistCollection PopulateArtistCollection(string record);
        Artist PopulateArtist(string record);
        Collection PopulateCollection(string record);
        CollectionMatch PopulateCollectionMatch(string record);
    }
}