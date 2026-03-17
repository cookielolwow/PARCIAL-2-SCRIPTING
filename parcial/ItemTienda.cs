using System;

public class ItemTienda
{
    public Item Item { get; }
    public int Cantidad { get; private set; }

    public ItemTienda(Item item, int cantidad)
    {
        Item = item;
        Cantidad = cantidad >= 0 ? cantidad : 0;
    }

    public bool AgregarCantidad(int cantidad)
    {
        if (cantidad < 0) return false;

        Cantidad += cantidad;
        return true;
    }

    public bool ReducirCantidad(int cantidad)
    {
        if (cantidad < 0 || cantidad > Cantidad)
            return false;

        Cantidad -= cantidad;
        return true;
    }
}