using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IActorService
{
    IEnumerable<Actor> GetAll();
    Actor GetById(Guid id);
    void Add(Actor actor);
    void Update(Actor actor);
    void Delete(Guid id);
    void Delete(Actor actor);
} 