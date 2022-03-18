using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
      
        public string Description { get; set; }
        public string Place { get; set; }

        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public int Spots_available { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

        [ForeignKey("Organizer_Id")]
        public Organizer Organizer { get; set; }

    }
}
