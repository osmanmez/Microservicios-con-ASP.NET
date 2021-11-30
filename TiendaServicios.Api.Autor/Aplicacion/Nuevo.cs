using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {

            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string FechaNacimiento { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())

                };

                _contexto.AutorLibro.Add(autorLibro);

                var valor = await _contexto.SaveChangesAsync();
                
                if (valor > 0) 
                {
                    return Unit.Value;
                }
                
                throw new Exception("No se pudo insertar el valor Libro.");

            }
        }

    }
}
