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
        public IEnumerable<Articulo> GetArticulo()
        {
            var data = db.Articulo.AsEnumerable();
            return data;
        }

        // GET api/Articulo/5
        public Articulo GetArticulo(Int32 id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
               throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return articulo;
        }

        // PUT api/Articulo/5
        public HttpResponseMessage PutArticulo(Int32 id, Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != articulo.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(articulo).State = EntityState.Modified;

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
        public HttpResponseMessage PostArticulo(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Articulo.Add(articulo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, articulo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = articulo.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Articulo/5
        public HttpResponseMessage DeleteArticulo(Int32 id)
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