using SubwayEntrance.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Models
{
    public class SubwayUser : IEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Station { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitute { get; set; }

        public virtual ICollection<UserWithSubway> UserWithSubways { get; set; }
        
    }
}
