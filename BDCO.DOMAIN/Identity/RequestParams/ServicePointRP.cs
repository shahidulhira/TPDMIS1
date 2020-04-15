namespace BDCO.Domain.RequestParams
{
    public class ServicePointRM
    {
        public string UserType { get; set; }
    }
    public class ServicePointView
    {
        public string UserType { get; set; }
    }
    public class ServicePointForPermission
    {
        public int? UserId { get; set; }
        public string UserType { get; set; }
    }
}
