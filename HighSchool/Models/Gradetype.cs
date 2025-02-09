using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Gradetype
{
    public int Id { get; set; }

    public string? Grade { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
