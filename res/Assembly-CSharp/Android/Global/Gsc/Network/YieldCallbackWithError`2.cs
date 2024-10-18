// Decompiled with JetBrains decompiler
// Type: Gsc.Network.YieldCallbackWithError`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;

namespace Gsc.Network
{
  public delegate IEnumerator YieldCallbackWithError<TRequest, TResponse>(TRequest request, TResponse response, IErrorResponse error);
}
