// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.BlockRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App.NetworkHelper
{
  public static class BlockRequest
  {
    public static BlockRequest<TRequest, TResponse> Create<TRequest, TResponse>(IRequest<TRequest, TResponse> request) where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
    {
      return new BlockRequest<TRequest, TResponse>(WebInternalTask.Create<TRequest, TResponse>(request));
    }
  }
}
