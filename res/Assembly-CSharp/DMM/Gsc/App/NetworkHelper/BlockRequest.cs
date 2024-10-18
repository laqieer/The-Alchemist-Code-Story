// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.BlockRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;

#nullable disable
namespace Gsc.App.NetworkHelper
{
  public static class BlockRequest
  {
    public static BlockRequest<TRequest, TResponse> Create<TRequest, TResponse>(
      IRequest<TRequest, TResponse> request)
      where TRequest : IRequest<TRequest, TResponse>
      where TResponse : IResponse<TResponse>
    {
      return new BlockRequest<TRequest, TResponse>(WebInternalTask.Create<TRequest, TResponse>(request));
    }
  }
}
