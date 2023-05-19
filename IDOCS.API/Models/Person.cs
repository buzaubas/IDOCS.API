using System;
using System.Collections.Generic;

namespace IDOCS.API.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Document> DocumentCreatedPeople { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentReceiverPeople { get; set; } = new List<Document>();
}
