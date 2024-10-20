﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask<TResponse> : IEnumerator, IWebTaskBase, IWebTask, ITask where TResponse : IResponse<TResponse>
  {
    TResponse Response { get; }

    IErrorResponse ErrorResponse { get; }
  }
}