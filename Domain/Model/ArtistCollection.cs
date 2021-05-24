namespace Domain.Model
{
    public class ArtistCollection : EntityBase
    {
		public int ArtistId { get; set; }
        public int RoleId { get; set; }
		public int CollectionId { get; set; }
		public string IsPrimaryArtist { get; set; }
        public string ExportDate { get; set; }
    }
}