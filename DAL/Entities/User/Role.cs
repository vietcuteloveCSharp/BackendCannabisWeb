using DAL.Entities.Inherited;

namespace DAL.Entities.User
{
    public class Role :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Role name is required.")]
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
        
	}
}
