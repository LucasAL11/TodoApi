using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Todo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "todos")]
        public async Task<IActionResult> GetActionAsync(
            [FromServices] AppDbContext appDbContext)
        {
            var todos = await appDbContext.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }
        [HttpGet]
        [Route(template: "todos/{Id}")]
        public async Task<IActionResult> GetByIdAsync(
             [FromServices] AppDbContext appDbContext,
             [FromRoute] int id)
        {
            var todo = await
            appDbContext
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id);

            return todo == null
            ? NotFound()
            : Ok(todo);

        }
        [HttpPost]
        [Route(template: "todos")]


        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext appDbContext,
            [FromBody] TodoModels model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new TodoModels
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await appDbContext.Todos.AddAsync(todo);
                await appDbContext.SaveChangesAsync();

                return Created($"v1/todos/{todo.ID}", todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }


        }

        [HttpPut]
        [Route(template: "todos/{Id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext appDbContext,
            [FromBody] TodoModels model,
            [FromRoute] int id
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await appDbContext.Todos.FirstOrDefaultAsync(x => x.ID == id);
            if (todo == null)
                return NotFound();


            try
            {
                todo.Title = model.Title;


                appDbContext.Todos.Update(todo);
                await appDbContext.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }


        }

        [HttpDelete("todos/{Id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext appDbContext,
            [FromRoute] int id
        )
        {

            var todo = await appDbContext.Todos.FirstOrDefaultAsync(x => x.ID == id);



            try
            {
                appDbContext.Todos.Remove(todo);
                await appDbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}



