using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.DAL.Dao.Settings;

public class StripePayment
{
    private readonly IOptions<StripeSettings> _settings;
    public StripePayment(IOptions<StripeSettings> settings)
    {
        _settings = settings;  
    }

    public Session CreateCheckoutSession(long Amount, string CurrencyCode, string ProductName)
    {
        StripeConfiguration.ApiKey = _settings.Value.SecretKey;

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Amount, // For example, $20.00 (this value is in cents)
                        Currency = CurrencyCode,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = ProductName,
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = _settings.Value.SuccessUrl,
            CancelUrl = _settings.Value.CancelUrl,
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return session;

    }
}