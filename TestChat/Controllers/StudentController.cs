using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TestChat.Models;
using TestChat.Models.view;

namespace TestChat.Controllers
{
    public class StudentController : ApiController
    {
        // GET: api/Student
        Mobile2Entities db = new Mobile2Entities(); 
        public IHttpActionResult Get()
        {
            return Ok(db.Students.ToList().Select(x=> new StudentView() {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,  
                Photo= x.Photo,
            }).ToList());
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            return Ok(db.Students.Select(x => new StudentView() {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                Photo = x.Photo,
            }).FirstOrDefault());
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody]StudentView student)
        {
            try {
                Students students = new Students() {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Photo = student.Photo,
                };
                db.Students.Add(students);
                return CreatedAtRoute("DefaultApi", new { id = students.Id }, students);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody]StudentView student)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != student.Id) {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                if (!Exists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            Students students = db.Students.Find(id);
            if (students == null) {
                return NotFound();
            }

            db.Students.Remove(students);
            db.SaveChanges();

            return Ok(students);
        }

        private bool Exists(long id) {
            return db.Students.Count(e => e.Id == id) > 0;
        }
    }
}
