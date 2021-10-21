using CSVProssesing.Data;
using CSVProssesing.Models.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProssesing.Controllers
{
    public class PersonController : Controller
    {
        private DataContext dataContext;
        public PersonController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost, HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await dataContext.People.FindAsync(id);

            dataContext.People.Remove(person);
            await dataContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await dataContext.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataContext.Update(person);
                    await dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(person);
        }

        private bool PersonExists(int id)
        {
            return dataContext.People.Any(e => e.Id == id);
        }
    }
}
