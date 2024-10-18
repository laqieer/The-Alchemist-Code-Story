// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Gsc.DOM
{
  public interface IObject : IEnumerable<IMember>, IEnumerable
  {
    int MemberCount { get; }

    bool HasMember(string name);

    bool TryGetValue(string name, out IValue value);

    IValue this[string name] { get; }
  }
}
