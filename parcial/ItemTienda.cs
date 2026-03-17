public class ItemTienda
{
    public Item Item { get; }
    public int Cantidad { get; private set; }

    public ItemTienda(Item item, int cantidad)
    {
        if (item == null || !item.EsValido() || cantidad < 0)
        {
            Item = item;
            Cantidad = 0;
            return;
        }

        Item = item;
        Cantidad = cantidad;
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