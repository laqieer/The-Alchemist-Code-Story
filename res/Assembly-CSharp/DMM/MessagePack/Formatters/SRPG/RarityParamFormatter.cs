// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RarityParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RarityParamFormatter : 
    IMessagePackFormatter<RarityParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RarityParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "UnitLvCap",
          0
        },
        {
          "UnitJobLvCap",
          1
        },
        {
          "UnitAwakeLvCap",
          2
        },
        {
          "UnitUnlockPieceNum",
          3
        },
        {
          "UnitChangePieceNum",
          4
        },
        {
          "UnitSelectChangePieceNum",
          5
        },
        {
          "UnitRarityUpCost",
          6
        },
        {
          "PieceToPoint",
          7
        },
        {
          "DropSE",
          8
        },
        {
          "EquipEnhanceParam",
          9
        },
        {
          "ArtifactLvCap",
          10
        },
        {
          "ArtifactCostRate",
          11
        },
        {
          "ArtifactCreatePieceNum",
          12
        },
        {
          "ArtifactGouseiPieceNum",
          13
        },
        {
          "ArtifactChangePieceNum",
          14
        },
        {
          "ArtifactCreateCost",
          15
        },
        {
          "ArtifactRarityUpCost",
          16
        },
        {
          "ArtifactChangeCost",
          17
        },
        {
          "ConceptCardLvCap",
          18
        },
        {
          "ConceptCardAwakeCountMax",
          19
        },
        {
          "GachaChangePieceCoinNum",
          20
        },
        {
          "GrowStatus",
          21
        }
      };
      this.____stringByteKeys = new byte[22][]
      {
        MessagePackBinary.GetEncodedStringBytes("UnitLvCap"),
        MessagePackBinary.GetEncodedStringBytes("UnitJobLvCap"),
        MessagePackBinary.GetEncodedStringBytes("UnitAwakeLvCap"),
        MessagePackBinary.GetEncodedStringBytes("UnitUnlockPieceNum"),
        MessagePackBinary.GetEncodedStringBytes("UnitChangePieceNum"),
        MessagePackBinary.GetEncodedStringBytes("UnitSelectChangePieceNum"),
        MessagePackBinary.GetEncodedStringBytes("UnitRarityUpCost"),
        MessagePackBinary.GetEncodedStringBytes("PieceToPoint"),
        MessagePackBinary.GetEncodedStringBytes("DropSE"),
        MessagePackBinary.GetEncodedStringBytes("EquipEnhanceParam"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactLvCap"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactCostRate"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactCreatePieceNum"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactGouseiPieceNum"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactChangePieceNum"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactCreateCost"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactRarityUpCost"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactChangeCost"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardLvCap"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCardAwakeCountMax"),
        MessagePackBinary.GetEncodedStringBytes("GachaChangePieceCoinNum"),
        MessagePackBinary.GetEncodedStringBytes("GrowStatus")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RarityParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 22);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitJobLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitAwakeLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitUnlockPieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitChangePieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitSelectChangePieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UnitRarityUpCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.PieceToPoint, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DropSE, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<RarityEquipEnhanceParam>().Serialize(ref bytes, offset, value.EquipEnhanceParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactCostRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactCreatePieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactGouseiPieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactChangePieceNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactCreateCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactRarityUpCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ArtifactChangeCost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ConceptCardLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ConceptCardAwakeCountMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.GachaChangePieceCoinNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<StatusParam>().Serialize(ref bytes, offset, value.GrowStatus, formatterResolver);
      return offset - num;
    }

    public RarityParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RarityParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      OInt oint7 = new OInt();
      OInt oint8 = new OInt();
      string str = (string) null;
      RarityEquipEnhanceParam equipEnhanceParam = (RarityEquipEnhanceParam) null;
      OInt oint9 = new OInt();
      OInt oint10 = new OInt();
      OInt oint11 = new OInt();
      OInt oint12 = new OInt();
      OInt oint13 = new OInt();
      OInt oint14 = new OInt();
      OInt oint15 = new OInt();
      OInt oint16 = new OInt();
      OInt oint17 = new OInt();
      OInt oint18 = new OInt();
      OInt oint19 = new OInt();
      StatusParam statusParam = (StatusParam) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              equipEnhanceParam = formatterResolver.GetFormatterWithVerify<RarityEquipEnhanceParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              oint10 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              oint11 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              oint12 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              oint13 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              oint14 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              oint15 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              oint16 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              oint17 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              oint18 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              oint19 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              statusParam = formatterResolver.GetFormatterWithVerify<StatusParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RarityParam()
      {
        UnitLvCap = oint1,
        UnitJobLvCap = oint2,
        UnitAwakeLvCap = oint3,
        UnitUnlockPieceNum = oint4,
        UnitChangePieceNum = oint5,
        UnitSelectChangePieceNum = oint6,
        UnitRarityUpCost = oint7,
        PieceToPoint = oint8,
        DropSE = str,
        EquipEnhanceParam = equipEnhanceParam,
        ArtifactLvCap = oint9,
        ArtifactCostRate = oint10,
        ArtifactCreatePieceNum = oint11,
        ArtifactGouseiPieceNum = oint12,
        ArtifactChangePieceNum = oint13,
        ArtifactCreateCost = oint14,
        ArtifactRarityUpCost = oint15,
        ArtifactChangeCost = oint16,
        ConceptCardLvCap = oint17,
        ConceptCardAwakeCountMax = oint18,
        GachaChangePieceCoinNum = oint19,
        GrowStatus = statusParam
      };
    }
  }
}
