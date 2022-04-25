using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Model;
namespace WebLibrary.View.Controllers
{
    public class LibraryController : Controller
    {
        Context db;
        public LibraryController() // Criando uma construtora
        {
            db = new Context(); //Construindo o "context"
        }
        // GET: LibraryController
        public ActionResult Index() // Atenção aqui qdo tiver criando os menus de acesso as paginas
        {
            return View();
        }
        public ActionResult Cliente() // Atenção aqui qdo tiver criando os menus de acesso as paginas
        {
            List <Cliente> oLista = db.Cliente.ToList(); // listando tabela "cliente" e colocando em "oList"
            return View(oLista); // E vai retornar "oLista"
        }
        //public ActionResult Livro()
        //{
        //    List<Livro> oLista = db.Livro.ToList();
        //    return View(oLista);
        //}
        //public ActionResult Emprestimo()
        //{
        //    List<LivroClienteEmprestimo> oLista = db.LivroClienteEmprestimo.ToList();
        //    return View(oLista);
        //}

        // GET: LibraryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibraryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibraryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente oCat) // é aqui que se altera!!!!!!!!!!!!! NÃO EM CIMA!!!!!!
        {
            db.Cliente.Add(oCat);
            db.SaveChanges();
            return RedirectToAction("Cliente");

        }

        // GET: LibraryController/Edit/5
        public ActionResult Edit(int id)
        {
           Cliente oCat = db.Cliente.Find(id);
            return View(oCat);            
        }

        // POST: LibraryController/Edit/5
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
            
            db.SaveChanges();
            return RedirectToAction("Cliente");
        }

        // GET: LibraryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibraryController/Delete/5
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
