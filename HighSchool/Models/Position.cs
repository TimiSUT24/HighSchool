using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Position
{
    public int? Id { get; set; }

    public string? Position1 { get; set; }

    public int? Departmentsid { get; set; }

    public virtual Department? Departments { get; set; }

    public virtual Personal? IdNavigation { get; set; }
}
