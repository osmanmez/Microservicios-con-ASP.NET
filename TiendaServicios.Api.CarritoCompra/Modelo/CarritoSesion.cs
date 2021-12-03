namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get;set;}

        public string? FechaCreacion { get; set; }

        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }

    }
}
