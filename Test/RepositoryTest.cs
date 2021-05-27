using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Persistence.Interface;
using Moq;
using Xunit;

namespace Test
{
    public class RepositoryTest
    {
        [Fact]
        public void Add_Artist_Collection()
        {
            //Arrange
            var repositoryMock = new Mock<IMusicCollectionRepository>();
            var mockObject = MockObject();

            //Act
            addItem(mockObject);

            //Assert
            repositoryMock.Verify(r  => r.ArtistCollection(mockObject), Times.Never);

        }

        public bool addItem(ArtistCollection artistCollection)
        {
            try
            {
                bool result = true;
                var repositoryMock = new Mock<IMusicCollectionRepository>();
                repositoryMock.Setup(f => f.ArtistCollection(artistCollection)).Returns(Task.FromResult(result));

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public ArtistCollection MockObject()
        {
            var artistCollection = new ArtistCollection
            {
                ExportDate = "2021-06-27",
                ArtistId = 1,
                CollectionId = 1,
                IsPrimaryArtist = "1",
                RoleId = 1
            };
            return artistCollection;
        }
    }
}