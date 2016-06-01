using PF_SOA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PF_SOA
{
    public class PersonDBInitializer : DropCreateDatabaseAlways<ArticuloDBContext>
    {
        protected override void Seed(ArticuloDBContext context)
        {
            var articulos = new List<Articulo> { 
                new Articulo { Producto = "Manzana", Costo = "1", Referencia = "Mz"},
                new Articulo { Producto = "Pera", Costo = "2", Referencia = "Pr"},
                new Articulo { Producto = "Arroz", Costo = "5", Referencia = "AZ"},
                new Articulo { Producto = "Tomate", Costo = "4", Referencia = "Tt"},
                new Articulo { Producto = "Libro", Costo = "10", Referencia = "Lb"},
                new Articulo { Producto = "Avena", Costo = "3", Referencia = "Av"}
            };

            articulos.ForEach(c => context.Articulo.Add(c));
            context.SaveChanges();
        }
    }
}