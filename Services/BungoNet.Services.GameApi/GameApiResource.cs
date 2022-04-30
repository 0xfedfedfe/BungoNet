using System;

using GenHTTP.Api.Protocol;

using BungoNet.Serialization.BnetApi;
using BungoNet.Extensions.GenHTTP;

namespace BungoNet.Services.GameApi
{
    /// <summary>
    /// Entry point to add all Game API routes at once to another router.
    /// </summary>
    public static class GameApiResource
    {
        public static ServiceMultipleResourceBuilder Create()
        {
            var gameApiRegistry = GenHTTP.Modules.Conversion.Serialization.Empty().
                Add(new FlexibleContentType(ContentType.TextPlain), new BnetApiFormat())
                .Default(ContentType.TextPlain);

            var gameApiResource = ServiceMultipleResource.From<Resource.FileShareResource>()
                .AddType<Resource.BnetSubscriptionResource>()
                .Formats(gameApiRegistry);

            return gameApiResource;
        }
    }
}
