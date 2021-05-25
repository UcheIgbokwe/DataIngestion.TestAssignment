namespace Domain.Model
{
    public class ArtistCollection : EntityBase
    {
		public long ArtistId { get; set; }
        public int RoleId { get; set; }
		public long CollectionId { get; set; }
		public string IsPrimaryArtist { get; set; }
        public string ExportDate { get; set; }
    }
}