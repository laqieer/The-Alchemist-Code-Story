// Decompiled with JetBrains decompiler
// Type: MessagePack.IFormatterResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;

#nullable disable
namespace MessagePack
{
  public interface IFormatterResolver
  {
    IMessagePackFormatter<T> GetFormatter<T>();
  }
}
