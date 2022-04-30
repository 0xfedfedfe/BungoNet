using System;

using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Webservices;

using BungoNet.Services.GameApi.Model;

namespace BungoNet.Services.GameApi.Resource
{
    public class BnetSubscriptionResource
    {
        [ResourceMethod("UserGetBnetSubscription.ashx")]
        public BnetSubscriptionStatusModel? GetBnetSubscription(IRequest request)
        {
            return new BnetSubscriptionStatusModel { Status = BnetSubscriptionStatus.Subscribed };
        }
    }
}
