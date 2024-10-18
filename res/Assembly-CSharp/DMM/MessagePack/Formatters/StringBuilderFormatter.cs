// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.StringBuilderFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class StringBuilderFormatter : 
    IMessagePackFormatter<StringBuilder>,
    IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<StringBuilder> Instance = (IMessagePackFormatter<StringBuilder>) new StringBuilderFormatter();

    private StringBuilderFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      StringBuilder value,
      IFormatterResolver formatterResolver)
    {
      return value == null ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteString(ref bytes, offset, value.ToString());
    }

    public StringBuilder Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new StringBuilder(MessagePackBinary.ReadString(bytes, offset, out readSize));
      readSize = 1;
      return (StringBuilder) null;
    }
  }
}
