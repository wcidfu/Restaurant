using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Restaurant.Entities
{
    [Table("user")]
    public class UserEntity
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("role_id")]
        public int Role_id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("failed_login_attempts")]
        public int FailedLoginAttempts { get; set; }

        [Column("blocked_until")]
        public DateTime? BlockedUntil { get; set; }

        // Cвязь с таблицей Роли

        [ForeignKey("Role_id")]
        public virtual RoleEntity Roles { get; set; }

    }

}
