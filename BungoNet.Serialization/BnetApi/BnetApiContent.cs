using System;
using System.Text;

using GenHTTP.Api.Protocol;

namespace BungoNet.Serialization.BnetApi
{
    public sealed class BnetApiContent : IResponseContent
    {
        #region Get-/Setters

        public ulong? Length => null;

        private object Data { get; }

        #endregion

        #region Initialization

        public BnetApiContent(object data)
        {
            Data = data;
        }

        #endregion

        #region Functionality

        public ValueTask<ulong?> CalculateChecksumAsync()
        {
            return new ValueTask<ulong?>((ulong)Data.GetHashCode());
        }

        public ValueTask WriteAsync(Stream target, uint bufferSize)
        {
            return DoWrite(this, target);
            
            static async ValueTask DoWrite(BnetApiContent self, Stream target)
            {
                await target.WriteAsync(Encoding.Latin1.GetBytes(BnetApiSerializer.Serialize(self.Data)));
            }
        }

        #endregion
    }
}
