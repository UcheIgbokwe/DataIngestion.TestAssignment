using System.Collections.Generic;
using Domain.Model;
using MediatR;
using Nest;

namespace Application.Features.GoogleDrive.Commands.ElasticSearch
{
    public class ElasticSearchCommand : MediatR.IRequest<BulkResponse>
    {
        public IEnumerable<Album> Library { get; set; }
    }
}