// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.FlowNode_ReqLoginPack_JSON_ReqLoginPackResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class FlowNode_ReqLoginPack_JSON_ReqLoginPackResponseFormatter : 
    IMessagePackFormatter<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public FlowNode_ReqLoginPack_JSON_ReqLoginPackResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "quests",
          0
        },
        {
          "trophyprogs",
          1
        },
        {
          "bingoprogs",
          2
        },
        {
          "channels",
          3
        },
        {
          "channel",
          4
        },
        {
          "support",
          5
        },
        {
          "device_id",
          6
        },
        {
          "is_pending",
          7
        },
        {
          "pubinfo",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("quests"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs"),
        MessagePackBinary.GetEncodedStringBytes("channels"),
        MessagePackBinary.GetEncodedStringBytes("channel"),
        MessagePackBinary.GetEncodedStringBytes("support"),
        MessagePackBinary.GetEncodedStringBytes("device_id"),
        MessagePackBinary.GetEncodedStringBytes("is_pending"),
        MessagePackBinary.GetEncodedStringBytes("pubinfo")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestProgress[]>().Serialize(ref bytes, offset, value.quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_ChatChannelMasterParam[]>().Serialize(ref bytes, offset, value.channels, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.channel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.support);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.device_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_pending);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<LoginNewsInfo.JSON_PubInfo>().Serialize(ref bytes, offset, value.pubinfo, formatterResolver);
      return offset - num;
    }

    public FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_QuestProgress[] jsonQuestProgressArray = (JSON_QuestProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
      Json_ChatChannelMasterParam[] channelMasterParamArray = (Json_ChatChannelMasterParam[]) null;
      int num3 = 0;
      long num4 = 0;
      string str = (string) null;
      int num5 = 0;
      LoginNewsInfo.JSON_PubInfo jsonPubInfo = (LoginNewsInfo.JSON_PubInfo) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              jsonQuestProgressArray = formatterResolver.GetFormatterWithVerify<JSON_QuestProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              channelMasterParamArray = formatterResolver.GetFormatterWithVerify<Json_ChatChannelMasterParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 6:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              jsonPubInfo = formatterResolver.GetFormatterWithVerify<LoginNewsInfo.JSON_PubInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse()
      {
        quests = jsonQuestProgressArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2,
        channels = channelMasterParamArray,
        channel = num3,
        support = num4,
        device_id = str,
        is_pending = num5,
        pubinfo = jsonPubInfo
      };
    }
  }
}
