// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Member
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.DOM.Generic
{
  public struct Member : IMember
  {
    private readonly string name;
    private readonly Value value;

    public Member(string name, Value value)
    {
      this.name = name;
      this.value = value;
    }

    IValue IMember.Value
    {
      get
      {
        return (IValue) this.value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public Value Value
    {
      get
      {
        return this.value;
      }
    }
  }
}
