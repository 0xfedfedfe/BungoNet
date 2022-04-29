using System;

namespace BungoNet.Services.GameApi.Model
{
    public enum BnetSubscriptionStatus
    {
        Unsubscribed,
        Subscribed
    }

    public class BnetSubscriptionStatusModel
    {
        public BnetSubscriptionStatus Status { get; set; }
    }
}
