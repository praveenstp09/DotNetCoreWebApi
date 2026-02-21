namespace FirstWebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? Age { get; set; } 

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public ICollection<UserRoleModel>? UserRoles { get; set; }

    }
}
