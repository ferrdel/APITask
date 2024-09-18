using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_DataAcccessLayer.Data;
using Gestion_DataAcccessLayer.Models;
using WebApp_APIGestionTareas.Infrastructure.Dto;
using Task = Gestion_DataAcccessLayer.Models.Task;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApp_APIGestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }
       
        /*
        // GET: api/Tasks/
        [HttpGet("GetByState")]
        public Task<IActionResult> GetTaskState(string state)
        {
            var _task = _context.Tasks.Where(e => e.State == state);

            return _task();

        }
        */

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(TaskDTO taskDto)
        {
            if (ModelState.IsValid)
            {
                var task = new Task
                {
                    Name = taskDto.Name,
                    Description = taskDto.Description,
                    State = taskDto.State,
                    UserId = taskDto.UserId
                };
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTask", new { id = task.Id }, task);
            }
            return BadRequest(ModelState);
            
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
