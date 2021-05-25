using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using MediatR;
using Nest;

namespace Application.Features.GoogleDrive.Commands.ElasticSearch
{
    public class ElasticSearchCommandHandler : IRequestHandler<ElasticSearchCommand, BulkResponse>
    {
        private readonly ElasticClient _client;
        public ElasticSearchCommandHandler(ElasticClient client)
        {
            _client = client;

        }
        public async Task<BulkResponse> Handle(ElasticSearchCommand request, CancellationToken cancellationToken)
        {
            var bulkResponse = new BulkResponse();
            var response = request.Library.Select(x => new BulkIndexOperation<Album>(x))
                                            .Cast<IBulkOperation>().ToList();
            
            var bulkRequest = new BulkRequest()
            {
                Refresh = new Elasticsearch.Net.Refresh(),
                Operations = response
            };

            return await _client.BulkAsync(bulkRequest, cancellationToken);
        }
    }
}