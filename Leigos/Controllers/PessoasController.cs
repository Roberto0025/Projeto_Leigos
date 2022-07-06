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

namespace Leigos.Controllers
{
    //Só apos autorização tem acesso a todos os metodos
    [Authorize]
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            //Where... a pessoa só vê os propios servicos, tirando Where(u=> u.IdPessoa == User.Identity.Name) todos os serviços são visiveis a todos
            return _context.Pessoas != null ?
                          View(await _context.Pessoas.AsNoTracking().Where(p => p.EmailPessoa == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pessoa'  is null.");
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pessoas == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            ViewBag.Gen = new SelectList(_context.Generos, "GeneroId", "GeneroNome");
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,NomePessoa,TelefonePessoa,SenhaPessoa,GeneroId,Cep,Rua,Numero,Bairro,Cidade,UF")] Pessoa pessoa)
        {
            //pega o email da possoa logado
            pessoa.EmailPessoa = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Gen = new SelectList(_context.Generos, "GeneroId", "GeneroNome");

            if (id == null || _context.Pessoas == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            //usuario só tem acesso aos propios servicos cadastrados
            if (pessoa.EmailPessoa != User.Identity.Name)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaId,NomePessoa,TelefonePessoa,SenhaPessoa,GeneroId,Cep,Rua,Numero,Bairro,Cidade,UF")] Pessoa pessoa)
        {
            //pega o email da possoa logado
            pessoa.EmailPessoa = User.Identity.Name;

            if (id != pessoa.PessoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //pode atualizar tudp menos o propio email
                    pessoa.EmailPessoa = User.Identity.Name;

                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaId))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pessoas == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            //só pode apagar o propio cadastro
            if (pessoa.EmailPessoa != User.Identity.Name)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pessoas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pessoas'  is null.");
            }
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
          return (_context.Pessoas?.Any(e => e.PessoaId == id)).GetValueOrDefault();
        }
    }
}
