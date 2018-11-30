using System.Threading.Tasks;

namespace Viper.Common
{
    public interface IRepository<TAggregate, TAggregateId>
        where TAggregate : AggregateRoot
    {
        Task<TAggregate> GetByIdAsync(TAggregateId id);

        Task SaveAsync(TAggregate aggregate);
    }
}
