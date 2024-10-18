// Decompiled with JetBrains decompiler
// Type: MessagePack.UnionAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  public class UnionAttribute : Attribute
  {
    public UnionAttribute(int key, Type subType)
    {
      this.Key = key;
      this.SubType = subType;
    }

    public int Key { get; private set; }

    public Type SubType { get; private set; }
  }
}
