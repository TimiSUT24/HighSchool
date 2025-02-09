using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Class
{
    public int Classid { get; set; }

    public string Name { get; set; } = null!;

    public int? Teacherid { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Personal? Teacher { get; set; }
}
