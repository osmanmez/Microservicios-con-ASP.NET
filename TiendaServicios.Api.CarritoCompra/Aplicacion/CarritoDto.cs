namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }

        public string? FechaCreacionSesion { get; set; }

        public List<CarritoDetalleDto> ListaProductos { get; set; }

    }
}
