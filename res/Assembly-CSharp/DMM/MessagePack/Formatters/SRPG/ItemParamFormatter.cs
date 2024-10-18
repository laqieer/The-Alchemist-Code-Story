// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ItemParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ItemParamFormatter : IMessagePackFormatter<ItemParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ItemParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Expr",
          0
        },
        {
          "Flavor",
          1
        },
        {
          "Recipe",
          2
        },
        {
          "IsCommon",
          3
        },
        {
          "IsLimited",
          4
        },
        {
          "IsExpire",
          5
        },
        {
          "no",
          6
        },
        {
          "iname",
          7
        },
        {
          "name",
          8
        },
        {
          "type",
          9
        },
        {
          "tabtype",
          10
        },
        {
          "rare",
          11
        },
        {
          "cap",
          12
        },
        {
          "invcap",
          13
        },
        {
          "equipLv",
          14
        },
        {
          "coin",
          15
        },
        {
          "tour_coin",
          16
        },
        {
          "arena_coin",
          17
        },
        {
          "multi_coin",
          18
        },
        {
          "piece_point",
          19
        },
        {
          "buy",
          20
        },
        {
          "sell",
          21
        },
        {
          "enhace_cost",
          22
        },
        {
          "enhace_point",
          23
        },
        {
          "facility_point",
          24
        },
        {
          "value",
          25
        },
        {
          "icon",
          26
        },
        {
          "skill",
          27
        },
        {
          "recipe",
          28
        },
        {
          "is_valuables",
          29
        },
        {
          "cmn_type",
          30
        },
        {
          "gallery_view",
          31
        },
        {
          "begin_at",
          32
        },
        {
          "end_at",
          33
        }
      };
      this.____stringByteKeys = new byte[34][]
      {
        MessagePackBinary.GetEncodedStringBytes("Expr"),
        MessagePackBinary.GetEncodedStringBytes("Flavor"),
        MessagePackBinary.GetEncodedStringBytes("Recipe"),
        MessagePackBinary.GetEncodedStringBytes("IsCommon"),
        MessagePackBinary.GetEncodedStringBytes("IsLimited"),
        MessagePackBinary.GetEncodedStringBytes("IsExpire"),
        MessagePackBinary.GetEncodedStringBytes("no"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("tabtype"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("cap"),
        MessagePackBinary.GetEncodedStringBytes("invcap"),
        MessagePackBinary.GetEncodedStringBytes("equipLv"),
        MessagePackBinary.GetEncodedStringBytes("coin"),
        MessagePackBinary.GetEncodedStringBytes("tour_coin"),
        MessagePackBinary.GetEncodedStringBytes("arena_coin"),
        MessagePackBinary.GetEncodedStringBytes("multi_coin"),
        MessagePackBinary.GetEncodedStringBytes("piece_point"),
        MessagePackBinary.GetEncodedStringBytes("buy"),
        MessagePackBinary.GetEncodedStringBytes("sell"),
        MessagePackBinary.GetEncodedStringBytes("enhace_cost"),
        MessagePackBinary.GetEncodedStringBytes("enhace_point"),
        MessagePackBinary.GetEncodedStringBytes("facility_point"),
        MessagePackBinary.GetEncodedStringBytes("value"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("recipe"),
        MessagePackBinary.GetEncodedStringBytes("is_valuables"),
        MessagePackBinary.GetEncodedStringBytes("cmn_type"),
        MessagePackBinary.GetEncodedStringBytes("gallery_view"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ItemParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 34);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Flavor, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<RecipeParam>().Serialize(ref bytes, offset, value.Recipe, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCommon);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsLimited);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsExpire);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<EItemType>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<EItemTabType>().Serialize(ref bytes, offset, value.tabtype, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.invcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.equipLv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tour_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arena_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multi_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.piece_point);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sell);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.enhace_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.enhace_point);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.facility_point);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.value);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_valuables);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.cmn_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Serialize(ref bytes, offset, value.gallery_view, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<DateTime>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<DateTime>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      return offset - num;
    }

    public ItemParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ItemParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      RecipeParam recipeParam = (RecipeParam) null;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      int num3 = 0;
      string str3 = (string) null;
      string str4 = (string) null;
      EItemType eitemType = EItemType.Used;
      EItemTabType eitemTabType = EItemTabType.None;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      int num18 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      bool flag4 = false;
      byte num19 = 0;
      GalleryVisibilityType galleryVisibilityType = GalleryVisibilityType.None;
      DateTime dateTime1 = new DateTime();
      DateTime dateTime2 = new DateTime();
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num20;
        if (!this.____keyMapping.TryGetValueSafe(key, out num20))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num20)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              recipeParam = formatterResolver.GetFormatterWithVerify<RecipeParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              eitemType = formatterResolver.GetFormatterWithVerify<EItemType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              eitemTabType = formatterResolver.GetFormatterWithVerify<EItemTabType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 30:
              num19 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 31:
              galleryVisibilityType = formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              dateTime1 = formatterResolver.GetFormatterWithVerify<DateTime>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              dateTime2 = formatterResolver.GetFormatterWithVerify<DateTime>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ItemParam()
      {
        no = num3,
        iname = str3,
        name = str4,
        type = eitemType,
        tabtype = eitemTabType,
        rare = num4,
        cap = num5,
        invcap = num6,
        equipLv = num7,
        coin = num8,
        tour_coin = num9,
        arena_coin = num10,
        multi_coin = num11,
        piece_point = num12,
        buy = num13,
        sell = num14,
        enhace_cost = num15,
        enhace_point = num16,
        facility_point = num17,
        value = num18,
        icon = str5,
        skill = str6,
        recipe = str7,
        is_valuables = flag4,
        cmn_type = num19,
        gallery_view = galleryVisibilityType,
        begin_at = dateTime1,
        end_at = dateTime2
      };
    }
  }
}
