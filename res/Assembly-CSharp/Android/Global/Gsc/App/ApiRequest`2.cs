﻿// Decompiled with JetBrains decompiler
// Type: Gsc.App.ApiRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App
{
  public abstract class ApiRequest<TRequest, TResponse> : Request<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
  }
}