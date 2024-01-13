using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages.Estates
{
    public class EditModel : TypesNamePageModel
    {
        private readonly RealEstate.Data.RealEstateContext _context;

        public EditModel(RealEstate.Data.RealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Estate Estate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Estate =  await _context.Estate.Include(t => t.Type).FirstOrDefaultAsync(m => m.Id == id);
            if (Estate == null)
            {
                return NotFound();
            }

            PopulateTypesDropDownList(_context, Estate.TypeId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateToUpdate = await _context.Estate.FindAsync(id);

            if (estateToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Estate>(
                 estateToUpdate,
                 "estate",
                   e => e.Name,e => e.Price, e=> e.BedRooms, e=> e.BathRooms, e=> e.BedRooms, e=>e.SquareFeet, e => e.TypeId)
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateTypesDropDownList(_context, estateToUpdate.TypeId);
            return Page();
            
        }

        private bool EstateExists(int id)
        {
            return _context.Estate.Any(e => e.Id == id);
        }
    }
}
