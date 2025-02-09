using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool
{
    public static class ADO_Employee
    {
        public static void EmployeeInfo()
        {
            Console.Clear();
            Console.WriteLine("--Employees--\n");
            using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Gets and shows info about all employees
            {
                string query = @"SELECT NAME,LASTNAME,NUMBER,YEARS_OF_EMPLOYMENT,POSITION 
                               FROM PERSONAL 
                               LEFT JOIN POSITIONS ON POSITIONS.ID = PERSONAL.PERSONALID ";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-20} {4,-20}", "Name", "Lastname", "Number", "Years_Of_Employment", "Position\n");
                    while (reader.Read())
                    {
                        string name = reader["NAME"].ToString();
                        string lastname = reader["LASTNAME"].ToString();
                        string number = reader["NUMBER"].ToString();
                        string yearsEmp = Convert.ToInt32(reader["YEARS_OF_EMPLOYMENT"]).ToString();
                        string position = reader["POSITION"].ToString();                       

                        Console.WriteLine("__________________________________________________________________________________________________________");

                        Console.WriteLine($"{name.PadRight(20)} {lastname.PadRight(20)} {number.PadRight(20)} {yearsEmp.PadRight(20)} {position.PadRight(20)}");
                    }
                }

                conn.Close();
            }
        }

        public static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("--Add Employee--\n");
            Console.WriteLine("Follow the following format\n" +
                "[Name,Lastname,Epost,Number,Hired_Date,Personal Number]\n");         
            try
            {
                string userInput = Console.ReadLine().ToUpper();
                userInput.Split(',');
                string[] inputs = userInput.Split(',');
                int.Parse(inputs[3]);
                int.Parse(inputs[5]);

                if (inputs.Length > 6 || inputs.Length <= 0)
                {
                    Console.WriteLine("Error");
                }

                if (inputs[3].Length > 11 || inputs[3].Length < 5)
                {
                    Console.WriteLine("Error: Number too short or long!!");
                    Thread.Sleep(1500);
                    Menu.MainMenu();
                }

                using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //User can add new employees
                {
                    string query = "INSERT INTO PERSONAL (NAME,LASTNAME,EPOST,NUMBER,HIRED_DATE,PERSONUMBER)" +
                        "VALUES (@NAME,@LASTNAME,@EPOST,@NUMBER,@HIRED_DATE,@PERSONUMBER)";

                    SqlCommand cmd = new SqlCommand(query, conn);   
                    cmd.Parameters.AddWithValue("@NAME", inputs[0]);
                    cmd.Parameters.AddWithValue("@LASTNAME", inputs[1]);
                    cmd.Parameters.AddWithValue("@EPOST", inputs[2]);
                    cmd.Parameters.AddWithValue("@NUMBER", inputs[3]);
                    cmd.Parameters.AddWithValue("@HIRED_DATE", Convert.ToDateTime(inputs[4]).Date);
                    cmd.Parameters.AddWithValue("@PERSONUMBER", inputs[5]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Employee added!!");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input. " + ex.Message);
            }
        }
    }
}
