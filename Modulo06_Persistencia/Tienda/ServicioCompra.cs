using Cun.Poo.Modulo06Persistencia.Datos;
using Cun.Poo.Modulo06Persistencia.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cun.Poo.Modulo06Persistencia.Tienda;

/// <summary>
/// Logica de compra de "Cafes de mi Tierra" (caso 2.2 del recurso).
///
/// Finalizar la compra son varias escrituras que deben ocurrir TODAS o
/// NINGUNA: crear el pedido, guardar sus detalles, descontar el stock y
/// vaciar el carrito. Eso es una TRANSACCION.
/// </summary>
public class ServicioCompra
{
    private readonly CursoDbContext _db;

    public ServicioCompra(CursoDbContext db)
    {
        _db = db;
    }

    /// <summary>INSERT en CARRITO_ITEMS.</summary>
    public void AgregarAlCarrito(string cliente, Cafe cafe, int cantidad)
    {
        _db.Carrito.Add(new ItemCarrito { Cliente = cliente, CafeId = cafe.Id, Cantidad = cantidad });
        _db.SaveChanges();
    }

    /// <summary>SELECT con JOIN entre CARRITO_ITEMS y PRODUCTOS (Include).</summary>
    public List<ItemCarrito> VerCarrito(string cliente) =>
        _db.Carrito.Include(i => i.Cafe)
                   .Where(i => i.Cliente == cliente)
                   .OrderBy(i => i.Id)
                   .ToList();

    /// <summary>
    /// La transaccion critica. Si cualquier paso falla, Rollback deja la base
    /// exactamente como estaba: ni pedido a medias ni stock descontado.
    /// </summary>
    /// <returns>El pedido creado.</returns>
    /// <exception cref="InvalidOperationException">Si un cafe no tiene stock suficiente.</exception>
    public Pedido FinalizarCompra(string cliente)
    {
        List<ItemCarrito> items = VerCarrito(cliente);
        if (items.Count == 0)
        {
            throw new InvalidOperationException("El carrito esta vacio.");
        }

        using var transaccion = _db.Database.BeginTransaction();
        try
        {
            var pedido = new Pedido { Cliente = cliente };
            _db.Pedidos.Add(pedido);
            _db.SaveChanges();                                       // 1. INSERT en PEDIDOS

            foreach (ItemCarrito item in items)
            {
                Cafe cafe = item.Cafe;
                if (cafe.Stock < item.Cantidad)                      // 2. verificar stock
                {
                    throw new InvalidOperationException(
                        $"No hay stock: {cafe.Nombre} tiene {cafe.Stock} y se pidieron {item.Cantidad}.");
                }
                pedido.Detalles.Add(new DetallePedido
                {
                    CafeId = cafe.Id, Cantidad = item.Cantidad, PrecioUnitario = cafe.Precio,
                });                                                  // 3. INSERT en DETALLES_PEDIDO
                pedido.Total += cafe.Precio * item.Cantidad;
                cafe.Stock -= item.Cantidad;                         // 4. UPDATE PRODUCTOS SET stock = ...
                _db.SaveChanges();
            }

            _db.Carrito.RemoveRange(items);                          // 5. DELETE de CARRITO_ITEMS
            _db.SaveChanges();

            transaccion.Commit();                                    // todo o nada: TODO
            return pedido;
        }
        catch
        {
            transaccion.Rollback();                                  // todo o nada: NADA
            _db.ChangeTracker.Clear();                               // olvida los objetos modificados en RAM
            throw;
        }
    }
}
