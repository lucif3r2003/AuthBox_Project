using System;
using System.Collections.Generic;

namespace Auth_Box.Models;

public partial class user_provider
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string provider { get; set; } = null!;

    public string provider_user_id { get; set; } = null!;

    public string? access_token { get; set; }

    public string? refresh_token { get; set; }

    public DateTime? created_at { get; set; }

    public virtual user user { get; set; } = null!;
}
