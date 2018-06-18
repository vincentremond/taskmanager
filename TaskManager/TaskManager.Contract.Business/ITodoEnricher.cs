using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface ITodoEnricher
    {
        MetaTodo Enrich(Todo t);
    }
}
