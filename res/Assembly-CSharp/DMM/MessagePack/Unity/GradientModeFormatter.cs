// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.GradientModeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class GradientModeFormatter : 
    IMessagePackFormatter<GradientMode>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      GradientMode value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt32(ref bytes, offset, (int) value);
    }

    public GradientMode Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return (GradientMode) MessagePackBinary.ReadInt32(bytes, offset, out readSize);
    }
  }
}
