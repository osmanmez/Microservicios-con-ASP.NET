using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libros.Modelo;
using TiendaServicios.Api.Libros.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {

            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mappper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mappper = mapper;
            }

            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();


                var librosDTO = _mappper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);

                return librosDTO;

            }
        };


    }
}
