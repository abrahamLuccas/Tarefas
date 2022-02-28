using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toDo.Data;
using toDo.Models;

namespace toDo.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppCont _appCont;

        public TarefasController(AppCont appCont)
        {
            _appCont = appCont;
        }

        public IActionResult Index()
        {
            var allTasks = _appCont.Tarefas.ToList(); 
            return View(allTasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id, Name, EndDate, Status")]
            Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _appCont.Add(tarefa);
                await _appCont.SaveChangesAsync();  
                return RedirectToAction("Index");   
            }
            return View(tarefa);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tarefa = await _appCont.Tarefas.FindAsync(id);
            if(tarefa == null)
            {
                return BadRequest();
            }
            return View(tarefa);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(
            int? id, [Bind("Id, Name, EndDate,  Status")]
            Tarefa tarefa)
        {
            if(id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _appCont.Update(tarefa);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(tarefa);

        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var tarefa = await _appCont.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

            if(tarefa==null)
            {
                return BadRequest(); 
            }
            return View(tarefa);
        }

        [HttpGet]

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _appCont.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _appCont.Tarefas.FindAsync(id);
            _appCont.Tarefas.Remove(tarefa);
            await _appCont.SaveChangesAsync();
            return RedirectToAction(nameof(Index));   
        }
    }
}
