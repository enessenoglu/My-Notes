using Notlarim102.BusinessLayer.Abstract;
using Notlarim102.Common.Helper;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.Massages;
using Notlarim102.Entity.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class NotlarimUserManager : ManagerBase<NotlarimUser>
    {
        BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = Find(s => s.UserName == data.UserName || s.Email == data.Email);

            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanıcı adı daha önce kullanılmış");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Email daha önce kullanılmış");
                }
                // throw new Exception("Username yada email kullanılmış");
            }
            else
            {
                int dbresult = base.Insert(new NotlarimUser()
                {
                    UserName = data.UserName,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    ProfileImageFile = "user1.jpg"
                    //Kapatılanlar repositoryde otomatik şekilde eklenecek şekilde düzenlenecek
                    //ModifiedOn=DateTime.Now,
                    //CreatedOn=DateTime.Now,
                    //ModifiedUserName="System"
                });
                if (dbresult > 0)
                {
                    res.Result = Find(s => s.Email == data.Email && s.UserName == data.UserName);
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba{res.Result.UserName};<br><br> Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>Tıklayın</a>.";
                    MailHelper.SendMail(body, res.Result.Email, "Notlarım102 hesap aktifleştirme");
                }
            }
            return res;
        }
        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giriş kontrolü
            //hesap aktif mii kontrolü
            //yonlendirme
            //sesiona kullanıcı bilgilerini gönderme

            res.Result = Find(s => s.UserName == data.Username && s.Password == data.Password);
            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.CheckYourEmail, " Lütfen mailinizi kontrol edin");
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktif değil.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre hatalı");
            }
            return res;
        }
        public BusinessLayerResult<NotlarimUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<NotlarimUser> user = new BusinessLayerResult<NotlarimUser>();
            user.Result = Find(x => x.ActivateGuid == id);
            if (user.Result != null)
            {
                if (user.Result.IsActive)
                {
                    user.AddError(ErrorMessageCode.UserAlreadyActive, "Bu hesap zaten aktif");
                    return user;
                }
                user.Result.IsActive = true;
                Update(user.Result);
            }
            else
            {
                user.AddError(ErrorMessageCode.ActivateIdDoesNotExist, "Kodunuzu kontrol ediniz");
            }
            return user;
        }
        public BusinessLayerResult<NotlarimUser> GetUserById(int id)
        {

            res.Result = Find(s => s.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }
            return res;
        }
        public BusinessLayerResult<NotlarimUser> UpdateProfile(NotlarimUser data)
        {
            NotlarimUser user = Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));

            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanıcı adı daha önce kullanılmış.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu e-posta adresi daha önce kullanılmış.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            if (!string.IsNullOrEmpty(data.ProfileImageFile))
            {
                res.Result.ProfileImageFile = data.ProfileImageFile;
            }
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdate, "Profil güncellenemedi");
            }
            return res;
        }
        public BusinessLayerResult<NotlarimUser> DeleteProfile(int id)
        {
            NotlarimUser user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Silme işleminiz başarısız");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı.");
            }
            return res;
        }
        public new BusinessLayerResult<NotlarimUser> Insert(NotlarimUser data)
        {
            NotlarimUser user = Find(s => s.UserName == data.UserName || s.Email == data.Email);
            res.Result = data;
            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanıcı adı daha önce kullanılmış");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Email daha önce kullanılmış");
                }
                // throw new Exception("Username yada email kullanılmış");
            }
            else
            {
                res.Result.ProfileImageFile = "user1.jpg";
                res.Result.ActivateGuid = Guid.NewGuid();
                if (base.Insert(res.Result) == 0)
                {

                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi");
                }
               
            }
            return res;
        }
        public new BusinessLayerResult<NotlarimUser> Update(NotlarimUser data)
        {
            NotlarimUser user = Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));

            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanıcı adı daha önce kullanılmış.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu e-posta adresi daha önce kullanılmış.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsActive = data.IsActive;
            res.Result.IsActive = data.IsAdmin;
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "User güncellenemedi");
            }
            return res;
        }
    }
}
