using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using MediatR;
using Nest;

namespace Application.Features.GoogleDrive.Commands.ElasticSearch
{
    public class ElasticSearchCommandHandler : IRequestHandler<ElasticSearchCommand, bool>
    {
        private readonly ElasticClient _client;
        public ElasticSearchCommandHandler(ElasticClient client)
        {
            _client = client;

        }
        public async Task<bool> Handle(ElasticSearchCommand request, CancellationToken cancellationToken)
        {

            try
            {
                 var bag = new ConcurrentBag<object>();
                var load = request.Library.Select(async x => {
                    var assignLoad = await _client.IndexDocumentAsync(x);
                    bag.Add(assignLoad);
                });
                await Task.WhenAll(load);

                return true;
            }
            catch (System.Exception ex)
            {
                 // TODO
                 return false;
            }
        }
    }
}