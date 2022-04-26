using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLibrary.Model;
using Microsoft.EntityFrameworkCore; //Link EF Core

namespace WebLibrary.View.Controllers
{
    public class EmprestimoController : Controller
    {
        Context db;
        public EmprestimoController()
        {
            db = new Context();
        }
        // GET: EmprestimoController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Emprestimo()
        {
            var oList = db.LivroClienteEmprestimo.Include(e => e.IdClienteNavigation).AsNoTracking().Include(e => e.IdLivroNavigation).AsNoTracking();
            return View(oList);
        }

        // GET: EmprestimoController/Details/5
        public ActionResult Details(int id)
        {
            LivroClienteEmprestimo oEmpr = db.LivroClienteEmprestimo.Find(id);
            Cliente oCliente = db.Cliente.Find(oEmpr.IdCliente);
            Livro oLivro = db.Livro.Find(oEmpr.IdLivro);

            oEmpr.IdClienteNavigation.Nome = oCliente.Nome;
            oEmpr.IdLivroNavigation.Nome = oLivro.Nome;

            return View(oEmpr);
        }

        // GET: EmprestimoController/Create
        public ActionResult Create()
        {
            var Cliente = new SelectList(db.Cliente.ToList(), "Id", "Nome");
            ViewBag.Cliente = Cliente;

            var Livro = new SelectList(db.Livro.ToList(), "Id", "Nome");
            ViewBag.Livro = Livro;

            return View();
        }

        // POST: EmprestimoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LivroClienteEmprestimo oEmpr)
        {
            
            db.LivroClienteEmprestimo.Add(oEmpr);
            db.SaveChanges();
            return RedirectToAction("Emprestimo");
        }

        // GET: EmprestimoController/Edit/5
        public ActionResult Edit(int id)
        {
            var Cliente = new SelectList(db.Cliente.ToList(), "Id", "Nome");
            ViewBag.Cliente = Cliente;

            var Livro = new SelectList(db.Livro.ToList(), "Id", "Nome");
            ViewBag.Livro = Livro;

            LivroClienteEmprestimo oEmpr = db.LivroClienteEmprestimo.Find(id);
            return View(oEmpr);
        }

        // POST: EmprestimoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LivroClienteEmprestimo oEmpr)
        {
            var oEmprBanco = db.LivroClienteEmprestimo.Find(id);
            oEmprBanco.IdLivro = oEmpr.IdLivro;
            oEmprBanco.IdCliente = oEmpr.IdCliente;
            oEmprBanco.DataEmprestimo = oEmpr.DataEmprestimo;
            oEmprBanco.DataDevolucao = oEmpr.DataDevolucao;

            db.Entry(oEmprBanco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Emprestimo");
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // GET: EmprestimoController/Delete/5
        public ActionResult Delete(int id = 0)
        {
            LivroClienteEmprestimo oEmpr = db.LivroClienteEmprestimo.Find(id);
            if (oEmpr == null)
            {
                return HttpNotFound();
            }
            return View(oEmpr);
        }

        // POST: LivroController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            LivroClienteEmprestimo oEmpr = db.LivroClienteEmprestimo.Find(id);
            if (oEmpr == null)
            {
                return HttpNotFound();
            }

            db.LivroClienteEmprestimo.Remove(oEmpr);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Response.WriteAsync("<script>" +

                    "var r=confirm('Ocorreu um erro interno!');" +
                        "if (r == true || r == false)" +
                        "{document.location.replace('../Emprestimo')}" +

                        "</script>");
            }
            return RedirectToAction("Emprestimo");
        }
    }
}
