using EventStore.ClientAPI;

namespace BuildingBlocks.EventSourcing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}