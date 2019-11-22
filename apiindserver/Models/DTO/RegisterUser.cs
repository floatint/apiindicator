﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Введите логин пользователя")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        public long? RoleID { set; get; }
    }
}