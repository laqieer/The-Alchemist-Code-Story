// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebCallback`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace Gsc.Network
{
  public interface IWebCallback<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    bool OnCallback(WebTask<TRequest, TResponse> task);
  }
}
