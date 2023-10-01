using System;
using System.Collections.Generic;

namespace LuvFinder_API.Models;

public partial class UserInfo
{
    public int Id { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public int? GenderId { get; set; }

    public int? MaritalStatusId { get; set; }

    public int? UserId { get; set; }

    public DateTime? Dob { get; set; }

    public int? SeekingGenderId { get; set; }

    public short CountryId { get; set; }

    public int CityId { get; set; }

    public int RegionId { get; set; }

    public DateTime? Date { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual UserGender? Gender { get; set; }

    public virtual UserMaritalStatus? MaritalStatus { get; set; }

    public virtual Region Region { get; set; } = null!;

    public virtual UserGender? SeekingGender { get; set; }

    public virtual User? User { get; set; }
}
