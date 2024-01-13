using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages.Estates
{
    public class CreateModel : TypesNamePageModel
    {
        private readonly RealEstate.Data.RealEstateContext _context;

        public CreateModel(RealEstate.Data.RealEstateContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            PopulateTypesDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Estate Estate { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            
            var emptyEstate = new Estate();

            if(await TryUpdateModelAsync<Estate>(
                emptyEstate,
                "estate",
                e=>e.Name, e=>e.Price, e=> e.BedRooms, e=> e.BathRooms, e=> e.SquareFeet, e=> e.TypeId))
            {

                _context.Estate.Add(emptyEstate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            PopulateTypesDropDownList(_context, emptyEstate.TypeId);
            return Page();

           
        }
    }
}
