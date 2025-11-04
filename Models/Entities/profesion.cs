using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Entities;

public partial class profesion
{
    public int id { get; set; }

    public string nom { get; set; } = null!;

    public string? des { get; set; }

    public virtual ICollection<estudio> estudios { get; set; } = new List<estudio>();
}
