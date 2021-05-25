using System;
using System.Collections.Generic;
using Nest;

namespace Domain.Model
{
    [ElasticsearchType]
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Upc { get; set; }
        public string ReleaseDate { get; set; }
        public string IsCompilation { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public long ArtistId { get; set; }
        public ICollection<Artistt> Artists { get; set; } = new List<Artistt>();

    }

    [ElasticsearchType]
    public class Artistt
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}