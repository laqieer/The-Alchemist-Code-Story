// Decompiled with JetBrains decompiler
// Type: MessagePack.ExtensionHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack
{
  public struct ExtensionHeader
  {
    public ExtensionHeader(sbyte typeCode, uint length)
    {
      this.TypeCode = typeCode;
      this.Length = length;
    }

    public sbyte TypeCode { get; private set; }

    public uint Length { get; private set; }
  }
}
