using System;
using System.Text;

using BungoNet.Extensions;

using BungoNet.Serialization.BnetApi.Interfaces;
using BungoNet.Serialization.BnetApi;

namespace BungoNet.Services.GameApi.Model
{
    public enum FileCatalogItemType
    {
        MapVariant,
    }

    public enum FileCatalogStateCondition
    {
        Ready,
    }

    public class FileCatalogItem
    {
        public uint Guid { get; set; }
        public FileCatalogStateCondition State { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public uint AuthorXuid { get; set; }
        public bool AuthorXuidIsOnline { get; set; }
        public uint SizeBytes { get; set; }
        public FileCatalogItemType FileType { get; set; }
        public uint SecondsPast19700101 { get; set; }
        public int LengthSeconds { get; set; }
        public int CampaignID { get; set; }
        public int MapID { get; set; }
        public int GameEngineType { get; set; }
        public int CampaignDifficulty { get; set; }
        public int GameID { get; set; }
    }

    public class FileCatalogItemList : IBnetSerializable
    {
        public List<FileCatalogItem> Items { get; private set; } = new();

        public void Serialize(StringBuilder sb)
        {
            sb.Append($"SlotCount: {Items.Count}\r\nVisibleSlots: {Items.Count}\r\nMessage:\r\n");

            foreach (var (item, index) in Items.WithIndex())
            {
                string formatted = BnetApiSerializer.Serialize(item, 2);

                sb.Append($"StartSlot: {index}\r\n");
                sb.Append(formatted);
                sb.Append($"EndSlot\r\n");
            }
        }
    }

    public record FileCatalogModel(int QuotaBytes, int QuotaSlots, int SubscriptionHash, FileCatalogItemList list);
}
