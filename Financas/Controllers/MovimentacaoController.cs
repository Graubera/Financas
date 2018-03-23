using Financas.DAO;
using Financas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financas.Controllers
{
    [Authorize]
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
                //movimentacao.Usuario.Nome = User.Identity.ToString();
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

        public ActionResult MovimentacoesPorUsuario(MovimentacoesPorUsuarioModel model)
        {
            model.Usuarios = usuarioDAO.Lista();
            model.Movimentacoes = movimentacaoDAO.BuscaPorUsuario(model.UsuarioId);
            return View(model);
        }

        public ActionResult Busca(BuscaMovimentacoesModel model)
        {
            model.Usuarios = usuarioDAO.Lista();
            model.Movimentacoes = movimentacaoDAO.Busca(model.ValorMinimo, model.ValorMaximo,
                                    model.DataMinima, model.DataMaxima,
                                    model.Tipo, model.UsuarioId);
            return View(model);
        }
    }
}