using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Playground.Data;
using Playground.Model;

namespace Playground.Pages
{
    public class SwitchModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<Player> AllPlayers { get; set; }
        public SwitchModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public void OnGet()
        {
            AllPlayers = _context.Players.ToList();
        }

        [BindProperty]
        public Player Player { get; set; }
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                AllPlayers = await _context.Players.ToListAsync();
                return Page();
            }
            if (ModelState.IsValid)
            {
                _context.Players.Add(Player);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("Player.Name", "Cant save!!");
                }

            }
            return RedirectToPage();

        }

        public async Task<IActionResult> OnGetMove(int id)
        {
            if (id == 0)
            {
                return Page();
            }

            Player player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }
            player.IsAPlayer = !player.IsAPlayer;
            _context.Players.Attach(player).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("Player.Name", "Cant change IsAplayer");
            }

            return RedirectToPage();
        }
    }
}

