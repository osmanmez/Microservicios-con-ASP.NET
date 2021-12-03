using AutoMapper;
using TiendaServicios.Api.Libros.Modelo;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();


        }


    }
}
