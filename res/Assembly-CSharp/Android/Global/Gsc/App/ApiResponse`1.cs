// Decompiled with JetBrains decompiler
// Type: Gsc.App.ApiResponse`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App
{
  public abstract class ApiResponse<TResponse> : Response<TResponse> where TResponse : IResponse<TResponse>
  {
  }
}
