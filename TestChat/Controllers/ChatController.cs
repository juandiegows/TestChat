using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestChat.Models;
using TestChat.Models.view;

namespace TestChat.Controllers
{
    [Authorize]
    public class ChatController : ApiController
    {
        private Mobile2Entities db = new Mobile2Entities();

        // GET: api/Chat
        public IHttpActionResult Get()
        {
            return Ok(db.Chats.Select(x=> new ChatView() {
                Id = x.Id,
                Date = x.Date,
                InstructorId = x.InstructorId,
                Message = x.Message,
                Sender = x.Sender  ,
                StudentId = x.StudentId
            }).ToList());
        }

        // GET: api/Chat/5
        [ResponseType(typeof(Chats))]
        public IHttpActionResult Get(long id)
        {
            return Ok(db.Chats.Select(x => new ChatView() {
                Id = x.Id,
                Date = x.Date,
                InstructorId = x.InstructorId,
                Message = x.Message,
                Sender = x.Sender,
                StudentId = x.StudentId
            }).FirstOrDefault());
        }

        // PUT: api/Chat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(long id, ChatView chatView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Chats chats = new Chats() {
                Id = chatView.Id,
                Date = chatView.Date,
                InstructorId = chatView.InstructorId,
                Message = chatView.Message,
                Sender = chatView.Sender,
                StudentId = chatView.StudentId
            };

            if (id != chats.Id)
            {
                return BadRequest();
            }

            db.Entry(chats).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Chat
        [ResponseType(typeof(Chats))]
        public IHttpActionResult PostChats(ChatView chatView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Chats chats = new Chats() {
                Id = chatView.Id,
                Date = chatView.Date,
                InstructorId = chatView.InstructorId,
                Message = chatView.Message,
                Sender = chatView.Sender,
                StudentId = chatView.StudentId
            };

            db.Chats.Add(chats);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chats.Id }, chats);
        }

        // DELETE: api/Chat/5
        [ResponseType(typeof(Chats))]
        public IHttpActionResult Delete(long id)
        {
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return NotFound();
            }

            db.Chats.Remove(chats);
            db.SaveChanges();

            return Ok(chats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatsExists(long id)
        {
            return db.Chats.Count(e => e.Id == id) > 0;
        }
    }
}