using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingRealtime.Data;
using MessagingRealtime.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessagingRealtime.Controllers
{
    public abstract class GenericController<TEntity, TRepository> : ControllerBase
        where TEntity: class,IEntity
        where TRepository: IRepository<TEntity>
    {
        private readonly TRepository repository;
        public GenericController(TRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var movie = await repository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }
            await repository.Update(message);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity message)
        {
            await repository.Add(message);
            return CreatedAtAction("Get", new { id = message.Id }, message);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var message = await repository.Delete(id);
            if (message == null)
            {
                return NotFound();
            }
            return message;
        }
    }
}