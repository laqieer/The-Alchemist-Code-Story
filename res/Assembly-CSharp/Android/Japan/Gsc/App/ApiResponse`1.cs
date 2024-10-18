// Decompiled with JetBrains decompiler
// Type: Gsc.App.ApiResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App
{
  public abstract class ApiResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
  }
}
