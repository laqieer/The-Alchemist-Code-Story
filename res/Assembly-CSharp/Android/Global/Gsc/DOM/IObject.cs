// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

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
