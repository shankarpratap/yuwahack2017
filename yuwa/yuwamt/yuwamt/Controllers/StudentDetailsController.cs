using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using yuwamt;

namespace yuwamt.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using yuwamt;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<StudentDetail>("StudentDetails");
    builder.EntitySet<FamilyDetail>("FamilyDetails"); 
    builder.EntitySet<Sibling>("Siblings"); 
    builder.EntitySet<AttendanceRecord>("AttendanceRecords"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentDetailsController : ODataController
    {
        private YuwaEntities db = new YuwaEntities();

        // GET: odata/StudentDetails
        [EnableQuery]
        public IQueryable<StudentDetail> GetStudentDetails()
        {
            StudentDetail s = new StudentDetail() { id = 1, firstname = "name" };
            List<StudentDetail> l = new List<StudentDetail>();
            l.Add(s);
            return l.AsQueryable();

            //return db.StudentDetails;
        }

        // GET: odata/StudentDetails(5)
        [EnableQuery]
        public SingleResult<StudentDetail> GetStudentDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentDetails.Where(studentDetail => studentDetail.id == key));
        }

        // PUT: odata/StudentDetails(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<StudentDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentDetail studentDetail = db.StudentDetails.Find(key);
            if (studentDetail == null)
            {
                return NotFound();
            }

            patch.Put(studentDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentDetail);
        }

        // POST: odata/StudentDetails
        public IHttpActionResult Post(StudentDetail studentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDetails.Add(studentDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentDetailExists(studentDetail.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(studentDetail);
        }

        // PATCH: odata/StudentDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<StudentDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentDetail studentDetail = db.StudentDetails.Find(key);
            if (studentDetail == null)
            {
                return NotFound();
            }

            patch.Patch(studentDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentDetail);
        }

        // DELETE: odata/StudentDetails(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            StudentDetail studentDetail = db.StudentDetails.Find(key);
            if (studentDetail == null)
            {
                return NotFound();
            }

            db.StudentDetails.Remove(studentDetail);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentDetails(5)/FamilyDetail
        [EnableQuery]
        public SingleResult<FamilyDetail> GetFamilyDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentDetails.Where(m => m.id == key).Select(m => m.FamilyDetail));
        }

        // GET: odata/StudentDetails(5)/Siblings
        [EnableQuery]
        public IQueryable<Sibling> GetSiblings([FromODataUri] int key)
        {
            return db.StudentDetails.Where(m => m.id == key).SelectMany(m => m.Siblings);
        }

        // GET: odata/StudentDetails(5)/AttendanceRecords
        [EnableQuery]
        public IQueryable<AttendanceRecord> GetAttendanceRecords([FromODataUri] int key)
        {
            return db.StudentDetails.Where(m => m.id == key).SelectMany(m => m.AttendanceRecords);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentDetailExists(int key)
        {
            return db.StudentDetails.Count(e => e.id == key) > 0;
        }
    }
}
