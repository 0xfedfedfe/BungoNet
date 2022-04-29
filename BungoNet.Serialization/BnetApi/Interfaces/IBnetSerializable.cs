using System;
using System.Text;

namespace BungoNet.Serialization.BnetApi.Interfaces
{
    public interface IBnetSerializable
    {
        public void Serialize(StringBuilder sb);
    }
}
