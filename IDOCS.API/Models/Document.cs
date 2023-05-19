using System;
using System.Collections.Generic;

namespace IDOCS.API.Models;

public partial class Document
{
    public int Number { get; set; }

    public string Type { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CreatedPersonId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Data { get; set; } = null!;

    public int ReceiverPersonId { get; set; }

    public virtual Person CreatedPerson { get; set; } = null!;

    public virtual Person ReceiverPerson { get; set; } = null!;
}
