using ProEventos.API.Data;
using ProEventos.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly DataContext _context;

    public EventoController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
        return _context.Eventos;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetById(int id) 
    {
        return _context.Eventos.Where(evento => evento.EventoId == id);
    }
}
