// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Object
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Gsc.DOM.Json
{
  public struct Object : IObject, IEnumerable<Member>, IEnumerable<IMember>, IEnumerable
  {
    private readonly rapidjson.Object value;

    public Object(rapidjson.Object value) => this.value = value;

    public int MemberCount => this.value.MemberCount;

    public bool HasMember(string name) => this.value.HasMember(name);

    public bool TryGetValue(string name, out Value value)
    {
      rapidjson.Value obj;
      bool flag = this.value.TryGetValue(name, out obj);
      value = new Value(obj);
      return flag;
    }

    bool IObject.TryGetValue(string name, out IValue value)
    {
      rapidjson.Value obj;
      bool flag = this.value.TryGetValue(name, out obj);
      value = (IValue) new Value(obj);
      return flag;
    }

    [DebuggerHidden]
    public IEnumerator<Member> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Member>) new Object.\u003CGetEnumerator\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    IEnumerator<IMember> IEnumerable<IMember>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IMember>) new Object.\u003CSystem_Collections_Generic_IEnumerable\u003CGsc_DOM_IMember\u003E_GetEnumerator\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public Value this[string name] => new Value(this.value[name]);

    IValue IObject.this[string name] => (IValue) this[name];
  }
}
