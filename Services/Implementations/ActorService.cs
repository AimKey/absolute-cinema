using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public IEnumerable<Actor> GetAll()
    {
        return _actorRepository.Get().ToList();
    }

    public Actor GetById(Guid id)
    {
        return _actorRepository.GetByID(id);
    }

    public void Add(Actor actor)
    {
        _actorRepository.Insert(actor);
        _actorRepository.Save();
    }

    public void Update(Actor actor)
    {
        _actorRepository.Update(actor);
        _actorRepository.Save();
    }

    public void Delete(Guid id)
    {
        _actorRepository.Delete(id);
        _actorRepository.Save();
    }

    public void Delete(Actor actor)
    {
        _actorRepository.Delete(actor);
        _actorRepository.Save();
    }
} 