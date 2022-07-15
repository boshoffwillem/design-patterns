namespace DomainModel.Models.Media;

public class Book : MediaBase
{
    public override MediaType Type => MediaType.Book;

    public override int MaxCheckoutDays => 28;
}
