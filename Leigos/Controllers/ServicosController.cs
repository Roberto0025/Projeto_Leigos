using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leigos.Data;
using Leigos.Models;
using Microsoft.AspNetCore.Authorization;
using Leigos.ViewModels;

namespace Leigos.Controllers
{
    //Só apos autorização tem acesso a todos os metodos
    [Authorize]
    public class ServicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            //Where... a pessoa só vê os propios servicos, tirando Where(u=> u.IdPessoa == User.Identity.Name) todos os serviços são visiveis a todos
            return _context.Servicos != null ?
                          View(await _context.Servicos.AsNoTracking().Where(p => p.EmailPessoa == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pessoa'  is null.");
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            ViewBag.Cat = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            //ViewBag.Pes = new SelectList(_context.Pessoas, "idPessoa", "emailPessoa");
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoId,NomeServico,DescricacaoServico,CategoriaId,NotaServico,Imagem")] Servico servico)
        {
            servico.EmailPessoa = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Cat = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            //ViewBag.Pes = new SelectList(_context.Pessoas, "idPessoa", "nomePessoa");

            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoId,NomeServico,DescricacaoServico,CategoriaId,EmailPessoa,NotaServico,Imagem")] Servico servico)
        {
            servico.EmailPessoa = User.Identity.Name;
            if (id != servico.ServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.ServicoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Servicos'  is null.");
            }
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
          return (_context.Servicos?.Any(e => e.ServicoId == id)).GetValueOrDefault();
        }

        //todos pode ver a lista de serviços cadastrados
        [AllowAnonymous]
        public IActionResult List()
        {
            var servicosListaViewModel = new ServicosListViewModel();
            servicosListaViewModel.Servicos = _context.Servicos;

            return View(servicosListaViewModel);
        }
    }
}
