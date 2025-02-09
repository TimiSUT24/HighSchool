using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int? Personalid { get; set; }

    public decimal Basesalary { get; set; }

    public decimal? Bonus { get; set; }

    public DateOnly? Salarydate { get; set; }

    public virtual Personal? Personal { get; set; }
}
