using Financas.DAO;
using Financas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financas.Controllers
{
    public class MovimentacaoController : Controller
    {
        private MovimentacaoDAO movimentacaoDAO;
        private UsuarioDAO usuarioDAO;

        public MovimentacaoController(MovimentacaoDAO movimentacaoDAO, UsuarioDAO usuarioDAO)
        {
            this.movimentacaoDAO = movimentacaoDAO;
            this.usuarioDAO = usuarioDAO;
        }

        public ActionResult Form()
        {
            ViewBag.Usuarios = usuarioDAO.Lista();
            return View();
        }

        public ActionResult Adiciona(Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacaoDAO.Adiciona(movimentacao);
                return RedirectToAction("Index");
            }
            ViewBag.Usuarios = usuarioDAO.Lista();
            return View("Form", movimentacao);
        }
        // GET: Movimentacao
        public ActionResult Index()
        {
            IList<Movimentacao> movimentacao = movimentacaoDAO.Lista();
            return View(movimentacao);
        }
    }
}