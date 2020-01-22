using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ERP_Api.Models;

namespace ERP_Api.Controllers
{
    public class ContratsController : ApiController
    {
        private ERP_WEB_CLIENTEntities db = new ERP_WEB_CLIENTEntities();

        // GET: api/Contrats
        public IQueryable<Contrat> GetContrats()
        {
            return db.Contrats;
        }

        // GET: api/Contrats/5
        [ResponseType(typeof(Contrat))]
        public async Task<IHttpActionResult> GetContrat(int id)
        {
            Contrat contrat = await db.Contrats.FindAsync(id);
            if (contrat == null)
            {
                return NotFound();
            }

            return Ok(contrat);
        }

        // PUT: api/Contrats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContrat(int id, Contrat contrat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contrat.Id)
            {
                return BadRequest();
            }

            db.Entry(contrat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratExists(id))
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

        // POST: api/Contrats
        [ResponseType(typeof(Contrat))]
        public async Task<IHttpActionResult> PostContrat(Contrat contrat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contrats.Add(contrat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contrat.Id }, contrat);
        }

        // DELETE: api/Contrats/5
        [ResponseType(typeof(Contrat))]
        public async Task<IHttpActionResult> DeleteContrat(int id)
        {
            Contrat contrat = await db.Contrats.FindAsync(id);
            if (contrat == null)
            {
                return NotFound();
            }

            db.Contrats.Remove(contrat);
            await db.SaveChangesAsync();

            return Ok(contrat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContratExists(int id)
        {
            return db.Contrats.Count(e => e.Id == id) > 0;
        }
    }
}