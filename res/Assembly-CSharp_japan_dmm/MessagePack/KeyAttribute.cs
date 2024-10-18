// Decompiled with JetBrains decompiler
// Type: MessagePack.KeyAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class KeyAttribute : Attribute
  {
    public KeyAttribute(int x) => this.IntKey = new int?(x);

    public KeyAttribute(string x) => this.StringKey = x;

    public int? IntKey { get; private set; }

    public string StringKey { get; private set; }
  }
}
