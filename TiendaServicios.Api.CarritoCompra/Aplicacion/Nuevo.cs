﻿using AutoMapper;
using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string FechaCreacionSesion { get; set; }
            
            public List<string> ProductoLista { get; set; }

        }


        public class Manejador : IRequestHandler<Ejecuta>
        {

            public readonly CarritoContexto _contexto;
            public readonly IMapper _mapper;

            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
               var value = await _contexto.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception(" No se creo la sesion.");
                };

                int id = carritoSesion.CarritoSesionId;


                foreach (var item in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = "2021-12-03",
                        CarritoSesionId = id,
                        ProductoSeleccionado = item
                    };


                    _contexto.CarritoSesionDetalle.Add(detalleSesion);

                }

                value = await _contexto.SaveChangesAsync();

                if(value > 0)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo crear el carrito de compras");

            }
        }
    }
}
