using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool
{
    public static class ADO_Salary
    {
        public static void MonthlyPayOut()
        {
            Console.Clear();
            Console.WriteLine("--Monthly Payouts--\n");

            Console.WriteLine("\n{0,-20} {1,-20}", "Department", "Total\n");
            using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Gets and shows montlhy payout in each department 
            {
                string query = @"SELECT 
                               DEPARTMENTS.DEPARTMENTNAME,
                               SUM(BASESALARY + BONUS) AS TOTAL
                               FROM SALARY
                               JOIN POSITIONS ON SALARY.PERSONALID = POSITIONS.ID
                               JOIN DEPARTMENTS ON POSITIONS.DEPARTMENTSID = DEPARTMENTS.ID
                               GROUP BY DEPARTMENTS.DEPARTMENTNAME";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["DEPARTMENTNAME"].ToString();
                        string total = reader["TOTAL"].ToString();

                        Console.WriteLine("__________________________________________________________________________________________________________");
                        Console.WriteLine($"{name.PadRight(20)} {total.PadRight(20)} SEK");
                    }
                }
                conn.Close();
            }
        }

        public static void AveragePayout()
        {
            Console.Clear();
            Console.WriteLine("--Average Payout--\n");
            Console.WriteLine("\n{0,-20} {1,-20}", "Department", "Average\n");

            using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Gets and shows the average payout in each department
            {
                string query = @"SELECT 
                               DEPARTMENTS.DEPARTMENTNAME,
                               AVG(BASESALARY + BONUS) AS Average
                               FROM SALARY
                               JOIN POSITIONS ON SALARY.PERSONALID = POSITIONS.ID
                               JOIN DEPARTMENTS ON POSITIONS.DEPARTMENTSID = DEPARTMENTS.ID
                               GROUP BY DEPARTMENTS.DEPARTMENTNAME";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["DEPARTMENTNAME"].ToString();
                        string average = reader["Average"].ToString();

                        Console.WriteLine("__________________________________________________________________________________________________________");
                        Console.WriteLine($"{name.PadRight(20)} {average.PadRight(20)}SEK");
                    }
                }
                conn.Close();
            }

        }
    }
}
