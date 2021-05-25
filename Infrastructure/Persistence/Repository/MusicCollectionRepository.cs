using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Repository
{
    public class MusicCollectionRepository : IMusicCollectionRepository
    {
        private readonly ILogger<MusicCollectionRepository> _logger;
        private readonly AppDbContext _dbContext;
        public MusicCollectionRepository(ILogger<MusicCollectionRepository> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;

        }
        public bool Artist(Artist artist)
        {
            try
            {
                if(artist != null)
                {
                    if( _dbContext.Artists.Any(c => c.ArtistId == artist.ArtistId 
                                                    && c.ArtistTypeId == artist.ArtistTypeId
                                                    && c.Name == artist.Name))
                    {
                        return false;
                    }
                    _dbContext.Artists.Add(artist);

                    try
                    { 
                        if (_dbContext.SaveChanges() > 0)
                        {
                            _logger.LogInformation($"{artist.ArtistId} has been saved.");
                            return true;
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        _logger.LogError(ex.Message);
                        return false;
                    }
                }
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool ArtistCollection(ArtistCollection artistCollection)
        {
            try
            {
                if(artistCollection != null)
                {
                    if( _dbContext.ArtistCollections.Any(c => c.ArtistId == artistCollection.ArtistId 
                                                            && c.CollectionId == artistCollection.CollectionId
                                                            && c.RoleId == artistCollection.RoleId))
                    {
                        return false;
                    }
                    _dbContext.ArtistCollections.Add(artistCollection);

                    try
                    { 
                        if (_dbContext.SaveChanges() > 0)
                        {
                            _logger.LogInformation($"{artistCollection.ArtistId} has been saved.");
                            return true;
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        _logger.LogError(ex.Message);
                        return false;
                    }
                }
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool Collection(Collection collection)
        {
            try
            {
                if(collection != null)
                {
                    if( _dbContext.Collections.Any(c => c.CollectionTypeId == collection.CollectionTypeId 
                                                        && c.CollectionId == collection.CollectionId
                                                        && c.Name == collection.Name))
                    {
                        return false;
                    }
                    _dbContext.Collections.Add(collection);

                    try
                    { 
                        if (_dbContext.SaveChanges() > 0)
                        {
                            _logger.LogInformation($"{collection.CollectionId} has been saved.");
                            return true;
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        _logger.LogError(ex.Message);
                        return false;
                    }
                }
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool CollectionMatch(CollectionMatch collectionMatch)
        {
            try
            {
                if(collectionMatch != null)
                {
                    if( _dbContext.CollectionMatches.Any(c => c.AmgAlbumId == collectionMatch.AmgAlbumId 
                                                            && c.CollectionId == collectionMatch.CollectionId
                                                            && c.Grid == collectionMatch.Grid))
                    {
                        return false;
                    }
                    _dbContext.CollectionMatches.Add(collectionMatch);

                    try
                    { 
                        if (_dbContext.SaveChanges() > 0)
                        {
                            _logger.LogInformation($"{collectionMatch.CollectionId} has been saved.");
                            return true;
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        _logger.LogError(ex.Message);
                        return false;
                    }
                }
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public List<Album> GetAlbums(int size, int skip)
        {
            try
            {
                var load = (from a in _dbContext.ArtistCollections
                                join b in _dbContext.Collections on a.CollectionId equals b.CollectionId
                                join c in _dbContext.CollectionMatches on a.CollectionId equals c.CollectionId
                                select new Album
                                {
                                    Id = b.CollectionId,
                                    Name = b.Name,
                                    ImageUrl = b.ViewUrl,
                                    Upc = c.Upc,
                                    ReleaseDate = b.OriginalReleaseDate,
                                    IsCompilation = b.IsCompilation,
                                    Label = b.LabelStudio,
                                    Url = b.ArtworkUrl,
                                    ArtistId = a.ArtistId
                                }).Skip(skip).Take(size).ToList();
                
                var refinedLoad = load.Select(d => d.Artists = new List<Artistt>(GetArtistts(d.ArtistId)));
                return load;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public List<Artistt> GetArtistts(long Id)
        {
            try
            {
                return _dbContext.Artists.Where(c => c.ArtistId == Id).Select(p => new Artistt { Id = p.ArtistId, Name = p.Name}).Distinct().ToList();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}