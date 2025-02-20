using Microsoft.EntityFrameworkCore;
using Restaurant.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Entities
{
    [Table("role")]
   public class RoleEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("role_name")]
        public string Role_name { get; set; }

        // Связь с таблицей Пользователи
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
