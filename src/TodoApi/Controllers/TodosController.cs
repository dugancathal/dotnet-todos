using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        public ITodoRepository Todos { get; set; }

        public TodosController(ITodoRepository todos)
        {
            Todos = todos;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return Todos.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = Todos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem todo)
        {
            if (todo == null)
            {
                return BadRequest();
            }
            Todos.Add(todo);
            return CreatedAtRoute("GetTodo", new {id = todo.Key}, todo);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem todo)
        {
            if (todo == null || todo.Key != id)
            {
                return BadRequest();
            }

            var foundTodo = Todos.Find(id);
            if (foundTodo == null)
            {
                return NotFound();
            }
            Todos.Update(todo);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Destroy(string id)
        {
            var todo = Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            Todos.Remove(id);
            return new ObjectResult(todo);
        }
    }
}