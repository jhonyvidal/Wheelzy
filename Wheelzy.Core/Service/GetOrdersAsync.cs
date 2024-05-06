using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wheelzy.Core.DTO;
using Wheelzy.Core.Interface;

namespace Wheelzy.Core.Services;
public class OrderService:IOrderService
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public OrderService(DbContextOptions<AppDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }
    public async Task<OrderDTO> GetOrders(DateTime dateFrom, DateTime dateTo,
                                            List<int> customerIds, List<int> statusIds, bool? isActive)
    {

        using (var context = new AppDbContext(_dbContextOptions))
        {
            var query = context.Orders.AsQueryable();

            //Filters based on parameters
            if (dateFrom != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate >= dateFrom);
            }
            if (dateTo != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate <= dateTo);
            }
            if (customerIds != null && customerIds.Any())
            {
                query = query.Where(o => customerIds.Contains(o.CustomerId));
            }
            if (statusIds != null && statusIds.Any())
            {
                query = query.Where(o => statusIds.Contains(o.StatusId));
            }
            if (isActive.HasValue)
            {
                if (isActive == true)
                {
                    query = query.Where(o => o.IsActive);
                }
                else
                {
                    query = query.Where(o => !o.IsActive);
                }
            }

            var result = await query.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                StatusId = o.StatusId,
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}