using System.ComponentModel.DataAnnotations;

namespace Playground.Model
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Need a player name")]
        public String Name { get; set; }

        public Boolean IsAPlayer { get; set; }

    }
}
