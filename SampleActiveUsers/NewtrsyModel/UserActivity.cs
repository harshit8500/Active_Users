using System;
using System.Collections.Generic;

namespace SampleActiveUsers.NewtrsyModel;

public partial class UserActivity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }

    public int? SessionDuration { get; set; }
}
