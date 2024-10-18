// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTask`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.App;
using Gsc.Core;
using Gsc.Tasks;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;

#nullable disable
namespace Gsc.Network
{
  public class WebTask<TRequest, TResponse> : 
    IWebTask<TRequest, TResponse>,
    IWebTask<TResponse>,
    IWebTask,
    IWebTaskBase,
    ITask,
    IEnumerator
    where TRequest : IRequest<TRequest, TResponse>
    where TResponse : IResponse<TResponse>
  {
    private WebInternalTask<TRequest, TResponse> internalTask;
    private WebTaskAttribute attributes;

    private WebTask(WebTaskAttribute attributes) => this.attributes = attributes;

    public IWebCallback<TRequest, TResponse> callback { get; private set; }

    public WebTaskResult acceptResults { get; private set; }

    public TRequest Request { get; private set; }

    public TResponse Response { get; private set; }

    public IErrorResponse ErrorResponse { get; private set; }

    public byte[] error { get; private set; }

    public bool isBreak { get; private set; }

    public bool isDone { get; private set; }

    public WebTaskResult Result { get; private set; }

    public bool handled { get; private set; }

    public static WebTask<TRequest, TResponse> Send(
      IRequest<TRequest, TResponse> request,
      WebTaskAttribute attributes)
    {
      WebTask<TRequest, TResponse> task = new WebTask<TRequest, TResponse>(attributes);
      task.Request = (TRequest) request;
      WebQueue.defaultQueue.Add((IWebTask) task);
      return task;
    }

    public Type GetRequestType() => typeof (TRequest);

    public bool IsAcceptResult(WebTaskResult result)
    {
      return (this.acceptResults & result) > WebTaskResult.None;
    }

    public bool HasAttributes(WebTaskAttribute attributes)
    {
      return (this.attributes & attributes) == attributes;
    }

    public void Retry()
    {
      if (this.internalTask != null)
        return;
      this.Reset();
      this.attributes |= WebTaskAttribute.Interrupt;
      WebQueue.defaultQueue.Add((IWebTask) this);
      WebQueue.defaultQueue.Pause(false);
    }

    public void Break()
    {
      if (this.internalTask == null)
        return;
      this.internalTask.Break();
    }

    public WebTask<TRequest, TResponse> SetAcceptResults(WebTaskResult handleResults)
    {
      this.acceptResults = handleResults;
      return this;
    }

    public WebInternalTask GetInternalTask() => (WebInternalTask) this.internalTask;

    public object Current => (object) null;

    public void Reset()
    {
      this.Response = default (TResponse);
      this.ErrorResponse = (IErrorResponse) null;
      this.error = (byte[]) null;
      this.isBreak = false;
      this.isDone = false;
      this.Result = WebTaskResult.None;
      this.handled = false;
    }

    public bool MoveNext() => !this.isDone;

    public WebTask<TRequest, TResponse> OnResponse(VoidCallback<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnResponse(VoidCallbackWithError<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnCoroutineResponse(YieldCallback<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnCoroutineResponse(
      YieldCallbackWithError<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public IEnumerator Run() => (IEnumerator) this.internalTask;

    public void OnStart()
    {
      this.internalTask = WebInternalTask.Create<TRequest, TResponse>((IRequest<TRequest, TResponse>) this.Request);
      this.internalTask.OnStart();
    }

    public void OnFinish()
    {
      this.internalTask.OnFinish();
      this.Response = this.internalTask.Response;
      this.ErrorResponse = this.internalTask.ErrorResponse;
      this.error = this.internalTask.error;
      this.isBreak = this.internalTask.isBreak;
      this.isDone = this.internalTask.isDone;
      this.Result = this.internalTask.Result;
      this.internalTask = (WebInternalTask<TRequest, TResponse>) null;
      if (this.callback == null)
        return;
      this.handled = this.callback.OnCallback(this);
    }

    private static WebTaskResult GetTaskResult(WebInternalResponse response)
    {
      int statusCode = response.StatusCode;
      byte[] payload = response.Payload;
      if (statusCode == 0)
        return WebTaskResult.ServerError;
      if (200 <= statusCode && statusCode <= 299)
        return WebTaskResult.Success;
      switch (statusCode)
      {
        case 401:
          return payload != null && payload.Length > 0 && Encoding.UTF8.GetString(payload).ToUpper() == "EXPIRED TOKEN" ? WebTaskResult.InternalExpiredTokenError : WebTaskResult.ExpiredSessionError;
        case 471:
          return WebTaskResult.UpdateApplication;
        case 472:
          return WebTaskResult.UpdateResource;
        case 479:
          return WebTaskResult.Maintenance;
        case 498:
          return WebTaskResult.Interrupt;
        case 499:
          return WebTaskResult.MustErrorHandle;
        default:
          return 500 <= statusCode && statusCode <= 599 ? WebTaskResult.ServerError : WebTaskResult.UnknownError;
      }
    }

    public static WebTaskResult TryGetResponse(
      TRequest request,
      WebInternalResponse internalResponse,
      out TResponse response,
      out IErrorResponse error)
    {
      error = (IErrorResponse) null;
      response = default (TResponse);
      byte[] payload = internalResponse.Payload;
      WebTaskResult response1 = request.InquireResult(WebTask<TRequest, TResponse>.GetTaskResult(internalResponse), internalResponse);
      if (WebInternalResponse.RETRY_FOR_CHECK_MENTE_FLAG.Equals((object) payload))
        return WebTaskResult.InternalCheckMaintenance;
      try
      {
        if (response1 == WebTaskResult.Success)
        {
          try
          {
            response = AssemblySupport.CreateInstance<TResponse>((object) internalResponse);
          }
          catch (MissingMethodException ex)
          {
            response = AssemblySupport.CreateInstance<TResponse>((object) payload);
          }
        }
        if (response1 == WebTaskResult.MustErrorHandle)
          error = AssemblySupport.CreateInstance<IErrorResponse>(request.GetErrorResponseType(), (object) internalResponse);
      }
      catch (TargetInvocationException ex)
      {
        if (ex.InnerException != null && (object) ex.InnerException.GetType() == (object) typeof (MissingFieldException))
          return WebTaskResult.InvalidChkver2Response;
        WebQueueListener.ErrorPayload = payload;
        Debug.LogError((object) Encoding.UTF8.GetString(payload));
        Debug.Log((object) ex);
        return WebTaskResult.ParseError;
      }
      return response1;
    }

    public class ParseError : Exception
    {
      public ParseError(byte[] payload, TargetInvocationException e)
        : base(string.Format("<{0}, {1}>: {2}", (object) typeof (TRequest), (object) typeof (TResponse), (object) Encoding.UTF8.GetString(payload)), (Exception) e)
      {
      }
    }
  }
}
