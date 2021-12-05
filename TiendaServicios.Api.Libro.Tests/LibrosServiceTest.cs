using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libros.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        [Fact]
        public void GetLibros()
        {

            var mockContexto = new Mock<ContextoLibreria>();
            var mockMapper = new Mock<IMapper>();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper.Object);



        }

    }
}
