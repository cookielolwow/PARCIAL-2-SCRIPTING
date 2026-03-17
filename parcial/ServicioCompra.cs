using System.Collections.Generic;

public class ServicioCompra
{
    public static bool Comprar(Jugador jugador, Tienda tienda, Dictionary<Item, int> itemsAComprar)
    {
        if (jugador == null  tienda == null 
 itemsAComprar == null)
            return false;

        decimal costoTotal = 0;

        foreach (var entry in itemsAComprar)
        {
            var item = entry.Key;
            var cantidad = entry.Value;

            if (cantidad <= 0)
                return false;

            if (!tienda.TieneStock(item, cantidad))
                return false;

            costoTotal += item.Precio * cantidad;
        }

        if (!jugador.PuedePagar(costoTotal))
            return false;

 
        if (!jugador.GastarOro(costoTotal))
            return false;

        foreach (var entry in itemsAComprar)
        {
            var item = entry.Key;
            var cantidad = entry.Value;

            if (!tienda.ReducirStock(item, cantidad))
                return false;

            for (int i = 0; i < cantidad; i++)
            {
                jugador.AgregarItem(item);
            }
        }

        return true;
    }
}
