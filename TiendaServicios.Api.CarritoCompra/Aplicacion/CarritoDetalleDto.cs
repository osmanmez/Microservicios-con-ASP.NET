﻿namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDetalleDto
    {
        public Guid? LibroId  { get; set; }

        public string TituloLibro { get; set; }
        
        public string AutorLibto { get; set; }

        public string? FechaPublicacion { get; set; }

    }
}
