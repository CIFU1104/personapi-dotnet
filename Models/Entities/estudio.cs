using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Entities;

public partial class estudio
{
    public int id_prof { get; set; }

    public int cc_per { get; set; }

    public DateOnly? fecha { get; set; }

    public string? univer { get; set; }

    public virtual persona cc_perNavigation { get; set; } = null!;

    public virtual profesion id_profNavigation { get; set; } = null!;
}
