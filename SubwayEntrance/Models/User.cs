using SubwayEntrance.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Models
{
    public class User
    {
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<UserWithSubway> UserWithSubways { get; set; }
       
    }


    public class UserWithSubway:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int userId { get; set; }
        public User  Authenticate { get; set; }
        public int SubwayUserId { get; set; }
        public SubwayUser  SubwayUser { get; set; }
        
    }
}
