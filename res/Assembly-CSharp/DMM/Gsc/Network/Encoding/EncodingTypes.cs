// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Encoding.EncodingTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Network.Encoding
{
  public class EncodingTypes
  {
    private static readonly string BASE = "application/octet-stream";
    private static readonly string BCT_JSON_SERIALIZED = "jhotuhiahanoatuhinga";
    private static readonly string BCT_MESSAGE_PACK_SERIALIZED = "karerepokai";
    private static readonly string BCT_AES_ENCRYPTED = "fakamunatanga";
    public static readonly string BCT_JSON_AES = EncodingTypes.BASE + "+" + EncodingTypes.BCT_JSON_SERIALIZED + "+" + EncodingTypes.BCT_AES_ENCRYPTED;
    public static readonly string BCT_MESSAGEPACK = EncodingTypes.BASE + "+" + EncodingTypes.BCT_MESSAGE_PACK_SERIALIZED;
    public static readonly string BCT_MESSAGEPACK_AES = EncodingTypes.BASE + "+" + EncodingTypes.BCT_MESSAGE_PACK_SERIALIZED + "+" + EncodingTypes.BCT_AES_ENCRYPTED;
    public static readonly string BCT_NO_EXTRA_KEY_SALT = "noeks";

    public static CompressMode GetCompressModeFromSerializeCompressMethod(
      EncodingTypes.ESerializeCompressMethod method)
    {
      return method == EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK_LZ4 ? CompressMode.Lz4 : CompressMode.None;
    }

    public static bool IsJsonSerializeCompressSelected(EncodingTypes.ESerializeCompressMethod method)
    {
      return method == EncodingTypes.ESerializeCompressMethod.JSON;
    }

    public enum ESerializeCompressMethod
    {
      JSON,
      TYPED_MESSAGE_PACK,
      TYPED_MESSAGE_PACK_LZ4,
    }
  }
}
