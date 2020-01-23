using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class UpdatedUser
    {
        [Required(ErrorMessage = "User login not defined")]
        public string Login { set; get; }
        public long? RoleId { set; get; }
    }
}
