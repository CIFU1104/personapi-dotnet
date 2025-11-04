using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Entities;

public partial class persona
{
    public int cc { get; set; }

    public string nombre { get; set; } = null!;

    public string apellido { get; set; } = null!;

    public string genero { get; set; } = null!;

    public int? edad { get; set; }

    public virtual ICollection<estudio> estudios { get; set; } = new List<estudio>();

    public virtual ICollection<telefono> telefonos { get; set; } = new List<telefono>();
}
