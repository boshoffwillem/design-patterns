namespace DomainModel.Models.Media;

public class Movie : MediaBase
{
    public override MediaType Type => MediaType.Movie;

    public override int MaxCheckoutDays => 28;
}

