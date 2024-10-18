// Decompiled with JetBrains decompiler
// Type: MessagePack.MessagePackCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack
{
  public static class MessagePackCode
  {
    public const byte MinFixInt = 0;
    public const byte MaxFixInt = 127;
    public const byte MinFixMap = 128;
    public const byte MaxFixMap = 143;
    public const byte MinFixArray = 144;
    public const byte MaxFixArray = 159;
    public const byte MinFixStr = 160;
    public const byte MaxFixStr = 191;
    public const byte Nil = 192;
    public const byte NeverUsed = 193;
    public const byte False = 194;
    public const byte True = 195;
    public const byte Bin8 = 196;
    public const byte Bin16 = 197;
    public const byte Bin32 = 198;
    public const byte Ext8 = 199;
    public const byte Ext16 = 200;
    public const byte Ext32 = 201;
    public const byte Float32 = 202;
    public const byte Float64 = 203;
    public const byte UInt8 = 204;
    public const byte UInt16 = 205;
    public const byte UInt32 = 206;
    public const byte UInt64 = 207;
    public const byte Int8 = 208;
    public const byte Int16 = 209;
    public const byte Int32 = 210;
    public const byte Int64 = 211;
    public const byte FixExt1 = 212;
    public const byte FixExt2 = 213;
    public const byte FixExt4 = 214;
    public const byte FixExt8 = 215;
    public const byte FixExt16 = 216;
    public const byte Str8 = 217;
    public const byte Str16 = 218;
    public const byte Str32 = 219;
    public const byte Array16 = 220;
    public const byte Array32 = 221;
    public const byte Map16 = 222;
    public const byte Map32 = 223;
    public const byte MinNegativeFixInt = 224;
    public const byte MaxNegativeFixInt = 255;
    private static readonly MessagePackType[] typeLookupTable = new MessagePackType[256];
    private static readonly string[] formatNameTable = new string[256];

    static MessagePackCode()
    {
      for (int index = 0; index <= (int) sbyte.MaxValue; ++index)
      {
        MessagePackCode.typeLookupTable[index] = MessagePackType.Integer;
        MessagePackCode.formatNameTable[index] = "positive fixint";
      }
      for (int index = 128; index <= 143; ++index)
      {
        MessagePackCode.typeLookupTable[index] = MessagePackType.Map;
        MessagePackCode.formatNameTable[index] = "fixmap";
      }
      for (int index = 144; index <= 159; ++index)
      {
        MessagePackCode.typeLookupTable[index] = MessagePackType.Array;
        MessagePackCode.formatNameTable[index] = "fixarray";
      }
      for (int index = 160; index <= 191; ++index)
      {
        MessagePackCode.typeLookupTable[index] = MessagePackType.String;
        MessagePackCode.formatNameTable[index] = "fixstr";
      }
      MessagePackCode.typeLookupTable[192] = MessagePackType.Nil;
      MessagePackCode.typeLookupTable[193] = MessagePackType.Unknown;
      MessagePackCode.typeLookupTable[194] = MessagePackType.Boolean;
      MessagePackCode.typeLookupTable[195] = MessagePackType.Boolean;
      MessagePackCode.typeLookupTable[196] = MessagePackType.Binary;
      MessagePackCode.typeLookupTable[197] = MessagePackType.Binary;
      MessagePackCode.typeLookupTable[198] = MessagePackType.Binary;
      MessagePackCode.typeLookupTable[199] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[200] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[201] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[202] = MessagePackType.Float;
      MessagePackCode.typeLookupTable[203] = MessagePackType.Float;
      MessagePackCode.typeLookupTable[204] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[205] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[206] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[207] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[208] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[209] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[210] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[211] = MessagePackType.Integer;
      MessagePackCode.typeLookupTable[212] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[213] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[214] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[215] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[216] = MessagePackType.Extension;
      MessagePackCode.typeLookupTable[217] = MessagePackType.String;
      MessagePackCode.typeLookupTable[218] = MessagePackType.String;
      MessagePackCode.typeLookupTable[219] = MessagePackType.String;
      MessagePackCode.typeLookupTable[220] = MessagePackType.Array;
      MessagePackCode.typeLookupTable[221] = MessagePackType.Array;
      MessagePackCode.typeLookupTable[222] = MessagePackType.Map;
      MessagePackCode.typeLookupTable[223] = MessagePackType.Map;
      MessagePackCode.formatNameTable[192] = "nil";
      MessagePackCode.formatNameTable[193] = "(never used)";
      MessagePackCode.formatNameTable[194] = "false";
      MessagePackCode.formatNameTable[195] = "true";
      MessagePackCode.formatNameTable[196] = "bin 8";
      MessagePackCode.formatNameTable[197] = "bin 16";
      MessagePackCode.formatNameTable[198] = "bin 32";
      MessagePackCode.formatNameTable[199] = "ext 8";
      MessagePackCode.formatNameTable[200] = "ext 16";
      MessagePackCode.formatNameTable[201] = "ext 32";
      MessagePackCode.formatNameTable[202] = "float 32";
      MessagePackCode.formatNameTable[203] = "float 64";
      MessagePackCode.formatNameTable[204] = "uint 8";
      MessagePackCode.formatNameTable[205] = "uint 16";
      MessagePackCode.formatNameTable[206] = "uint 32";
      MessagePackCode.formatNameTable[207] = "uint 64";
      MessagePackCode.formatNameTable[208] = "int 8";
      MessagePackCode.formatNameTable[209] = "int 16";
      MessagePackCode.formatNameTable[210] = "int 32";
      MessagePackCode.formatNameTable[211] = "int 64";
      MessagePackCode.formatNameTable[212] = "fixext 1";
      MessagePackCode.formatNameTable[213] = "fixext 2";
      MessagePackCode.formatNameTable[214] = "fixext 4";
      MessagePackCode.formatNameTable[215] = "fixext 8";
      MessagePackCode.formatNameTable[216] = "fixext 16";
      MessagePackCode.formatNameTable[217] = "str 8";
      MessagePackCode.formatNameTable[218] = "str 16";
      MessagePackCode.formatNameTable[219] = "str 32";
      MessagePackCode.formatNameTable[220] = "array 16";
      MessagePackCode.formatNameTable[221] = "array 32";
      MessagePackCode.formatNameTable[222] = "map 16";
      MessagePackCode.formatNameTable[223] = "map 32";
      for (int index = 224; index <= (int) byte.MaxValue; ++index)
      {
        MessagePackCode.typeLookupTable[index] = MessagePackType.Integer;
        MessagePackCode.formatNameTable[index] = "negative fixint";
      }
    }

    public static MessagePackType ToMessagePackType(byte code)
    {
      return MessagePackCode.typeLookupTable[(int) code];
    }

    public static string ToFormatName(byte code) => MessagePackCode.formatNameTable[(int) code];
  }
}
