using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;

namespace RealEstate.Pages.Estates
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RealEstate.Data.RealEstateContext _context;

        public IndexModel(RealEstate.Data.RealEstateContext context)
        {
            _context = context;
        }

        public IList<Estate> Estate { get;set; } = default!;

        //search
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Types { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? EstateType { get; set; }

        public async Task OnGetAsync()
        {

            var estates = from e in _context.Estate
                         select e;

            IQueryable<string> typeQuery = from t in _context.Type
                                           orderby t.Name
                                           select t.Name;

            if (!string.IsNullOrEmpty(SearchString))
            {
                estates = estates.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(EstateType))
            {
                estates = estates.Where(x => x.Type != null && x.Type.Name == EstateType);
            }

            Types = new SelectList(await typeQuery.Distinct().ToListAsync());
            Estate = await estates.ToListAsync();

        }
    }
}
