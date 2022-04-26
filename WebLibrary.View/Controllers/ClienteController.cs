using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Model;

namespace WebLibrary.View.Controllers
{  

    public class ClienteController : Controller
    {
        Context db;
        public ClienteController()
        {
            db = new Context();
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }
        // -------------------------------------------------------------------

        public ActionResult Cliente()
        {
            List<Cliente> oLista = db.Cliente.ToList();
            return View(oLista);
        }

        //-----------------------------------------------------------------

            // GET: ClienteController/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente oCat)
        {
            db.Cliente.Add(oCat);
            db.SaveChanges();
            return RedirectToAction("Cliente");
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            Cliente oCat = db.Cliente.Find(id);
            return View(oCat);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cliente oCat)
        {
            var oCatBanco = db.Cliente.Find(id);
            oCatBanco.Cpf = oCat.Cpf;
            oCatBanco.Nome = oCat.Nome;
            oCatBanco.Endereco = oCat.Endereco;
            oCatBanco.Cidade = oCat.Cidade;
            oCatBanco.Bairro = oCat.Bairro;

            db.Entry(oCatBanco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Cliente");
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
