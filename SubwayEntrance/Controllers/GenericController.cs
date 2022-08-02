using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubwayEntrance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Controllers
{
    
    public abstract class GenericController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {

        public readonly TRepository repository;

        public GenericController(TRepository repository)
        {
            this.repository = repository;
        }


        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity UserSubway)
        {
            await repository.Add(UserSubway);
            return CreatedAtAction("Get", new { id = UserSubway.Id }, UserSubway);
        }

    }
}
