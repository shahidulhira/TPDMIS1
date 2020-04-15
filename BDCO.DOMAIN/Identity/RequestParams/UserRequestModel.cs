using BDCO.Domain.Identity;

namespace BDCO.Domain.RequestModels
{
    public class UserRM
    {
        public AspNetUsers objUser { get; set; }
        public int UserId { get; set; }
    }
}
