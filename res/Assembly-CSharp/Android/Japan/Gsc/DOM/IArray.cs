// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Gsc.DOM
{
  public interface IArray : IEnumerable<IValue>, IEnumerable
  {
    int Length { get; }

    IValue this[int index] { get; }
  }
}
