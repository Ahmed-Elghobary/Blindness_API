using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Color_blindeens.Data;
using Color_blindeens.Models;

namespace Color_blindeens.Views.USER
{
    public class IndexModel : PageModel
    {
        private readonly Color_blindeens.Data.Color_blindeensContext _context;

        public IndexModel(Color_blindeens.Data.Color_blindeensContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
