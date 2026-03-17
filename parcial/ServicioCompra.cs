using System.Collections.Generic;

public class ServicioCompra
{
    public static bool Comprar(Jugador jugador, Tienda tienda, List<ItemTienda> itemsAComprar)
    {
      
        if (jugador == null || tienda == null || itemsAComprar == null)
            return false;

        decimal costoTotal = 0;

       
        for (int i = 0; i < itemsAComprar.Count; i++)
        {
            ItemTienda it = itemsAComprar[i];

            if (it == null || it.Item == null || !it.Item.EsValido() || it.Cantidad <= 0)
                return false;

            costoTotal += it.Item.Precio * it.Cantidad;
        }

     
        if (!jugador.PuedePagar(costoTotal))
            return false;

        for (int i = 0; i < itemsAComprar.Count; i++)
        {
            ItemTienda it = itemsAComprar[i];

            if (!tienda.TieneStock(it.Item, it.Cantidad))
                return false;
        }

       
        if (!jugador.GastarOro(costoTotal))
            return false;

       
        for (int i = 0; i < itemsAComprar.Count; i++)
        {
            ItemTienda it = itemsAComprar[i];

            if (!tienda.ReducirStock(it.Item, it.Cantidad))
                return false; 
        }


        for (int i = 0; i < itemsAComprar.Count; i++)
        {
            ItemTienda it = itemsAComprar[i];

            for (int j = 0; j < it.Cantidad; j++)
            {
                jugador.AgregarItem(it.Item);
            }
        }

        return true;
    }
}