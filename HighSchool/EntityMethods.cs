using HighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool
{
    public static class EntityMethods
    {
        public static void CountOfEmployees()
        {
            Console.Clear();
            Console.WriteLine("--Departments--\n");

            Console.WriteLine("{0,-20} {1,-15}", "Departmentname", "Teachers\n");        

            using (var context = new HögstadieskolaContext()) //Shows number of employees in each department 
            {

                var CountEmployees = from department in context.Departments
                                     join position in context.Positions on department.Id equals position.Departmentsid   
                                     group position by department.Departmentname into departmentgroup
                                     select new
                                     {
                                         Departmentname = departmentgroup.Key,  //Stores departments in a group 
                                         Count = departmentgroup.Count()
                                     };                                                                      
                
                var test = CountEmployees.ToList();               
                foreach(var item in test)
                {
                    Console.WriteLine("___________________________________");                
                    Console.WriteLine($"{item.Departmentname.PadRight(20)} {item.Count}" );
                  
                }     
            }
        }

        public static void InfoAboutStudents()
        {
            Console.Clear();
            Console.WriteLine("--Student info--\n");

            Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-35} {4,-15}", "Name", "Lastname", "Personal Number", "Email", "Class\n");


            using (var context = new HögstadieskolaContext()) //Shows info about all students like name,lastname,classname
            {
                var ShowStudents = from student in context.Students
                                   join classname in context.Classes on student.Classid equals classname.Classid         
                                   select new
                                   {
                                       classname = classname.Name,
                                       studentname = student.Name,
                                       studentlastname = student.Lastname,
                                       studentepost = student.Epost,
                                       studentnum = student.Personumber
                                   };
             
                foreach (var students in ShowStudents)
                {
                    Console.WriteLine("__________________________________________________________________________________________________________");
                    Console.WriteLine($"{students.studentname.PadRight(15)} {students.studentlastname.PadRight(15)} {students.studentnum.PadRight(20)} {students.studentepost.PadRight(35)} {students.classname.PadRight(15)} ");
                }
            }
        }

        public static void ActiveCourses()
        {
            Console.Clear();
            Console.WriteLine("--Active courses--\n");

            Console.WriteLine("{0,-18} {1,-20}", "Subjects", "Status\n");
            using(var context = new HögstadieskolaContext())   //Shows all the subjects that are active/ongoing
            {
                var ActiveCourses = context.Subjects
                    .Select(s => new { s.Subjects, s.Status })
                    .Where(s => s.Status == "ACTIVE");

                foreach(var course in ActiveCourses)
                {
                    Console.WriteLine("___________________________________");
                    Console.WriteLine($"{course.Subjects.PadRight(18)} {course.Status.PadRight(20)}");
                }
            }
        }
    }
}
