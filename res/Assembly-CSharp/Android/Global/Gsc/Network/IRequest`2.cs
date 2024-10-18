// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Network
{
  public interface IRequest<TRequest, TResponse> : IRequest where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
  }
}
