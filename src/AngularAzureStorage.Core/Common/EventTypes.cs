namespace AngularAzureStorage.Core.Common
{
    public static class EventTypes
    {
        public static class CardLayouts
        {

        }

        public static class Cards
        {

        }

        public static class ContactRequests {
            public const string ContactRequestCreated = nameof(ContactRequestCreated);
        }

        public static class Conversations
        {
            public const string ConversationRequested = nameof(ConversationRequested);
            public const string ConversationAccepted = nameof(ConversationAccepted);
            public const string MessageRead = nameof(MessageRead);
        }

        public static class Customers
        {
            public const string CreditsAdded = nameof(CreditsAdded);
            public const string CreditsUsed = nameof(CreditsUsed);
            public const string CustomerCreated = nameof(CustomerCreated);
        }

        public static class DashboardCards
        {

        }

        public static class Dashboards
        {

        }

        public static class DigitalAssets
        {
            public const string DigitalAssetUploaded = nameof(DigitalAssetUploaded);
        }
        
        public static class Identity {
            public const string UserSignedIn = nameof(UserSignedIn);
            public const string UserSignedOut = nameof(UserSignedOut);
            public const string UserCreated = nameof(UserCreated);
        }

        public static class Orders
        {
            public const string OrderCreated = nameof(OrderCreated);
            public const string OrderCancelled = nameof(OrderCancelled);
        }

        public static class Products
        {
            public const string ProductPurchased = nameof(ProductPurchased);
        }

        public static class Profiles
        {
            public const string ProfileCreated = nameof(ProfileCreated);
        }

        public static class ServiceProviders
        {
            public const string ServiceProviderCreated = nameof(ServiceProviderCreated);
        }

        public static class Reports
        {

        }
    }
}
