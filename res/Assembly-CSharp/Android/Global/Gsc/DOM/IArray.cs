// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Gsc.DOM
{
  public interface IArray : IEnumerable, IEnumerable<IValue>
  {
    int Length { get; }

    IValue this[int index] { get; }
  }
}
