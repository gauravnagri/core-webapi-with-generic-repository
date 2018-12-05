using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarterAPI.Models;
using StarterAPI.Repositories;

namespace StarterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGenericRepository<Todo> _repository;
        public HomeController(IGenericRepository<Todo> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await _repository.GetEntities();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id = 123)
        {
            var item = await _repository.GetEntity(id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Todo item)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.PostEntity(item);
            
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Todo item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = await _repository.UpdateEntity(id, item);
            if(todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _repository.DeleteEntity(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
    }
}