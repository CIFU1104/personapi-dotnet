using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Entities;

public partial class telefono
{
    public string num { get; set; } = null!;

    public string oper { get; set; } = null!;

    public int duenio { get; set; }

    public virtual persona duenioNavigation { get; set; } = null!;
}
