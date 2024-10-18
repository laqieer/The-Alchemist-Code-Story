// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.StaticNullableFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class StaticNullableFormatter<T> : IMessagePackFormatter<T?>, IMessagePackFormatter where T : struct
  {
    private readonly IMessagePackFormatter<T> underlyingFormatter;

    public StaticNullableFormatter(IMessagePackFormatter<T> underlyingFormatter)
    {
      this.underlyingFormatter = underlyingFormatter;
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      T? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : this.underlyingFormatter.Serialize(ref bytes, offset, value.Value, formatterResolver);
    }

    public T? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new T?(this.underlyingFormatter.Deserialize(bytes, offset, formatterResolver, out readSize));
      readSize = 1;
      return new T?();
    }
  }
}
