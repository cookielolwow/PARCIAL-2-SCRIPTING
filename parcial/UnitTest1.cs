using NUnit.Framework;
using System.Collections.Generic;

namespace parcial
{
   
    public static class TestDataRPG
    {
        public static readonly Item espada = new Item("Espada", 10m, CategoriaItem.Weapon);
        public static readonly Item armadura = new Item("Armadura", 20m, CategoriaItem.Armor);
        public static readonly Item anillo = new Item("Anillo", 5m, CategoriaItem.Accessory);
        public static readonly Item pocion = new Item("Pocion", 3m, CategoriaItem.Supply);

     
        public static IEnumerable<TestCaseData> CrearItemData()
        {
            yield return new TestCaseData(espada.Nombre, espada.Precio, espada.Categoria, true)
                .SetName("CrearItem_Espada_Valido");
            yield return new TestCaseData(armadura.Nombre, armadura.Precio, armadura.Categoria, true)
                .SetName("CrearItem_Armadura_Valido");
            yield return new TestCaseData(anillo.Nombre, anillo.Precio, anillo.Categoria, true)
                .SetName("CrearItem_Anillo_Valido");
            yield return new TestCaseData(pocion.Nombre, pocion.Precio, pocion.Categoria, true)
                .SetName("CrearItem_Pocion_Valido");

            yield return new TestCaseData("", 10m, CategoriaItem.Weapon, false)
                .SetName("CrearItem_NombreVacio_NoValido");
            yield return new TestCaseData("ItemMalo", -1m, CategoriaItem.Weapon, false)
                .SetName("CrearItem_PrecioNegativo_NoValido");
        }

   
        public static IEnumerable<TestCaseData> JugadorPuedePagarData()
        {
            yield return new TestCaseData(100m, 50m, true)
                .SetName("JugadorOroSuficiente_PuedePagar");
            yield return new TestCaseData(10m, 20m, false)
                .SetName("JugadorOroInsuficiente_NoPuedePagar");
        }

   
        public static IEnumerable<TestCaseData> ItemInventarioData()
        {
            yield return new TestCaseData(espada, true, false)
                .SetName("AgregarItem_Espada_Equipamiento");
            yield return new TestCaseData(pocion, false, true)
                .SetName("AgregarItem_Pocion_Consumible");
        }

        public static IEnumerable<TestCaseData> TiendaTieneItemsData()
        {
            yield return new TestCaseData(0, false).SetName("TiendaVacia_NoTieneItems");
            yield return new TestCaseData(1, true).SetName("TiendaConItems_TieneItems");
        }

  
        public static IEnumerable<TestCaseData> ComprarItemsData()
        {
            yield return new TestCaseData(100m, 10m, 5, true)
                .SetName("Comprar_SuficienteOro_StockDisponible");
            yield return new TestCaseData(10m, 10m, 2, false)
                .SetName("Comprar_OroInsuficiente");
            yield return new TestCaseData(100m, 10m, 20, false)
                .SetName("Comprar_StockInsuficiente");
        }
    }


    [TestFixture]
    public class PruebasCreacionItem
    {
        [TestCaseSource(typeof(TestDataRPG), nameof(TestDataRPG.CrearItemData))]
        public void CrearItem(string nombre, decimal precio, CategoriaItem categoria, bool esperado)
        {
            Item item = new Item(nombre, precio, categoria);
            Assert.That(item.EsValido(), Is.EqualTo(esperado));
        }
    }


    [TestFixture]
    public class PruebasCreacionTienda
    {
        [TestCaseSource(typeof(TestDataRPG), nameof(TestDataRPG.TiendaTieneItemsData))]
        public void TiendaTieneItems(int cantidadItems, bool esperado)
        {
            List<ItemTienda> lista = new List<ItemTienda>();
            for (int i = 0; i < cantidadItems; i++)
            {
                lista.Add(new ItemTienda(TestDataRPG.espada, 5));
            }

            Tienda tienda = new Tienda(lista);
            Assert.That(tienda.TieneItems(), Is.EqualTo(esperado));
        }
    }


    [TestFixture]
    public class PruebasJugador
    {
        [TestCaseSource(typeof(TestDataRPG), nameof(TestDataRPG.JugadorPuedePagarData))]
        public void JugadorPuedePagar(decimal oroInicial, decimal costo, bool esperado)
        {
            Jugador jugador = new Jugador(oroInicial);
            bool resultado = jugador.PuedePagar(costo);
            Assert.That(resultado, Is.EqualTo(esperado));
        }
    }

   
    [TestFixture]
    public class PruebasCompra
    {
        [TestCaseSource(typeof(TestDataRPG), nameof(TestDataRPG.ComprarItemsData))]
        public void ComprarItems(decimal oro, decimal precio, int cantidad, bool esperado)
        {
            Item item = new Item("Pocion", precio, CategoriaItem.Supply);
            Tienda tienda = new Tienda(new List<ItemTienda> { new ItemTienda(item, 10) });
            Jugador jugador = new Jugador(oro);
            List<ItemTienda> compra = new List<ItemTienda> { new ItemTienda(item, cantidad) };

            bool resultado = ServicioCompra.Comprar(jugador, tienda, compra);

            Assert.That(resultado, Is.EqualTo(esperado));
        }
    }


     [TestFixture]
    public class PruebasInventarioJugador
    {
        [TestCaseSource(typeof(TestDataRPG), nameof(TestDataRPG.ItemInventarioData))]
        public void ItemSeAgregaAlInventarioCorrecto(Item item, bool enEquipamiento, bool enConsumibles)
        {
            Jugador jugador = new Jugador(100m);
            jugador.AgregarItem(item);

            Assert.That(jugador.Equipamiento.Contains(item), Is.EqualTo(enEquipamiento));
            Assert.That(jugador.Consumibles.Contains(item), Is.EqualTo(enConsumibles));
        }
    }

}