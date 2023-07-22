using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab06.Models.DataAccess;
using Lab7.Models.DataAccess;

namespace Lab06.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StudentRecordContext _context;
        public bool ViewBagEmpty = false;
        public bool userDoesExist = false;
        public EmployeesController(StudentRecordContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
            
            var employeeRolesContext= _context.Employees.Include(a => a.Roles);
            var employee_Roles = await _context.Roles.ToListAsync();

            return View(await employeeRolesContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var roleContext = _context.Roles;
            ViewData["Roles"] = await roleContext.ToListAsync();
            ViewData["CheckedBox"] = null;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            string[] selectedRoles = Request.Form["checkBoxList"];

            var roles = _context.Roles.Where(r => selectedRoles.Contains(r.Role1));
            employee.Roles  = await  roles.ToListAsync();

            var checkedboxes = await roles.ToListAsync();
            ViewData["CheckedBox"] = checkedboxes;

            ViewData["RoleErrorMessage"] = "";
            if (selectedRoles.Count() == 0)
            {
                ViewData["ViewBagEmpty"] = "true";
                ViewData["RoleErrorMessage"] = "You must select at least one role! ";
            }
            //List of employee
            List<Employee> employees = new List<Employee>();
            employees.Add(employee);
            var alldbEmployees = _context.Employees;

            //Check if employee User name does not already exist
            int userName = 0;
            ViewData["UserErrorMessage"] = "";
            foreach (var emp in alldbEmployees)
            {
                if (emp.UserName == employee.UserName)
                {
                    ViewData["UserExist"] = "true";
                    ViewData["UserErrorMessage"] = "This user name has been used by another employee";
                    userName = 1;
                }
            }

            if (ModelState.IsValid && selectedRoles.Count() != 0 && userName != 1)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var roleContext = _context.Roles.Include(a=> a.Employees);
            ViewData["Roles"] = await roleContext.ToListAsync();
            ViewData["EmployeeId"] = id.ToString();
            List<Employee> employees = new List<Employee>();
            employees.Add(employee);    
            ViewData["Employee"]= employees;

            var employeeRolesContext = _context.Employees.Include(a => a.Roles);
            List<Role> Roles = new List<Role>();

            foreach (var emp in employees)
            {
                foreach(var role in emp.Roles)
                {
                    Roles.Add(role);
                }
            }

            ViewData["Employee_Roles"] = Roles;

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            userDoesExist = false;

            // MOVE THIS CODES TO THE BOTTOM AFTER PASSING VALIDATIONS ELSE WE DO NOT ADD ANYTHING TO THER LIST OR CHECK ANYTHING
            string[] selectedRoles = Request.Form["checkBoxList"];
            var roles = _context.Roles.Where(r => selectedRoles.Contains(r.Role1));
            var updatedRoles = await roles.ToListAsync();
            var dbEmployee = await _context.Employees.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Id == employee.Id);

            dbEmployee.Name = employee.Name;
            dbEmployee.UserName= employee.UserName;
            dbEmployee.Password = employee.Password;
            dbEmployee.Roles = new HashSet<Role>(updatedRoles);
            employee.Roles = await roles.ToListAsync();


            //Check if employee User name does not already exist

            var all = _context.Employees.Where(em => em.UserName == employee.UserName);
            var alldbEmployees = await all.ToListAsync();

            //int userName = 0;
            ViewData["UserErrorMessage"] = "";
            foreach (var emp in alldbEmployees)
            {
                if (emp.UserName.Equals(employee.UserName))
                {
                    ViewData["UserExist"] = "true";
                    userDoesExist = true;
                }
                else
                {
                    userDoesExist = false;
                }
            }

            if(userDoesExist)
            {
                ViewData["UserErrorMessage"] = "This user name has been used by another employee";
            }
            //Getting previousroles
            var roleContext = _context.Roles.Include(a => a.Employees);
            ViewData["Roles"] = await roleContext.ToListAsync();


            // if cheched boxes are empty at the same time with user already exist , it save the user which shos that the 
            //user alresdy exist because was added in the list . chech to validationjs beforre adding anythiong in a list if any fail nothing 
            // just to go back to the page with error message. bellow and validate checkbox at the top so that with follow steps 

            //List of employee
            List<Employee> employees = new List<Employee>();

            List<Employee> employees1 = new List<Employee>();
            employees.Add(employee);
            ViewData["Employee"] = employees1;

            var employeeRolesContext = _context.Employees.Include(a => a.Roles);
            List<Role> Roles = new List<Role>();

            foreach (var emp in employees)
            {
                foreach (var role in emp.Roles)
                {
                    Roles.Add(role);
                }
            }
            ViewData["Employee_Roles"] = Roles;

            ViewData["RoleErrorMessage"] = null;
            if (selectedRoles.Count() == 0)
            {
                ViewData["ViewBagEmpty"] = "true";
                ViewData["RoleErrorMessage"] = "You must select at least one role! ";
                //Getting roles in new role list
                Roles = new List<Role>();
                ViewData["Employee_Roles"] = Roles;
            }
            




            if (ModelState.IsValid && selectedRoles.Count() != 0 && !userDoesExist)
            {
                try
                {
                    _context.Update(dbEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'StudentRecordContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.Id == id);
        }
    }
}
