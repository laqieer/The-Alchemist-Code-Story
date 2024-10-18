// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CollectionFormatterBase`3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public abstract class CollectionFormatterBase<TElement, TIntermediate, TCollection> : 
    CollectionFormatterBase<TElement, TIntermediate, IEnumerator<TElement>, TCollection>
    where TCollection : IEnumerable<TElement>
  {
    protected override IEnumerator<TElement> GetSourceEnumerator(TCollection source)
    {
      return source.GetEnumerator();
    }
  }
}
