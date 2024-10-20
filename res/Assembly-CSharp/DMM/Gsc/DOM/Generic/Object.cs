﻿// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Object
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Gsc.DOM.Generic
{
  public class Object : IObject, IEnumerable<Member>, IEnumerable<IMember>, IEnumerable
  {
    private readonly List<Member> members;

    public Object() => this.members = new List<Member>();

    public int MemberCount => this.members.Count;

    private int IndexOf(string name)
    {
      return this.members.FindIndex((Predicate<Member>) (x => x.Name == name));
    }

    public bool HasMember(string name) => this.IndexOf(name) >= 0;

    public bool TryGetValue(string name, out Value value)
    {
      int index = this.IndexOf(name);
      if (index >= 0)
      {
        value = this.members[index].Value;
        return true;
      }
      value = new Value();
      return false;
    }

    bool IObject.TryGetValue(string name, out IValue value)
    {
      Value obj;
      bool flag = this.TryGetValue(name, out obj);
      value = (IValue) obj;
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

    public void Add(string name, Value value) => this.members.Add(new Member(name, value));

    public Value this[string name]
    {
      get
      {
        Value obj;
        if (this.TryGetValue(name, out obj))
          return obj;
        throw new KeyNotFoundException();
      }
      set => this.Add(name, value);
    }

    IValue IObject.this[string name] => (IValue) this[name];
  }
}