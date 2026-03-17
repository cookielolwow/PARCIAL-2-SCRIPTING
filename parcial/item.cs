using System;

public enum CategoriaItem
{
    Weapon,
    Armor,
    Accessory,
    Supply
}
public class Item
{
    public string Nombre { get; }
    public decimal Precio { get; }
    public CategoriaItem Categoria { get; }

    public Item(string nombre, decimal precio, CategoriaItem categoria)
    {
        Nombre = nombre;
        Precio = precio;
        Categoria = categoria;
    }

    public bool EsValido()
    {
        return !string.IsNullOrWhiteSpace(Nombre) && Precio > 0;
    }
}