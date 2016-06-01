using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PF_SOA.Models;

namespace PF_SOA.Controllers
{
    public class ArticuloController : ApiController
    {
        private ArticuloDBContext db = new ArticuloDBContext();

        // GET api/Articulo
        public IEnumerable<Articulo> GetPerson()
        {
            var data = db.Articulo.AsEnumerable();
            return data;
        }

        // GET api/Articulo/5
        public Articulo GetPerson(Int32 id)
        {
            Articulo person = db.Articulo.Find(id);
            if (person == null)
            {
               throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return person;
        }

        // PUT api/Articulo/5
        public HttpResponseMessage PutPerson(Int32 id, Articulo person)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != person.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Articulo
        public HttpResponseMessage PostPerson(Articulo person)
        {
            if (ModelState.IsValid)
            {
                db.Articulo.Add(person);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, person);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = person.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Articulo/5
        public HttpResponseMessage DeletePerson(Int32 id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Articulo.Remove(articulo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, articulo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}