// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Member
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    IValue IMember.Value
    {
      get
      {
        return (IValue) this.value;
      }
    }
  }
}
