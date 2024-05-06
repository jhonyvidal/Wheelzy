
using Microsoft.EntityFrameworkCore;

namespace Wheelzy.Core.Services;
public class CustomersService
{
    private readonly DbContextOptions<AppDbContext> dbContext;

    public CustomersService(DbContextOptions<AppDbContext> dbContextOptions)
    {
        dbContext = dbContextOptions;
    }
    // public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices)
    // {
    //     // Lista para almacenar los ID únicos de los clientes asociados a las facturas
    //     var customerIds = invoices.Select(i => i.CustomerId.Value).Distinct().ToList();

    //     // Consulta para recuperar todos los clientes asociados a las facturas en una sola llamada
    //     var customers = dbContext.Customers.Where(c => customerIds.Contains(c.Id)).ToList();

    //     // Usamos una transacción única para todas las actualizaciones
    //     using (var transaction = dbContext.Database.BeginTransaction())
    //     {
    //         try
    //         {
    //             foreach (var invoice in invoices)
    //             {
    //                 // Buscamos el cliente asociado a la factura en la lista de clientes
    //                 var customer = customers.Single(c => c.Id == invoice.CustomerId.Value);

    //                 // Actualizamos el saldo del cliente
    //                 customer.Balance -= invoice.Total;
    //             }

    //             // Guardamos los cambios en la base de datos
    //             dbContext.SaveChanges();

    //             // Confirmamos la transacción
    //             transaction.Commit();
    //         }
    //         catch (Exception)
    //         {
    //             // En caso de error, revertimos la transacción
    //             transaction.Rollback();
    //             throw; // Volvemos a lanzar la excepción para que sea manejada en un nivel superior
    //         }
    //     }
    // }
}