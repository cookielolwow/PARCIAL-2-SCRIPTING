using System;

public class Jugador
{
    public decimal Oro { get; private set; }

    public List<Item> Equipamiento { get; } = new List<Item>();
    public List<Item> Consumibles { get; } = new List<Item>();

    public Jugador(decimal oro)
    {
        Oro = oro >= 0 ? oro : 0;
    }

    public bool PuedePagar(decimal monto)
    {
        return monto >= 0 && Oro >= monto;
    }

    public bool GastarOro(decimal monto)
    {
        if (monto < 0 || monto > Oro)
            return false;

        Oro -= monto;
        return true;
    }

    public void AgregarItem(Item item)
    {
        if (item == null || !item.EsValido())
            return;

        if (item.Categoria == CategoriaItem.Supply)
            Consumibles.Add(item);
        else
            Equipamiento.Add(item);
    }
}