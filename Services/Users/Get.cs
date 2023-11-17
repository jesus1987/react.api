using MediatR;
using Microsoft.EntityFrameworkCore;
using react.api.Models;
using react.api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace react.api.Services.Users;

public class Get: IRequest<Usuarios>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

internal class HandlerGetInfo : IRequestHandler<Get, Usuarios>
{
    private readonly IRepository<Usuario> _usuarioRepository;
    public HandlerGetInfo(IRepository<Usuario> localRepository)
    {
        _usuarioRepository = localRepository;
    }

    public async Task<Usuarios> Handle(Get request, CancellationToken cancellationToken)
    {
        var resultado = await _usuarioRepository.Query
            .OrderBy(at => at.Nombre)
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size).ToListAsync();
        var total = _usuarioRepository.Query.Count();
        return new Usuarios { Data = resultado, Length = total};
    }
}

