using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBellaVista.Models;

namespace PruebaBellaVista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipocafesController : ControllerBase
    {
        private readonly BellaVistaDbContext _context;

        public TipocafesController(BellaVistaDbContext context)
        {
            _context = context;
        }

        // GET: api/Tipocafes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipocafe>>> GetTipocaves()
        {
            return await _context.Tipocaves.ToListAsync();
        }

        // GET: api/Tipocafes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipocafe>> GetTipocafe(int id)
        {
            var tipocafe = await _context.Tipocaves.FindAsync(id);

            if (tipocafe == null)
            {
                return NotFound();
            }

            return tipocafe;
        }

        // PUT: api/Tipocafes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipocafe(int id, Tipocafe tipocafe)
        {
            if (id != tipocafe.IdTipocafe)
            {
                return BadRequest();
            }

            _context.Entry(tipocafe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipocafeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tipocafes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipocafe>> PostTipocafe(Tipocafe tipocafe)
        {
            _context.Tipocaves.Add(tipocafe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipocafe", new { id = tipocafe.IdTipocafe }, tipocafe);
        }

        // DELETE: api/Tipocafes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipocafe(int id)
        {
            var tipocafe = await _context.Tipocaves.FindAsync(id);
            if (tipocafe == null)
            {
                return NotFound();
            }

            _context.Tipocaves.Remove(tipocafe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipocafeExists(int id)
        {
            return _context.Tipocaves.Any(e => e.IdTipocafe == id);
        }
    }
}
