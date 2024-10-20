﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Gsc.Network
{
  public abstract class WebInternalTask : IWebTaskBase, ITask, IEnumerator
  {
    private int retryCount;
    private bool completed;
    private UnityWebRequest webRequest;
    private readonly string method;
    private readonly string uri;
    private readonly byte[] payload;
    private readonly CustomHeaders customHeaders;
    private WebInternalTask.WaitTask waitTask;
    private object subroutine;

    public WebInternalTask(string method, string uri, byte[] payload, CustomHeaders customHeaders)
    {
      this.method = method;
      this.uri = uri;
      this.payload = payload;
      this.customHeaders = customHeaders;
    }

    public WebTaskResult Result { get; protected set; }

    public bool isBreak { get; private set; }

    public bool isDone
    {
      get
      {
        if (!this.completed)
          return this.isBreak;
        return true;
      }
    }

    public static WebInternalTask<TRequest, TResponse> Create<TRequest, TResponse>(IRequest<TRequest, TResponse> request) where TRequest : IRequest<TRequest, TResponse>, IRequest where TResponse : IResponse<TResponse>
    {
      return new WebInternalTask<TRequest, TResponse>(request);
    }

    private void Update()
    {
      if (this.webRequest == null)
      {
        this.webRequest = WebInternalTask.CreateRequest(this.method, this.uri, this.payload, this.customHeaders);
        this.webRequest.timeout = 30;
        this.subroutine = (object) this.webRequest.Send();
      }
      else
      {
        if (!this.webRequest.isDone)
          return;
        WebInternalResponse response = !this.webRequest.isError || !(this.webRequest.error == "Request timeout") ? new WebInternalResponse(this.webRequest) : new WebInternalResponse(504);
        int statusCode = response.StatusCode;
        try
        {
          if ((statusCode == 0 || 500 <= statusCode && statusCode <= 599) && (statusCode != 503 && statusCode != 504) && ++this.retryCount < 3)
          {
            this.waitTask = new WebInternalTask.WaitTask();
          }
          else
          {
            this.Result = this.ProcessResponse(response);
            this.completed = true;
          }
        }
        finally
        {
          this.InternalDispose();
        }
      }
    }

    protected abstract WebTaskResult ProcessResponse(WebInternalResponse response);

    public void Break()
    {
      this.isBreak = true;
    }

    public void Reset()
    {
      if (this.isBreak)
        return;
      this.retryCount = 0;
      this.completed = false;
    }

    public void OnStart()
    {
      this.Reset();
    }

    public void OnFinish()
    {
    }

    public IEnumerator Run()
    {
      return (IEnumerator) this;
    }

    public object Current
    {
      get
      {
        return this.subroutine;
      }
    }

    public bool MoveNext()
    {
      this.subroutine = (object) null;
      if (this.waitTask != null && this.waitTask.Wait())
        return true;
      this.waitTask = (WebInternalTask.WaitTask) null;
      if (!this.isDone)
        this.Update();
      if (!this.isDone)
        return true;
      this.InternalDispose();
      return false;
    }

    private void InternalDispose()
    {
      if (this.webRequest == null)
        return;
      this.webRequest.Dispose();
      this.webRequest = (UnityWebRequest) null;
    }

    private static UnityWebRequest CreateRequest(string method, string uri, byte[] payload, CustomHeaders customHeaders)
    {
      UnityWebRequest unityWebRequest = new UnityWebRequest(uri);
      unityWebRequest.method = method;
      if (method == "GET")
        unityWebRequest.uploadHandler = (UploadHandler) null;
      else if (payload != null)
        unityWebRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(payload);
      unityWebRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
      customHeaders.Dispatch(new Action<string, string>(unityWebRequest.SetRequestHeader));
      return unityWebRequest;
    }

    private class WaitTask
    {
      private readonly float time;

      public WaitTask()
      {
        this.time = Time.unscaledTime;
      }

      public bool Wait()
      {
        return (double) (Time.unscaledTime - this.time) < 1.0;
      }
    }
  }
}