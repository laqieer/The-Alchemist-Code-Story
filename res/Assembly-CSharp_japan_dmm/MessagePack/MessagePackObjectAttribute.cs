// Decompiled with JetBrains decompiler
// Type: MessagePack.MessagePackObjectAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
  public class MessagePackObjectAttribute : Attribute
  {
    public MessagePackObjectAttribute(bool keyAsPropertyName = false)
    {
      this.KeyAsPropertyName = keyAsPropertyName;
    }

    public bool KeyAsPropertyName { get; private set; }
  }
}
