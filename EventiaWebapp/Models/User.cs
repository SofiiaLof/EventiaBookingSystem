using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Models
{
    public class User : IdentityUser
    {
     
        public string? First_name { get; set; }
        public string? Last_name { get; set; }


        [InverseProperty("Organizer")]
        public List<Event> HostedEvents { get; set; }

        [InverseProperty("Attendees")]
        public IList<Event> JoinedEvents { get; set; }
    }
}
