using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Student
{
    public int Studentid { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Personumber { get; set; } = null!;

    public string Epost { get; set; } = null!;

    public int? Classid { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
