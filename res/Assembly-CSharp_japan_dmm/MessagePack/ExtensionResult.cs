// Decompiled with JetBrains decompiler
// Type: MessagePack.ExtensionResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack
{
  public struct ExtensionResult
  {
    public ExtensionResult(sbyte typeCode, byte[] data)
    {
      this.TypeCode = typeCode;
      this.Data = data;
    }

    public sbyte TypeCode { get; private set; }

    public byte[] Data { get; private set; }
  }
}
