// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.AnonymousSerializableFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;

#nullable disable
namespace MessagePack.Internal
{
  internal class AnonymousSerializableFormatter<T> : IMessagePackFormatter<T>, IMessagePackFormatter
  {
    private readonly byte[][] stringByteKeysField;
    private readonly object[] serializeCustomFormatters;
    private readonly object[] deserializeCustomFormatters;
    private readonly AnonymousSerializeFunc<T> serialize;
    private readonly AnonymousDeserializeFunc<T> deserialize;

    public AnonymousSerializableFormatter(
      byte[][] stringByteKeysField,
      object[] serializeCustomFormatters,
      object[] deserializeCustomFormatters,
      AnonymousSerializeFunc<T> serialize,
      AnonymousDeserializeFunc<T> deserialize)
    {
      this.stringByteKeysField = stringByteKeysField;
      this.serializeCustomFormatters = serializeCustomFormatters;
      this.deserializeCustomFormatters = deserializeCustomFormatters;
      this.serialize = serialize;
      this.deserialize = deserialize;
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      T value,
      IFormatterResolver formatterResolver)
    {
      if (this.serialize == null)
        throw new InvalidOperationException(this.GetType().Name + " does not support Serialize.");
      return this.serialize(this.stringByteKeysField, this.serializeCustomFormatters, ref bytes, offset, value, formatterResolver);
    }

    public T Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (this.deserialize == null)
        throw new InvalidOperationException(this.GetType().Name + " does not support Deserialize.");
      return this.deserialize(this.deserializeCustomFormatters, bytes, offset, formatterResolver, out readSize);
    }
  }
}
