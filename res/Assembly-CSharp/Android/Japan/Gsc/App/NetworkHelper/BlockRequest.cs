// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.BlockRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
