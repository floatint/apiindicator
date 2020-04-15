using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO.Auth
{
    public class NewUser
    {
        [Required(ErrorMessage = "Введите логин пользователя")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Name { set; get; }
        public long? RoleID { set; get; }
    }
}
