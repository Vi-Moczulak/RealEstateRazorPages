using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;

namespace RealEstate.Pages.Estates
{
    public class TypesNamePageModel : PageModel

    {
        public SelectList TypeNameSL { get; set; }

        public void PopulateTypesDropDownList(RealEstateContext _context, Object selectedType = null)
        {
            var typeQuery = from t in _context.Type 
                               orderby t.Name
                               select t;

            TypeNameSL = new SelectList(typeQuery.AsNoTracking(),
                                            nameof(Models.Type.Id),
                                            nameof(Models.Type.Name),
                                            selectedType);
        }
    }
}
