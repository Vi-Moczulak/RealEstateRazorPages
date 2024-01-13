using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages.Types
{
    public class DeleteModel : PageModel
    {
        private readonly RealEstate.Data.RealEstateContext _context;

        public DeleteModel(RealEstate.Data.RealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Type Type { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = await _context.Type.FirstOrDefaultAsync(m => m.Id == id);

            if (type == null)
            {
                return NotFound();
            }
            else
            {
                Type = type;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = await _context.Type.FindAsync(id);
            if (type != null)
            {
                Type = type;
                _context.Type.Remove(Type);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
