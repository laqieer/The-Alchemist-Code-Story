﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.eSkillTargetExFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class eSkillTargetExFormatter : 
    IMessagePackFormatter<eSkillTargetEx>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      eSkillTargetEx value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteByte(ref bytes, offset, (byte) value);
    }

    public eSkillTargetEx Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return (eSkillTargetEx) MessagePackBinary.ReadByte(bytes, offset, out readSize);
    }
  }
}
