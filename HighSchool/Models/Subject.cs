using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Subject
{
    public int Subjectid { get; set; }

    public string Subjects { get; set; } = null!;

    public string? Status { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
