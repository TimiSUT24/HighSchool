using System;
using System.Collections.Generic;

namespace HighSchool.Models;

public partial class Personal
{
    public int Personalid { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Epost { get; set; } = null!;

    public string Number { get; set; } = null!;

    public DateOnly? HiredDate { get; set; }

    public int? YearsOfEmployment { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> GradeGraders { get; set; } = new List<Grade>();

    public virtual ICollection<Grade> GradeTeachers { get; set; } = new List<Grade>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
}
