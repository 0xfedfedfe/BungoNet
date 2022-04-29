﻿using System;

using GenHTTP.Engine;
using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Webservices;
using GenHTTP.Modules.Layouting;

using BungoNet.Serialization.BnetApi;

namespace BungoNet.Service
{
    class Program
    {
        static private void Main(string[] args)
        {
            var gameApiRegistry = GenHTTP.Modules.Conversion.Serialization.Empty().
                Add(new FlexibleContentType(ContentType.TextPlain), new BnetApiFormat())
                .Default(ContentType.TextPlain);

            var gameApiResource = ServiceResource.From<Services.GameApi.GameApiResource>()
                .Formats(gameApiRegistry);

            var hopperResource = ServiceResource.From<Services.Storage.Resource.HopperResource>();

            var cunt4 = Layout.Create()
                .Add("default_hoppers", hopperResource);
            var cunt3 = Layout.Create()
                .Add("12070", cunt4);
            var cunt2 = Layout.Create()
                .Add("tracked", cunt3);
            var cunt = Layout.Create()
                .Add("title", cunt2);

            var service = Layout.Create()
                .Add("gameapi", gameApiResource)
                .Add("storage", cunt);

            Host.Create()
                .Port(80)
                .Handler(service)
                .Console()
#if DEBUG
                .Development()
#endif
                .Run();
        }
    }
}
