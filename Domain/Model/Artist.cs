using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Artist : EntityBase
    {
        public string ExportDate { get; set; }

        [ForeignKey("ArtistCollection")]
		public long ArtistId { get; set; }
		public string Name { get; set; }
		public string IsActualArtist { get; set; }
		public string ViewUrl { get; set; }
		public string ArtistTypeId { get; set; }
        
    }
}