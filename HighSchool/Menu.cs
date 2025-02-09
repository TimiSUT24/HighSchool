using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool
{
    public static class Menu
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.Title = "Silmarillion HighSchool";       
           
            Dictionary<string, Action> UserActions = new Dictionary<string, Action>() //User menu with the using of Action delegate
            {
                {"1", () => { EntityMethods.CountOfEmployees(); } },
                {"2", () => { EntityMethods.InfoAboutStudents(); } },
                {"3", () => { EntityMethods.ActiveCourses(); } },
                {"4", () => { ADO_Employee.EmployeeInfo(); } },
                {"5", () => { ADO_Employee.AddEmployee(); } },
                {"6", () => { ADOMethods.Grades(); } },
                {"7", () => { ADO_Salary.MonthlyPayOut(); } },
                {"8", () => { ADO_Salary.AveragePayout(); } },
                {"9", () => { ADOMethods.GetStudentById(); } },
                {"10", () => { ADOMethods.Transaction(); } },
                {"11", () => { Environment.Exit(0); } },              
            };

            while (true)
            {
                Console.WriteLine("--Welcome to the Silmarillion HighSchool--\n" +
               "Want you want to do? Please choose following options.\n");

                Console.WriteLine("1. Numbers of employees in different departments\n" + 
                                  "2. Show info about all students\n" +
                                  "3. Active Courses\n" +
                                  "4. Show info about employees\n" +
                                  "5. Add new employees\n" +
                                  "6. Show grades for one student\n" +
                                  "7. Departments payouts\n" +
                                  "8. Average paycheck for each departments\n" +
                                  "9. Get Student Info\n" +
                                  "10. Set grade\n" +
                                  "11. Exit\n");

                string userInput = Console.ReadLine();

                if (UserActions.ContainsKey(userInput))
                {
                    UserActions[userInput].Invoke();
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
                Console.WriteLine("\nPress any key to continue"); 
                Console.ReadKey();
                Console.Clear();           
            }
        }
    }
}
