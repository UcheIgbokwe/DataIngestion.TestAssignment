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
        public async Task<bool> Artist(Artist artist)
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
                    await _dbContext.Artists.AddAsync(artist);

                    try
                    { 
                        if ( await _dbContext.SaveChangesAsync() > 0)
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

        public async Task<bool> ArtistCollection(ArtistCollection artistCollection)
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
                    await _dbContext.ArtistCollections.AddAsync(artistCollection);

                    try
                    { 
                        if (await _dbContext.SaveChangesAsync() > 0)
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

        public async Task<bool> Collection(Collection collection)
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
                    await _dbContext.Collections.AddAsync(collection);

                    try
                    { 
                        if (await _dbContext.SaveChangesAsync() > 0)
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

        public async Task<bool> CollectionMatch(CollectionMatch collectionMatch)
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
                    await _dbContext.CollectionMatches.AddAsync(collectionMatch);

                    try
                    { 
                        if (await _dbContext.SaveChangesAsync() > 0)
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

        public async Task<List<Album>> GetAlbums(int size, int skip)
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
                                    ImageUrl = !string.IsNullOrEmpty(b.ViewUrl) ? b.ViewUrl : "null",
                                    Upc = !string.IsNullOrEmpty(c.Upc) ? c.Upc : "null",
                                    ReleaseDate = !string.IsNullOrEmpty(b.OriginalReleaseDate) ? b.OriginalReleaseDate : "null",
                                    IsCompilation = !string.IsNullOrEmpty(b.IsCompilation) ? b.IsCompilation : "null",
                                    Label = !string.IsNullOrEmpty(b.LabelStudio) ? b.LabelStudio : "null",
                                    Url = !string.IsNullOrEmpty(b.ArtworkUrl) ? b.ArtworkUrl : "null",
                                    ArtistId = a.ArtistId
                                    
                                }).Skip(skip).Take(size).ToList();

                var newLoad = load.Select( async a => 
                { 
                    var b = a.Artistts = new List<Artistt>(GetArtistts(a.ArtistId));
                });

                await Task.WhenAll(newLoad);

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
                var load = (from d in _dbContext.Artists where d.ArtistId == Id
                            select new Artistt 
                            {
                                Id = d.ArtistId,
                                Name = d.Name
                            }).Distinct().ToList();

                return load;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}