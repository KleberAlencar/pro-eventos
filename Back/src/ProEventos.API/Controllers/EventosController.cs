using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Authorization;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IAccountService _accountService;

    public EventosController(
        IEventoService eventoService,
        IWebHostEnvironment hostEnvironment,
        IAccountService accountService
    )
    {
        _eventoService = eventoService;
        _hostEnvironment = hostEnvironment;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {    
            var eventos = await _eventoService.GetAllEventosAsync(User.GetUserId(), true);
            if (eventos == null) return NoContent();    

            return Ok(eventos);        
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
    {
        try
        {    
            var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), id, true);
            if (evento == null) return NoContent();

            return Ok(evento);        
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
        }
    }

    [HttpGet("{tema}/tema")]
    public async Task<IActionResult> GetByTema(string tema) 
    {
        try
        {    
            var eventos = await _eventoService.GetAllEventosByTemaAsync(User.GetUserId(), tema, true);
            if (eventos == null) return NoContent();

            return Ok(eventos);        
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
        try
        {    
            var evento = await _eventoService.AddEvento(User.GetUserId(), model);
            if (evento == null) return NoContent();

            return Ok(evento);        
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar inserir evento. Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
        try
        {    
            var evento = await _eventoService.UpdateEvento(User.GetUserId(), id, model);
            if (evento == null) return NoContent();

            return Ok(evento);        
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {    
            var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), id, true);
            if (evento == null) return NoContent();

            return await _eventoService.DeleteEvento(User.GetUserId(), id) ? Ok("Deletado") : throw new Exception("Erro ao deletar");
        }
        catch (Exception ex)
        {            
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir evento. Erro: {ex.Message}");
        }        
    }
}
