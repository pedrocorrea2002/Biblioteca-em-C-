using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebLibrary.View.Models;

namespace WebLibrary.View.Controllers
{
    public class LibraryController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult livro() // Método "Livro"
        {
            return View();
        }
        public IActionResult Cliente() // Método "Cliente"
        {
            return View();
        }
        public IActionResult Emprestimo() // Método "Emprestimo"
        {
            return View();
        }
    }
}

