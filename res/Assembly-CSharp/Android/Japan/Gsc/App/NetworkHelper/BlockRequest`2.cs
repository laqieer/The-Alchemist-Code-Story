﻿// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.BlockRequest`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Network;
using System.Threading;

namespace Gsc.App.NetworkHelper
{
  public class BlockRequest<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    protected WebInternalTask<TRequest, TResponse> task;

    public BlockRequest(WebInternalTask<TRequest, TResponse> task)
    {
      this.task = task;
    }

    private void Wait()
    {
      if (this.task.isDone)
        return;
      this.task.OnStart();
      float webapiTimeoutSec = SRPG.Network.WEBAPI_TIMEOUT_SEC;
      while (this.task.MoveNext())
      {
        webapiTimeoutSec -= 0.5f;
        if (0.0 <= (double) webapiTimeoutSec)
          Thread.Sleep(500);
        else
          break;
      }
      this.task.OnFinish();
    }

    public WebTaskResult GetResult()
    {
      this.Wait();
      return this.task.Result;
    }

    public TResponse GetResponse()
    {
      this.Wait();
      return this.task.Response;
    }

    public IErrorResponse GetErrorResponse()
    {
      this.Wait();
      return this.task.ErrorResponse;
    }
  }
}
