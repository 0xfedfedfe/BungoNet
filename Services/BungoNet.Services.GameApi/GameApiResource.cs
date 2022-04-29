using System;

using GenHTTP.Modules.Webservices;

using BungoNet.Services.GameApi.Model;

namespace BungoNet.Services.GameApi
{
    public class GameApiResource
    {
        private FileCatalogModel? GetTempFileCatalog()
        {
            FileCatalogItemList list = new();

            for (int i = 0; i < 20; i++)
            {
                list.Items.Add(
                    new FileCatalogItem
                    {
                        Guid = (uint)Random.Shared.Next(),
                        State = FileCatalogStateCondition.Ready,
                        Name = $"Ligma {i}",
                        Description = "Ligma",
                        Author = "Ligma",
                        AuthorXuid = (uint)Random.Shared.Next(),
                        AuthorXuidIsOnline = true,
                        SizeBytes = 0x420,
                        FileType = FileCatalogItemType.MapVariant,
                        SecondsPast19700101 = (uint)Random.Shared.Next(),
                        LengthSeconds = Random.Shared.Next(),
                        CampaignID = -1,
                        MapID = 300,
                        GameEngineType = 2,
                        CampaignDifficulty = 0,
                        GameID = 1,
                    });
            }

            return new FileCatalogModel(5000000, 25, 0, list);
        }

        [ResourceMethod("FilesGetCatalog.ashx")]
        public FileCatalogModel? GetFileCatalog()
        {
            return GetTempFileCatalog();
        }

        [ResourceMethod("FilesGetCatalogODST.ashx")]
        public FileCatalogModel? GetFileCatalogODST()
        {
            return GetTempFileCatalog();
        }

        [ResourceMethod("BungieFavourites.ashx")]
        public FileCatalogModel? GetBungieFavourites()
        {
            return GetTempFileCatalog();
        }

        [ResourceMethod("UserGetBnetSubscription.ashx")]
        public BnetSubscriptionStatusModel? GetBnetSubscription()
        {
            return new BnetSubscriptionStatusModel { Status = BnetSubscriptionStatus.Subscribed };
        }
    }
}
