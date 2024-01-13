using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        //sort
        public string Order {  get; set; }  

        public async Task OnGetAsync(string sortOrder, string sortField)
        {
            Order = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder.Equals("desc") ? "asc": "desc"; 


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

            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.Equals("asc")){
                switch (sortField)
                {
                    case "name":
                        estates = estates.OrderBy(x => x.Name);
                        break;
                    case "price":
                        estates = estates.OrderBy(x => x.Price);
                        break;
                    case "bedRooms":
                        estates = estates.OrderBy(x => x.BedRooms);
                        break;
                    case "bathRooms":
                        estates = estates.OrderBy(x => x.BathRooms);
                        break;
                    case "squareFeet":
                        estates = estates.OrderBy(x => x.SquareFeet);
                        break;
                    default:
                        estates = estates.OrderBy(x => x.Name);
                        break;
                }
            } else
            {
                switch (sortField)
                {
                    case "name":
                        estates = estates.OrderByDescending(x => x.Name);
                        break;
                    case "price":
                        estates = estates.OrderByDescending(x => x.Price);
                        break;
                    case "bedRooms":
                        estates = estates.OrderByDescending(x => x.BedRooms);
                        break;
                    case "bathRooms":
                        estates = estates.OrderByDescending(x => x.BathRooms);
                        break;
                    case "squareFeet":
                        estates = estates.OrderByDescending(x => x.SquareFeet);
                        break;
                    default:
                        estates = estates.OrderByDescending(x => x.Name);
                        break;
                }
            }

            Types = new SelectList(await typeQuery.Distinct().ToListAsync());
            Estate = await estates.ToListAsync();

        }
    }
}
