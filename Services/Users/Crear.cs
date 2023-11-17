using MediatR;
using react.api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using react.api.Models;

namespace react.api.Services.Users
{
    public class Crear : IRequest
    {
        public string Nombre { get; set; }
        public string Coreo { get; set; }
        public int Edad { get; set; }
    }

    internal class HandlerCreate : IRequestHandler<Crear>
    {
        private readonly IRepository<Usuario> _usuarioRepository;
        public HandlerCreate(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public async Task Handle(Crear request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Nombre)) 
            {
                throw new System.Exception("El nombre no pueder null or vacio.");
            }

            if (string.IsNullOrEmpty(request.Coreo))
            {
                throw new System.Exception("El Coreo no pueder null or vacio.");
            }
            if (request.Edad <= 0)
            {
                throw new System.Exception("La Edad no puede ser menor o igual a Zero.");
            }
            await _usuarioRepository.Add(new Usuario { Nombre = request.Nombre, Coreo= request.Coreo, Edad= request.Edad });
        }
    }

}
