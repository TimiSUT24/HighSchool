using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Grade
{
    public int Gradeid { get; set; }

    public int? Studentid { get; set; }

    public int? Subjectid { get; set; }

    public int? Teacherid { get; set; }

    public DateOnly Date { get; set; }

    public int? Graderid { get; set; }

    public int? Gradetypeid { get; set; }

    public virtual Personal? Grader { get; set; }

    public virtual Gradetype? Gradetype { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Personal? Teacher { get; set; }
}
