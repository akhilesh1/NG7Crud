using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NGCrud
{
    public class EmployeesController : ApiController
    {
        // GET api/<controller>
        private EmployeeDBEntities empEntities;
        EmployeesController()
        {
            EmployeeDBEntities empEntities = new EmployeeDBEntities();
        }
        public IQueryable<Employee> Get()
        {
            return empEntities.Employees;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            if (empEntities.Employees.Any(e => e.Id == id))
            {
                return Ok(empEntities.Employees.Where(e => e.Id == id).FirstOrDefault());
            }
            else
                return NotFound();
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empEntities.Employees.Add(emp);
                    empEntities.SaveChanges();

                }
                else
                {
                    BadRequest(ModelState);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(emp);

        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]Employee emp)
        {
            Employee objEmp=new Employee();
            try
            {
                if (ModelState.IsValid)
                {
                    objEmp = empEntities.Employees.Find(emp.Id);
                    if (objEmp != null)
                    {
                        objEmp.Name = emp.Name;
                        objEmp.Address = emp.Address;
                        objEmp.Email = emp.Email;
                        objEmp.DateOfBirth = emp.DateOfBirth;
                        objEmp.Gender = emp.Gender;
                        empEntities.SaveChanges();
                    }
                    else
                        return NotFound();
                }
                else
                {
                    BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(objEmp);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Employee employee = empEntities.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            empEntities.Employees.Remove(employee);
            empEntities.SaveChanges();
            return Ok(employee);
        }
    }
}