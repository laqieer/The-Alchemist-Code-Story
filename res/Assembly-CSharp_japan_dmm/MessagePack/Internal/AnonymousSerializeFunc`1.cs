// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.AnonymousSerializeFunc`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Internal
{
  internal delegate int AnonymousSerializeFunc<T>(
    byte[][] stringByteKeysField,
    object[] customFormatters,
    ref byte[] bytes,
    int offset,
    T value,
    IFormatterResolver resolver);
}
