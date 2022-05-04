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
            var oLivro = db.Livro.Find(oEmpr.IdLivro);
            //oEmpr.DataDevolucao = DateTime.MinValue;

            if(oLivro.Emprestado == false) 
            {
                oLivro.Emprestado = true;

                db.LivroClienteEmprestimo.Add(oEmpr);
                db.Entry(oLivro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();
            }
            else
            {
                Response.WriteAsync("<script>alert('O livro ja esta emprestado');document.location.replace('./Emprestimo')</script>");
            }

            
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
            var oLivro = db.Livro.Find(oEmpr.IdLivro);
            var oEmprBanco = db.LivroClienteEmprestimo.Find(id);
            var oLivroOriginal = db.Livro.Find(oEmprBanco.IdLivro);

            if (oLivroOriginal.Id != oLivro.Id && oLivro.Emprestado == true)
            {
                Response.WriteAsync("<script>alert('O livro já está emprestado');{document.location.replace('../Emprestimo')}</script>");
            }
            else
            {
                if (oEmpr.DataDevolucao != null && oEmprBanco.DataDevolucao == null)
                {    
                    oLivro.Emprestado = false;
                    oEmprBanco.DataDevolucao = oEmpr.DataDevolucao;
                }

                if(oLivro.Id != oLivroOriginal.Id)
                {
                    oLivroOriginal.Emprestado = false;
                    oLivro.Emprestado = true;
                }

                oEmprBanco.IdLivro = oEmpr.IdLivro;
                oEmprBanco.IdCliente = oEmpr.IdCliente;
                oEmprBanco.DataEmprestimo = oEmpr.DataEmprestimo;

                db.Entry(oEmprBanco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.Entry(oLivro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.Entry(oLivroOriginal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }

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

            Cliente oCliente = db.Cliente.Find(oEmpr.IdCliente);
            Livro oLivro = db.Livro.Find(oEmpr.IdLivro);

            oEmpr.IdClienteNavigation.Nome = oCliente.Nome;
            oEmpr.IdLivroNavigation.Nome = oLivro.Nome;

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

            var oLivro = db.Livro.Find(oEmpr.IdLivro);

            if(oEmpr.DataDevolucao == null)
            {
                oLivro.Emprestado = false;
            }

            db.LivroClienteEmprestimo.Remove(oEmpr);
            db.Entry(oLivro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Response.WriteAsync("<script>alert('Ocorreu um erro interno!');{document.location.replace('../Emprestimo')}</script>");
            }
            return RedirectToAction("Emprestimo");
        }
    }
}
