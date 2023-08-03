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
    public class InstructorController : ApiController
    {
        private Mobile2Entities db = new Mobile2Entities();

        // GET: api/Instructor
        public IHttpActionResult Get()
        {
            return Ok(db.Instructors.Select(x => new InstructorView() {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                Photo = x.Photo,
            }).ToList());
        }

        // GET: api/Instructor/5
        [ResponseType(typeof(Instructors))]
        public IHttpActionResult Get(long id)
        {
            return Ok(db.Instructors.Select(x => new InstructorView() {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                Photo = x.Photo,
            }).FirstOrDefault());
        }

        // PUT: api/Instructor/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(long id, InstructorView instructorView)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Instructors instructors = new Instructors() {
                FirstName = instructorView.FirstName,
                LastName = instructorView.LastName,
                Id = instructorView.Id,
                Photo = instructorView.Photo,
            };
            if (id != instructors.Id)
            {
                return BadRequest();
            }

            db.Entry(instructors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorsExists(id))
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

        // POST: api/Instructor
        [ResponseType(typeof(Instructors))]
        public IHttpActionResult Post(InstructorView instructorView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Instructors instructors = new Instructors() {
                FirstName = instructorView.FirstName,
                LastName = instructorView.LastName,
                Id = instructorView.Id,
                Photo = instructorView.Photo,
            };
            db.Instructors.Add(instructors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instructors.Id }, instructors);
        }

        // DELETE: api/Instructor/5
        [ResponseType(typeof(Instructors))]
        public IHttpActionResult DeleteInstructors(long id)
        {
            Instructors instructors = db.Instructors.Find(id);
            if (instructors == null)
            {
                return NotFound();
            }

            db.Instructors.Remove(instructors);
            db.SaveChanges();

            return Ok(instructors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorsExists(long id)
        {
            return db.Instructors.Count(e => e.Id == id) > 0;
        }
    }
}