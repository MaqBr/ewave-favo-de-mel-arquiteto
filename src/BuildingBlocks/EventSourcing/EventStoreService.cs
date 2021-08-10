using EventStore.ClientAPI;
using FavoDeMel.Domain.Core.Model.Configuration;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection
                .Create(configuration[AppSettings.Keys.ConnectionStrings.DEFAULT_EVENT_STORE_CONNECTION_STRING]);

            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}