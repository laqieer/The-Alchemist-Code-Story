// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.EnumAsStringFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class EnumAsStringFormatter<T> : IMessagePackFormatter<T>, IMessagePackFormatter
  {
    private readonly Dictionary<string, T> nameValueMapping;
    private readonly Dictionary<T, string> valueNameMapping;

    public EnumAsStringFormatter()
    {
      string[] names = Enum.GetNames(typeof (T));
      Array values = Enum.GetValues(typeof (T));
      this.nameValueMapping = new Dictionary<string, T>(names.Length);
      this.valueNameMapping = new Dictionary<T, string>(names.Length);
      for (int index = 0; index < names.Length; ++index)
      {
        this.nameValueMapping[names[index]] = (T) values.GetValue(index);
        this.valueNameMapping[(T) values.GetValue(index)] = names[index];
      }
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      T value,
      IFormatterResolver formatterResolver)
    {
      string str;
      if (!this.valueNameMapping.TryGetValue(value, out str))
        str = value.ToString();
      return MessagePackBinary.WriteString(ref bytes, offset, str);
    }

    public T Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      string key = MessagePackBinary.ReadString(bytes, offset, out readSize);
      T obj;
      if (!this.nameValueMapping.TryGetValue(key, out obj))
        obj = (T) Enum.Parse(typeof (T), key);
      return obj;
    }
  }
}
