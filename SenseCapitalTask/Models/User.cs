using Microsoft.AspNetCore.Identity;

namespace SenseCapitalTask.Models
{
    public class User : IdentityUser
    {
        List<Invitation> Invitations { get; set; }
    }
}
