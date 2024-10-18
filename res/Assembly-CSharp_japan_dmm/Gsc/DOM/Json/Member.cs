// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Member
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.DOM.Json
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

    public string Name => this.name;

    public Value Value => this.value;

    IValue IMember.Value => (IValue) this.value;
  }
}
