﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CollectionFormatterBase`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public abstract class CollectionFormatterBase<TElement, TCollection> : 
    CollectionFormatterBase<TElement, TCollection, TCollection>
    where TCollection : IEnumerable<TElement>
  {
    protected override sealed TCollection Complete(TCollection intermediateCollection)
    {
      return intermediateCollection;
    }
  }
}
