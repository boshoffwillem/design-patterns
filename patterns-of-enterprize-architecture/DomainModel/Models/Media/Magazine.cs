namespace DomainModel.Models.Media;

public class Magazine : MediaBase
{
    public override MediaType Type => MediaType.Book;

    public override int MaxCheckoutDays => 28;
}


