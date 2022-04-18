using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Playground.Data;
using Playground.Model;
using Playground.Utilities;

namespace Playground.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<Customer> Customers { get; set; }
        internal const int RECORDS = 3;
        public static int PAGE = 1;
        public readonly int NumOfPages;


        public CustomerModel(ApplicationDbContext context)
        {
                _context = context;
                NumOfPages = Utilities.NumberOfPages.CountPages(_context.Customers.Count(), RECORDS);
        }
        public void OnGet(int? num)
        {
            Customers = _context.Customers.ToList();
            if (num == null)
            {
                num = 1;
            }
            else if (PAGE < NumOfPages || PAGE > 1)
            {
                PAGE += (int)num;
            }
            Customers = _context.Customers.AsNoTracking().Paginate(PAGE, RECORDS).ToList();
        }


        public IActionResult OnGetSeed()
        {
            SeedContext();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetDelete()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Customers]");

            }catch(DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return Page();
            }
            return RedirectToPage();
        }



        private void SeedContext()
        {
            Customer max = new Customer()
            {
                FirstName = "Max",
                LastName = "Mustermann",
                DoB = new DateTime(1980, 4, 20),
                City = "Hamburg"
            };
            Customer tom = new Customer()
            {
                FirstName = "Tom",
                LastName = "Moritz",
                DoB = new DateTime(1990, 8, 10),
                City = "Hamburg"
            };
            Customer saskia = new Customer()
            {
                FirstName = "Saskia",
                LastName = "Klein",
                DoB = new DateTime(1998, 10, 8),
                City = "Hamburg"
            };
            Customer anna = new Customer()
            {
                FirstName = "Anna",
                LastName = "Schulz",
                DoB = new DateTime(2000, 2, 9),
                City = "Köln"
            };
            Customer hendrik = new Customer()
            {
                FirstName = "Hendrik",
                LastName = "Peterson",
                DoB = new DateTime(1979, 7, 11),
                City = "Köln"
            };
            Customer claudia = new Customer()
            {
                FirstName = "Claudia",
                LastName = "Musterfrau",
                DoB = new DateTime(1999, 1, 10),
                City = "Buxtehude"
            };
            Customer john = new Customer()
            {
                FirstName = "John",
                LastName = "Doe",
                DoB = new DateTime(1980, 1, 1),
                City = "Stade"
            };
            Customer bob = new Customer()
            {
                FirstName = "Bob",
                LastName = "Hero",
                DoB = new DateTime(1997, 4, 10),
                City = "Wismar"
            };


            _context.Customers.Add(max);
            _context.Customers.Add(tom);
            _context.Customers.Add(saskia);
            _context.Customers.Add(anna);
            _context.Customers.Add(hendrik);
            _context.Customers.Add(claudia);
            _context.Customers.Add(john);
            _context.Customers.Add(bob);

            try
            {
                _context.SaveChanges();
            }catch(DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
