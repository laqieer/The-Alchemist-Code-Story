﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Network.YieldCallback`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;

#nullable disable
namespace Gsc.Network
{
  public delegate IEnumerator YieldCallback<TRequest, TResponse>(
    TRequest request,
    TResponse response);
}
