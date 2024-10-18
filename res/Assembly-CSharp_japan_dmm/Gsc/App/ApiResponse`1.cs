// Decompiled with JetBrains decompiler
// Type: Gsc.App.ApiResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;

#nullable disable
namespace Gsc.App
{
  public abstract class ApiResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
  }
}
