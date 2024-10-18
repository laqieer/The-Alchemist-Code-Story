// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Tasks;
using System.Collections;

#nullable disable
namespace Gsc.Network
{
  public interface IWebTask<TRequest, TResponse> : 
    IWebTask<TResponse>,
    IWebTask,
    IWebTaskBase,
    ITask,
    IEnumerator
    where TRequest : IRequest<TRequest, TResponse>
    where TResponse : IResponse<TResponse>
  {
    TRequest Request { get; }
  }
}
