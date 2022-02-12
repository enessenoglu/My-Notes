using Notlarim102.BusinessLayer;
using Notlarim102.Common;
using Notlarim102.Entity;
using Notlarim102.Entity.Massages;
using Notlarim102.Entity.ValueObject;
using Notlarim102.WepApp.Init;
using Notlarim102.WepApp.Models;
using Notlarim102.WepApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notlarim102.WepApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NoteManager nm = new NoteManager();
        CategoryManager cm = new CategoryManager();
        NotlarimUserManager num = new NotlarimUserManager();
        BusinessLayerResult<NotlarimUser> res;
        public ActionResult Index()
        {
            //Test test = new Test();
            //// test.InsertTest();
            ////  test.UpdateTest();
            //// test.DeleteTest();
            //test.CommentTest();
            //if (TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
           
            return View(nm.QList().Where(s=>s.IsDraft==false).OrderByDescending(x=>x.ModifiedOn).ToList());
           // return View(nm.GetAllNoteQueryable());
        }
        public ActionResult ByCategory(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            
           
            List<Note> notes = nm.QList().Where(x=> x.IsDraft==false && x.CategoryId==Id ).OrderByDescending(x=> x.ModifiedOn).ToList();
           
            
            return View("Index", notes);
        }
        public ActionResult MostLiked()
        {
            
            return View("Index",nm.QList().OrderByDescending(x => x.likeCount).ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
              
                 res = num.LoginUser(model);
                if (res.Error.Count>0)
                {
                    if (res.Error.Find(x=>x.Code==Entity.Massages.ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = $"https://localhost:44328/Home/UserActivate/{res.Result.ActivateGuid}";
                    }
                    res.Error.ForEach(s => ModelState.AddModelError("",s.Message));
                    return View(model);
                }
              
                CurrentSession.Set("login",res.Result);
                return RedirectToAction("Index");
                App.Common = new WebCommon();
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanıcı adı uniq olmalıdır 
            //email kontrolü
            //activasyon işlemi
            //bool hasError = false;
            if (ModelState.IsValid)
            {
               
                 res = num.RegisterUser(model);
                if (res.Error.Count>0)
                {
                    res.Error.ForEach(s=>ModelState.AddModelError("",s.Message));
                    return View(model);
                }
                // NotlarimUser user = null;
                //try
                //{
                //    user = num.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}
                //    if (model.UserName=="aaa")
                //    {
                //        ModelState.AddModelError("","Bu username daha önce alınmış");
                //       // hasError = true;
                //    }
                //    if (model.Email=="aaa@aaa.com")
                //    {
                //        ModelState.AddModelError("", "Email adresi daha önce kullanılmış");
                //  //      hasError = true;
                //    }
                //    //if (hasError==true)
                //    //{
                //    //    return View(model);
                //    //}
                //    //else
                //    //{
                //    //    return RedirectToAction("RegisterOK");
                //    //}
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}
                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl="/Home/Login"

                };
                notifyObj.Items.Add("Lütfen e-posta adresinizde gönderilen aktivasyon linkine tıklayarak hesabınızı aktif edin.Hesabınızı aktif etmeden not ekleyemez veya beğeni yapamazsınız.");
                return View("Ok",notifyObj);
            }
            return View(model);
        }
        public ActionResult RegisterOK()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {
            
            BusinessLayerResult<NotlarimUser> res = num.ActivateUser(id);
            if (res.Error.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata",
                    Items = res.Error
                };
                return View("Error", errorNotifyObj);
            }
            OkViewModel notifyObj = new OkViewModel()
            {
                Title = "Hesabınız Aktifleştirildi",
                RedirectingUrl = "Home/Login"
            };
            notifyObj.Items.Add("Hesabınız Aktifleştirildi");
            return View("Ok",notifyObj);
        }
        public ActionResult UserActivateOk()
        {

            return View();
        }
        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObject> errors = null;
            if (TempData["errors"]!=null)
            {
                errors = TempData["errors"] as List<ErrorMessageObject>;
            }
            return View(errors);
        }
        public ActionResult ShowProfile()
        {
            //NotlarimUser currentUser = Session["login"] as NotlarimUser;            
            //res = num.GetUserById(currentUser.Id);
            if (CurrentSession.User is NotlarimUser currentUser) res = num.GetUserById(currentUser.Id);
            if (res.Error.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata",
                    Items = res.Error
                };
                return View("Error", errorNotifyObj);

            }
            return View(res.Result);
        }
        public ActionResult EditProfile()
        {
            //NotlarimUser currentUser = Session["login"] as NotlarimUser;            
            //res = num.GetUserById(currentUser.Id);
            if (CurrentSession.User is NotlarimUser currentUser) res = num.GetUserById(currentUser.Id);
            if (res.Error.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Error
                };
                return View("Error", errorNotifyObj);
            }
            return View(res.Result);
        }
        [HttpPost]
        public ActionResult EditProfile(NotlarimUser model,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage!=null &&(ProfileImage.ContentType=="image/jpeg"||ProfileImage.ContentType=="image/jpeg"||ProfileImage.ContentType=="image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    //user_5.png
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFile = filename;
                }
               
                 res = num.UpdateProfile(model);
                if (res.Error.Count>0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Title = "Hata oluştu",
                        Items = res.Error
                    };
                    return View("Error", errorNotifyObj);
                }
               
                CurrentSession.Set("login", res.Result);
                return RedirectToAction("ShowProfile");
               
            }
            return View(model);
        }
        public ActionResult DeleteProfile()
        {
            if (CurrentSession.User is NotlarimUser currentUser) res = num.DeleteProfile(currentUser.Id);
            if (res.Error.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Error
                };
                return View("Error", errorNotifyObj);
            }
            CurrentSession.Clear();
            return RedirectToAction("Index");
           
        }
        //[HttpPost]
        //public ActionResult DeleteProfile(int id)
        //{
        //    return View();
        //}
        public ActionResult Logout()
        {
            CurrentSession.Clear();
            return RedirectToAction("Index");
        }
    }
}