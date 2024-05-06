using Wheelzy.Core.DTO;
using Wheelzy.Core.Services;

namespace Wheelzy.Core.Interface;
public interface IOrderService {
    Task<OrderDTO> GetOrders(DateTime dateFrom, DateTime dateTo,
                                            List<int> customerIds, List<int> statusIds, bool? isActive);
}