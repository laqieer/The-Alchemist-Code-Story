﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.FlowNode_ReqAutoRepeatQuestStart_MP_AutoRepeatQuestStartResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class FlowNode_ReqAutoRepeatQuestStart_MP_AutoRepeatQuestStartResponseFormatter : 
    IMessagePackFormatter<FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public FlowNode_ReqAutoRepeatQuestStart_MP_AutoRepeatQuestStartResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "body",
          0
        },
        {
          "stat",
          1
        },
        {
          "stat_msg",
          2
        },
        {
          "stat_code",
          3
        },
        {
          "time",
          4
        },
        {
          "ticket",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("body"),
        MessagePackBinary.GetEncodedStringBytes("stat"),
        MessagePackBinary.GetEncodedStringBytes("stat_msg"),
        MessagePackBinary.GetEncodedStringBytes("stat_code"),
        MessagePackBinary.GetEncodedStringBytes("time"),
        MessagePackBinary.GetEncodedStringBytes("ticket")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ReqAutoRepeatQuestStart.Response>().Serialize(ref bytes, offset, value.body, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stat);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.stat_msg, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.stat_code, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.time);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ticket);
      return offset - num;
    }

    public FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ReqAutoRepeatQuestStart.Response response = (ReqAutoRepeatQuestStart.Response) null;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      long num4 = 0;
      int num5 = 0;
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
              response = formatterResolver.GetFormatterWithVerify<ReqAutoRepeatQuestStart.Response>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse questStartResponse = new FlowNode_ReqAutoRepeatQuestStart.MP_AutoRepeatQuestStartResponse();
      questStartResponse.body = response;
      questStartResponse.stat = num3;
      questStartResponse.stat_msg = str1;
      questStartResponse.stat_code = str2;
      questStartResponse.time = num4;
      questStartResponse.ticket = num5;
      return questStartResponse;
    }
  }
}
