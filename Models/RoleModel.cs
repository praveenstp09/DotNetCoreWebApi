namespace FirstWebAPI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = Roles.User.ToString();
        
        public ICollection<UserRoleModel>? UserRoles { get; set; }
    }

    public enum Roles
    {
        Admin,
        User
    }
}
