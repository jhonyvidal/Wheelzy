using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using Wheelzy.Core.DTO;
using Wheelzy.Core.Interface;

namespace Wheelzy.Core.Services;

public class CarService:ICarService
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public CarService(DbContextOptions<AppDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }
    public async Task<List<CarInfoDTO>> GetCarDetailsAsync()
    {
        try
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var result = await context.Cars
                    .Include(c => c.Quotes)
                        .ThenInclude(q => q.Buyer)
                    .Include(c => c.StatusHistories)
                        .ThenInclude(sh => sh.Status)
                    .Select(c => new
                    {
                        Car = c,
                        LatestQuote = c.Quotes.FirstOrDefault(q => q.IsCurrent),
                        LatestStatusHistory = c.StatusHistories.OrderByDescending(sh => sh.StatusHistoryID).FirstOrDefault()
                    })
                    .ToListAsync();

                var carInfoDTOs = result.Select(r => new CarInfoDTO
                {
                    Year = r.Car.Year,
                    Make = r.Car.Make,
                    Model = r.Car.Model,
                    Submodel = r.Car.Submodel,
                    CurrentBuyerName = r.LatestQuote.Buyer.Name,
                    CurrentQuote = r.LatestQuote.Amount,
                    CurrentStatus = r.LatestStatusHistory.Status.Name,
                    StatusDate = r.LatestStatusHistory.StatusDate
                }).ToList();

                return carInfoDTOs;
            }
            
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving car details", ex);
        }
    }


    public bool IsDatabaseConnected()
    {
        try
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Database.OpenConnection();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to the database: " + ex.Message);
            return false;
        }
    }

}