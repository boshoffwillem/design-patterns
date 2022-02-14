using System;

namespace DomainModel.Services;

public class CheckoutMediaService
{
    private readonly Gateway _gateway;

    public CheckoutMediaService()
    {
        _gateway = new();
    }

    public void CheckoutMedia(int mediaId)
    {
        var media = _gateway.FindMedia(mediaId);
        if (!media.IsCheckedOut)
        {
            _gateway.InsertCheckout(mediaId, DateTime.Now.AddDays(media.MaxCheckoutDays));
        }
    }
}

