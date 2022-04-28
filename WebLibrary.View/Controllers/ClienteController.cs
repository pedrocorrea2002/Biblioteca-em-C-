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
            Cliente oCliente = db.Cliente.Find(id);
            return View(oCliente);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id,Cliente oCliente)
        {
            List<Cliente> oLista = db.Cliente.ToList();
            var cpfExiste = false;

            oLista.ForEach(c =>
            {
                if(c.Cpf == oCliente.Cpf)
                {
                    cpfExiste = true;
                }
            });

            if (!cpfExiste)
            {
                db.Cliente.Add(oCliente);

                try
                {
                    db.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    Response.WriteAsync("<script>alert('Este Cliente possui emprestimos em seu nome!');{document.location.replace('../Cliente')}</script>");
                }
            }
            else
            {
                Response.WriteAsync("<script>alert('Este CPF ja foi registrado!');document.location.replace('./Create');</script>");
            }
            
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
        public ActionResult Edit(int id, Cliente oCliente)
        {
            List<Cliente> oLista = db.Cliente.ToList();
            var cpfExiste = false;

            oLista.ForEach(c =>
            {
                if (c.Cpf == oCliente.Cpf&&id !=c.Id)
                {
                    cpfExiste = true;
                }
            });
            if (!cpfExiste)
            {
                var oCatBanco = db.Cliente.Find(id);

                oCatBanco.Cpf = oCliente.Cpf;
                oCatBanco.Nome = oCliente.Nome;
                oCatBanco.Endereco = oCliente.Endereco;
                oCatBanco.Cidade = oCliente.Cidade;
                oCatBanco.Bairro = oCliente.Bairro;


                db.Entry(oCatBanco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    Response.WriteAsync("<script>alert('ERRO iNTERNO!');document.location.replace('./Edit');</script>");
                }
                return RedirectToAction("Cliente");
            }
            else
            {
                Response.WriteAsync("<script>alert('Este CPF ja foi registrado!');document.location.replace('./Edit');</script>");
            }

            return RedirectToAction("Cliente");








           
        }




        public ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            Cliente oCliente = db.Cliente.Find(id);
            return View(oCliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Cliente oCliente = db.Cliente.Find(id);

            db.Cliente.Remove(oCliente);

            try
            {
                db.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Response.WriteAsync("<script>alert('Este Cliente possui emprestimos em seu nome!');{document.location.replace('../Cliente')}</script>");
            }
            catch (Exception ex)
            {
                Response.WriteAsync("<script>alert('Ocorreu um erro interno!');{document.location.replace('../Cliente')}</script>");
            }
            return RedirectToAction("Cliente");
        
        }
    }
}
