using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim102.Entity.ValueObject
{
    public class LoginViewModel
    {   [DisplayName("User Name"),Required(ErrorMessage ="{0} alanı boş geçilemez"),StringLength(30,ErrorMessage ="{0} max.{1} karakter olmalı.")]
        public string Username { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "{0} alanı boş geçilemez"),DataType(DataType.Password), StringLength(30, ErrorMessage = "{0} max.{1} karakter olmalı.")]
        public string Password { get; set; }
    }
}