using HighSchool.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HighSchool
{
    public static class ADOMethods
    {             
        public static void Grades()
        {
            Console.Clear();
            Console.WriteLine("--Grades--\n" +
                             "\nChoose Student\n" +
                             "[Name,Lastname]\n");
            try
            {
                string ChooseStudent = Console.ReadLine();
                string[] inputs = ChooseStudent.Split(',');

                Console.WriteLine("\n{0,-10} {1,-10} {2,-20} {3,-10} {4,-20} {5,-20}", "GradeId", "StudentId", "Subjects", "Grade", "Teacher Name", "Date\n");

                using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Gets and shows what grades a student has 
                {
                    string query = @"SELECT GRADES.GRADEID,STUDENTS.STUDENTID,SUBJECTS.SUBJECTS,GRADETYPE.GRADE,PERSONAL.NAME,GRADES.DATE FROM STUDENTS
                    JOIN GRADES ON STUDENTS.STUDENTID = GRADES.STUDENTID
                    JOIN SUBJECTS ON SUBJECTS.SUBJECTID = GRADES.SUBJECTID
                    JOIN PERSONAL ON PERSONAL.PERSONALID = GRADES.TEACHERID
                    JOIN GRADETYPE ON GRADETYPE.ID = GRADES.GRADETYPEID
                    WHERE STUDENTS.NAME = @NAME AND STUDENTS.LASTNAME = @LASTNAME"; 

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NAME", inputs[0]);       
                    cmd.Parameters.AddWithValue("@LASTNAME", inputs[1]);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Id = reader["GRADEID"].ToString();       //Store the data in string objects
                            string student = reader["STUDENTID"].ToString();
                            string subject = reader["SUBJECTS"].ToString();
                            string grade = reader["GRADE"].ToString();
                            string name = reader["NAME"].ToString();
                            string date = Convert.ToDateTime(reader["DATE"]).Date.ToString();

                            Console.WriteLine("____________________________________________________________________________________________________");
                            Console.WriteLine($"{Id.PadRight(10)} {student.PadRight(10)} {subject.PadRight(20)} {grade.PadRight(10)} {name.PadRight(20)} {date.PadRight(20)}");
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input. " + ex.Message);
            }
            
        }        
        public static void StoredProcedure()
        {
            Console.Clear();
            Console.WriteLine("--Info--");
        
            using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Creates a stored procedure 
            {
                string query = @"CREATE PROCEDURE GETID @STUDENTID NVARCHAR(50)     
                              AS
                              SELECT * FROM STUDENTS WHERE STUDENTID = @STUDENTID";

                SqlCommand cmd = new SqlCommand(query, conn);
                
                conn.Open();

                var procedure = cmd.ExecuteNonQuery();
                conn.Close();
            }        
        }

        public static void GetStudentById()
        {
            Console.Clear();
            Console.WriteLine("--Get Student--\n" +
                             "\nType Student Id.\n");
            try
            {
                string input = Console.ReadLine();

                Console.WriteLine("\n{0,-20} {1,-20} {2,-20} {3,-20} {4,-20}", "StudentId", "Name", "Lastname", "PersonalNumber", "Email\n");
                using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Using the stored procedure GETID and shows the student info
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GETID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDENTID", input);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {                           
                            string studentid = reader["STUDENTID"].ToString();
                            string name = reader["NAME"].ToString();
                            string lastname = reader["LASTNAME"].ToString();
                            string persoNumber = reader["PERSONUMBER"].ToString();
                            string epost = reader["EPOST"].ToString();
                            
                            Console.WriteLine("__________________________________________________________________________________________________________");
                            Console.WriteLine($"{studentid.PadRight(20)} {name.PadRight(20)} {lastname.PadRight(20)} {persoNumber.PadRight(20)} {epost}");
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input. " + ex.Message);
            }       
        }

        public static void Transaction()
        {
           
            Console.Clear();
            Grades();
            Console.WriteLine("\n--Set grade--\n" +
                             "\n1. Set new grade\n" +
                             "2. Edit grade\n");
        
            string input = Console.ReadLine();
            if( input == "1")
            {
                Console.WriteLine("Follow the following format to set grade\n" +
                                 "[StudentId,SubjectId,TeacherId,Date,GraderId,GradeTypeId]\n");
                string userInput = Console.ReadLine();
                string[] insertInputs = userInput.Split(',');

                using (SqlConnection conn = new SqlConnection(Database.ConnectionString)) //Creates a transaction so we can rollback any errors.
                {
                    conn.Open();                                                        //User can set a grade and edit a grade for a student. 
                    SqlTransaction tran = conn.BeginTransaction();

                    try
                    {
                        string insert = @"INSERT INTO GRADES (STUDENTID, SUBJECTID,TEACHERID,DATE,GRADERID,GRADETYPEID)
                                       VALUES (@STUDENTID,@SUBJECTID,@TEACHERID,@DATE,@GRADERID,@GRADETYPEID)";

                        SqlCommand cmd = new SqlCommand(insert, conn, tran);
                        cmd.Parameters.AddWithValue("@STUDENTID", insertInputs[0]);
                        cmd.Parameters.AddWithValue("@SUBJECTID", insertInputs[1]);
                        cmd.Parameters.AddWithValue("@TEACHERID", insertInputs[2]);
                        cmd.Parameters.AddWithValue("@DATE", insertInputs[3]);
                        cmd.Parameters.AddWithValue("@GRADERID", insertInputs[4]);
                        cmd.Parameters.AddWithValue("@GRADETYPEID", insertInputs[5]);
                      
                        cmd.ExecuteNonQuery();
                       
                        Console.WriteLine("Grade set");

                        tran.Commit(); //Completes the transaction
                        conn.Close();
                    }
                    catch(Exception ex) 
                    {                      
                        tran.Rollback();
                        Console.WriteLine("Transaction rolled back. " + ex.Message);
                    }
                }
            }
            
            if(input == "2")
            {
                Console.WriteLine("Follow the following format to edit grade\n" +
                                 "[Grade,Date,GradeId]\n");
                string userInput2 = Console.ReadLine();
                string[] insertInputs2 = userInput2.Split(',');

                using (SqlConnection conn2 = new SqlConnection(Database.ConnectionString))
                {
                    conn2.Open();
                    SqlTransaction tran2 = conn2.BeginTransaction();

                    try
                    {
                        string update = @"UPDATE GRADES SET GRADETYPEID = @GRADETYPEID, DATE = @DATE
                                        WHERE GRADEID = @GRADEID";

                        SqlCommand cmd2 = new SqlCommand(update, conn2, tran2);
                        cmd2.Parameters.AddWithValue("@GRADETYPEID", insertInputs2[0]);
                        cmd2.Parameters.AddWithValue("@DATE", insertInputs2[1]);
                        cmd2.Parameters.AddWithValue("@GRADEID", insertInputs2[2]);

                        cmd2.ExecuteNonQuery();

                        Console.WriteLine("Grade updated");

                        tran2.Commit();
                        conn2.Close();
                    }
                    catch(Exception ex)
                    {
                        tran2.Rollback();
                        Console.WriteLine("Transaction rolled back. " + ex.Message);
                    }
                }

            }        
          
        }
    }
}
