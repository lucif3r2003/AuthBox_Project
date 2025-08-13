using System;
using System.Collections.Generic;

namespace Auth_Box.Models;

public partial class user
{
    public Guid id { get; set; }

    public string email { get; set; } = null!;

    public string? password_hash { get; set; }

    public string? full_name { get; set; }

    public string? avatar_url { get; set; }

    public string? phone_number { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public bool? is_active { get; set; }

    public virtual ICollection<user_provider> user_providers { get; set; } = new List<user_provider>();
}
