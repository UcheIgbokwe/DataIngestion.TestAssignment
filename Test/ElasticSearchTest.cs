using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Moq;
using Nest;
using Xunit;

namespace Test
{
    public class ElasticSearchTest
    {
        [Fact]
        public void Create_Elastic_Search_Object()
        {
            var albums = AllAlbum().ToList();
            var hits = new List<IHit<Album>>
            {
                new Mock<IHit<Album>>().Object
            };

            var mockSearchResponse = new Mock<ISearchResponse<Album>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(albums);
            mockSearchResponse.Setup(x => x.Hits).Returns(hits);

            var mockElasticClient = new Mock<IElasticClient>();

            mockElasticClient.Setup(x => x
                .Search(It.IsAny<Func<SearchDescriptor<Album>, ISearchRequest>>()))
            .Returns(mockSearchResponse.Object);

            var rq = new IndexRequest<Album>("albums", "config");
            var r2 = mockElasticClient.Object.Index(new Album(), null);
            var result = mockElasticClient.Object.Search<Album>(s => s);

            Assert.Equal(3, result.Documents.Count);
            Assert.Equal(1, result.Hits.Count);
        }

        public List<Album> AllAlbum()
        {
            var load = new  List<Album>()
            {
                new Album
                {
                    Id = 1, 
                    Name = "Uche",
                    Label = "YMCMB", 
                    Upc = "NULL", 
                    Url = "www.applemusic.com", 
                    ImageUrl = "http://foo", 
                    IsCompilation = "1", 
                    ReleaseDate = "2020-05-26", 
                    Artistts = new List<Artistt>()
                    {
                        new Artistt 
                        {
                            Id = 1, 
                            Name = "Uche"
                        }
                    }
                },
                
                new Album
                {
                    Id = 2, 
                    Name = "Uche",
                    Label = "YMCMB", 
                    Upc = "NULL", 
                    Url = "www.applemusic.com", 
                    ImageUrl = "http://foo", 
                    IsCompilation = "1", 
                    ReleaseDate = "2020-05-26", 
                    Artistts = new List<Artistt>()
                    {
                        new Artistt 
                        {
                            Id = 2, 
                            Name = "Uche"
                        }
                    }
                },

                new Album
                {
                    Id = 3, 
                    Name = "Uche",
                    Label = "YMCMB", 
                    Upc = "NULL", 
                    Url = "www.applemusic.com", 
                    ImageUrl = "http://foo", 
                    IsCompilation = "1", 
                    ReleaseDate = "2020-05-26", 
                    Artistts = new List<Artistt>()
                    {
                        new Artistt 
                        {
                            Id = 3, 
                            Name = "Uche"
                        }
                    }
                }
            };
            return load;
        }
    }
}