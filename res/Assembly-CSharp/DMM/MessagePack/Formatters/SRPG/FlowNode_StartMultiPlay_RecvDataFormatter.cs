// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.FlowNode_StartMultiPlay_RecvDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class FlowNode_StartMultiPlay_RecvDataFormatter : 
    IMessagePackFormatter<FlowNode_StartMultiPlay.RecvData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public FlowNode_StartMultiPlay_RecvDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "senderPlayerID",
          0
        },
        {
          "version",
          1
        },
        {
          "playerList",
          2
        },
        {
          "playerListJson",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("senderPlayerID"),
        MessagePackBinary.GetEncodedStringBytes("version"),
        MessagePackBinary.GetEncodedStringBytes("playerList"),
        MessagePackBinary.GetEncodedStringBytes("playerListJson")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      FlowNode_StartMultiPlay.RecvData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.senderPlayerID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.version);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MyPhotonPlayerBinaryParam[]>().Serialize(ref bytes, offset, value.playerList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.playerListJson, formatterResolver);
      return offset - num;
    }

    public FlowNode_StartMultiPlay.RecvData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (FlowNode_StartMultiPlay.RecvData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      Json_MyPhotonPlayerBinaryParam[] playerBinaryParamArray = (Json_MyPhotonPlayerBinaryParam[]) null;
      string str = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              playerBinaryParamArray = formatterResolver.GetFormatterWithVerify<Json_MyPhotonPlayerBinaryParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new FlowNode_StartMultiPlay.RecvData()
      {
        senderPlayerID = num3,
        version = num4,
        playerList = playerBinaryParamArray,
        playerListJson = str
      };
    }
  }
}
