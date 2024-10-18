// Decompiled with JetBrains decompiler
// Type: MessagePack.MessagePackFormatterAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
  public class MessagePackFormatterAttribute : Attribute
  {
    public MessagePackFormatterAttribute(Type formatterType) => this.FormatterType = formatterType;

    public MessagePackFormatterAttribute(Type formatterType, params object[] arguments)
    {
      this.FormatterType = formatterType;
      this.Arguments = arguments;
    }

    public Type FormatterType { get; private set; }

    public object[] Arguments { get; private set; }
  }
}
