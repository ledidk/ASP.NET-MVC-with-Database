#ASP.NET-MVC-with-Database

**Description:**

This project is an ASP.NET MVC web application that manages academic records for students. It provides functionalities to view, add, edit, and delete academic records. The application also allows employees to be managed, including their roles and permissions.

**Project Structure:**

 The project consists of three main controllers:

**1.AcademicRecordsController:**

Manages academic records, including viewing, adding, editing, and deleting records. It communicates with the database through the StudentRecordContext context.


**2. CoursesController:**

Handles courses, including viewing course details, adding new courses, editing existing courses, and deleting courses. It also interacts with the StudentRecordContext context for database operations.


**3. EmployeesController:** 

Manages employees, their roles, and permissions. The controller allows adding new employees, editing existing employee information, and deleting employees. It utilizes the StudentRecordContext context to interact with the database.


**Views:**

The project contains various views that allow users to interact with the application, such as viewing academic records, courses, and employee details. Additionally, there are views for creating, editing, and deleting records.

**Models:**

 The project uses several model classes, such as AcademicRecord, Course, and Employee, to represent entities in the database. These models are used by the controllers and views to manage data.
