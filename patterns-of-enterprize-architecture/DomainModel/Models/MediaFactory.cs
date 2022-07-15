using DomainModel.Models.Media;

namespace DomainModel.Models;

public class MediaFactory
{
    public MediaBase CreateMedia(MediaType mediaType)
    {
        switch(mediaType)
        {
            case MediaType.Book:
                return new Book();
            case MediaType.Magazine:
                return new Magazine();
            case MediaType.Movie:
                return new Movie();
            default:
                return new Book();
        }
    }
}

public enum MediaType
{
    Book,
    Magazine,
    Movie
}
