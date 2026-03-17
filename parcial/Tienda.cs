using System;

using System.Collections.Generic;
using System.Linq;

public class Tienda
{
    private List<ItemTienda> inventario = new List<ItemTienda>();

    public Tienda(List<ItemTienda> itemsIniciales)
    {
        if (itemsIniciales != null && itemsIniciales.Count > 0)
            inventario = itemsIniciales;
    }

    public bool TieneItems()
    {
        return inventario.Count > 0;
    }

    public bool AgregarItem(Item item, int cantidad)
    {
        if (item == null || !item.EsValido() || cantidad < 0)
            return false;

        var existente = inventario.FirstOrDefault(i =>
            i.Item.Nombre == item.Nombre &&
            i.Item.Categoria == item.Categoria);

        if (existente != null)
        {
            if (existente.Item.Precio != item.Precio)
                return false;

            return existente.AgregarCantidad(cantidad);
        }

        inventario.Add(new ItemTienda(item, cantidad));
        return true;
    }

    public bool TieneStock(Item item, int cantidad)
    {
        var existente = inventario.FirstOrDefault(i =>
            i.Item.Nombre == item.Nombre &&
            i.Item.Categoria == item.Categoria);

        return existente != null && existente.Cantidad >= cantidad;
    }

    public bool ReducirStock(Item item, int cantidad)
    {
        var existente = inventario.FirstOrDefault(i =>
            i.Item.Nombre == item.Nombre &&
            i.Item.Categoria == item.Categoria);

        if (existente == null)
            return false;

        return existente.ReducirCantidad(cantidad);
    }
}