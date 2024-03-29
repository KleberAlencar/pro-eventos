using AutoMapper;
using ProEventos.Domain;
using ProEventos.Application.Dtos;
using ProEventos.Application.Contratos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;

        public EventoService(
            IGeralPersistence geralPersistence, 
            IEventoPersistence eventoPersistence,
            IMapper mapper
        )
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEvento(int userId, EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);    
                evento.UserId = userId; 

                _geralPersistence.Add<Evento>(evento);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return _mapper.Map<EventoDto>(await _eventoPersistence.GetEventoByIdAsync(userId, evento.Id, false));
                }
                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;

                _geralPersistence.Update<Evento>(_mapper.Map(model, evento));
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return _mapper.Map<EventoDto>(await _eventoPersistence.GetEventoByIdAsync(userId, evento.Id, false));
                }
                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(userId, includePalestrantes);
                if (eventos == null) return null;

                return _mapper.Map<EventoDto[]>(eventos);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(userId, tema, includePalestrantes);
                if (eventos == null) return null;

                return _mapper.Map<EventoDto[]>(eventos);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, includePalestrantes);
                if (evento == null) return null;

                return _mapper.Map<EventoDto>(evento);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}