using System;

namespace DomainModel.Models.Media;

public abstract class MediaBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; } = DateTime.MinValue;
    public bool IsCheckedOut => DueDate != DateTime.MinValue;
    public abstract MediaType Type { get; }
    public abstract int MaxCheckoutDays { get; }
}

