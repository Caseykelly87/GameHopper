using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public string DMUserId { get; set; }
        public User DMUser { get; set; }

        [Required]
        public DateTime SessionDateTime { get; set; }

        [Required]
        public string RecurrencePattern { get; set; } // e.g., "weekly", "biweekly", "monthly"

        public DayOfWeek RecurrenceDayOfWeek { get; set; } // 0 (Sunday) to 6 (Saturday)
        public TimeSpan RecurrenceTime { get; set; }

        public DateTime NextSessionDateTime { get; set; }
    }
}
