using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notlarim102.WepApp.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        NoteManager nm = new NoteManager();
        CommentManager cm = new CommentManager();
        public ActionResult ShowNoteComments(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            Note note = nm.Find(s => s.Id == id);
            if (note==null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComments", note.Comments);
        }
    }
}