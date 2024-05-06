using Microsoft.AspNetCore.Mvc;
using Wheelzy.Core.Interface;
using Wheelzy.Core.Services;

namespace Wheelzy.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WheelzyController : ControllerBase
{

    private readonly ICarService _CarService;
    private readonly IOrderService _OrderService;


    public WheelzyController(ICarService service, IOrderService OrderService)
    {
        _CarService = service;
        _OrderService = OrderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCarDetails()
    {
        try
        {
            var carDetails = await _CarService.GetCarDetailsAsync();
            return Ok(carDetails);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving car details", ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetOrder([FromQuery] DateTime dateFrom, 
                                          [FromQuery] DateTime dateTo,
                                          [FromQuery] List<int> customerIds,
                                          [FromQuery] List<int> statusIds,
                                          [FromQuery] bool? isActive)
    {
        try
        {
            if (dateTo < dateFrom)
            {
                return BadRequest("End date must be greater than or equal to start date.");
            }

            var order = await _OrderService.GetOrders(dateFrom, dateTo, customerIds, statusIds, isActive);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


}
