using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApplication.Compartilhado.Apresentacao
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

    }
}
