using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class CollectionMatch : EntityBase
    {
        public string ExportDate { get; set; }

        [ForeignKey("ArtistCollection")]
		public int CollectionId { get; set; }
		public string Upc { get; set; }
		public string Grid { get; set; }
		public string AmgAlbumId { get; set; }
    }
}