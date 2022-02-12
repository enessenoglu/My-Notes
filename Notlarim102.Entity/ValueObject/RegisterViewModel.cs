using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim102.Entity.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("User Name"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(30, ErrorMessage = "{0} max.{1} karakter olmalı.")]
        public string UserName { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(100, ErrorMessage = "{0} max.{1} karakter olmalı."),EmailAddress(ErrorMessage ="{0} alanının doğru olup olmadığını kontrol ediniz.")]
        public string Email { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(30, ErrorMessage = "{0} max.{1} karakter olmalı.")]
        public string Password { get; set; }
        [DisplayName("RePassword"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(30, ErrorMessage = "{0} max.{1} karakter olmalı."),Compare("Password",ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }
       
    }
}