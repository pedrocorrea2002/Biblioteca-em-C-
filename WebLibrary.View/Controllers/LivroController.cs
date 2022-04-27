using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Model;

namespace WebLibrary.View.Controllers
{
    public class LivroController : Controller
    {
        Context db;
        public LivroController()
        {
            db = new Context();
        }

        // GET: LivroController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Livro() // retorna a lista com os livros 
        {
            List<Livro> oLivro = db.Livro.ToList();
            return View(oLivro);

        }

        //// GET: LivroController/Details/5
        public ActionResult Details(int id, Livro livro)
        {

            Livro oLivro = db.Livro.Find(id);
            return View(oLivro);

        }

        // GET: LivroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro oLivro)  // CRIAR NOVO LIVRO NA TABELA 
        {
            db.Livro.Add(oLivro);
            db.SaveChanges();
            return RedirectToAction("Livro");
        }

        // GET: LivroController/Edit/5
        public ActionResult Edit(int id)  // SELECIONA INFORMAÇÕES DO LIVRO NA  TABELA
        {
            Livro oCatLivro = db.Livro.Find(id);
            return View(oCatLivro);
        }

        // POST: LivroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Livro oLivro) // EDITAR INFORMAÇÕES DO LIVRO NA  TABELA
        {
            var oCatLivro = db.Livro.Find(id);
            oCatLivro.Id = oLivro.Id;
            oCatLivro.Nome = oLivro.Nome;
            oCatLivro.Autor = oLivro.Autor;
            oCatLivro.Editora = oLivro.Editora;
            oCatLivro.Emprestado = oLivro.Emprestado;

            db.Entry(oCatLivro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Livro");
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // GET: LivroController/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Livro oLivro = db.Livro.Find(id);
            if (Livro == null)
            {
                return HttpNotFound();
            }
            return View(oLivro);
        }

        // POST: LivroController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Livro oLivro = db.Livro.Find(id);
            if (oLivro == null)
            {
                return HttpNotFound();
            }
            db.Livro.Remove(oLivro);

            try
            {
                db.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Response.WriteAsync("<script>alert('Este livro possui emprestimos em seu nome!');{document.location.replace('../Livro')}</script>");
            }
            catch(Exception ex)
            {
                Response.WriteAsync("<script>alert('Ocorreu um erro interno!');{document.location.replace('../Livro')}</script>");
            }
            return RedirectToAction("Livro");
        }
    }
}
