// Decompiled with JetBrains decompiler
// Type: Gsc.App.ApiRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App
{
  public abstract class ApiRequest<TRequest, TResponse> : Request<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
  }
}
