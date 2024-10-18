// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Request`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.Network.Encoding;
using Gsc.Network.Parser;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Network
{
  public abstract class Request<TRequest, TResponse> : 
    ApiObject,
    IRequest<TRequest, TResponse>,
    IRequest
    where TRequest : IRequest<TRequest, TResponse>
    where TResponse : IResponse<TResponse>
  {
    private readonly string ___request_id;
    protected IWebTask<TRequest, TResponse> ___task;

    public Request()
    {
      this.___request_id = Guid.NewGuid().ToString("N");
      this.CustomHeaders = new CustomHeaders(this.___request_id);
    }

    public CustomHeaders CustomHeaders { get; private set; }

    public byte[] UnencryptedPayload { get; set; }

    public bool IsUseEncryption
    {
      get
      {
        bool isUseEncryption = this.GetUrl() == null || SDK.Configuration.Env == null || string.IsNullOrEmpty(SDK.Configuration.Env.ServerUrl) ? GameUtility.Config_UseEncryption.Value : GameUtility.Config_UseEncryption.Value && this.GetUrl().StartsWith(SDK.Configuration.Env.ServerUrl);
        if (SRPG.Network.MenteCheckFlag)
          isUseEncryption = false;
        if (this.CustomHeaders != null && this.CustomHeaders.IsUseEncryption != isUseEncryption)
          this.CustomHeaders.IsUseEncryption = isUseEncryption;
        return isUseEncryption;
      }
    }

    public bool isDone => this.___task != null && this.___task.isDone;

    public string GetRequestID() => this.___request_id;

    public virtual string GetHost() => SDK.Configuration.Env.ServerUrl;

    public virtual string GetUrl() => this.GetHost() + this.GetPath();

    public abstract string GetPath();

    public abstract string GetMethod();

    protected virtual Dictionary<string, object> GetParameters()
    {
      return (Dictionary<string, object>) null;
    }

    protected virtual bool IsParameterUseParam() => true;

    public virtual byte[] GetPayload()
    {
      Dictionary<string, object> parameters = this.GetParameters();
      if (parameters != null)
      {
        this.UnencryptedPayload = System.Text.Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize((object) parameters));
        return !this.GetPath().Equals("/chkver2") && this.IsUseEncryption ? EncryptionHelper.Encrypt(!this.GetPath().StartsWith("/charge") ? EncryptionHelper.KeyType.APP : EncryptionHelper.KeyType.DLC, this.UnencryptedPayload, this.GetPath()) : this.UnencryptedPayload;
      }
      this.UnencryptedPayload = System.Text.Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize(Gsc.DOM.FastJSON.Json.Deserialize((IValue) (Gsc.DOM.Generic.Value) (Gsc.DOM.Generic.Object) this)));
      return !this.GetPath().Equals("/chkver2") && this.IsUseEncryption ? EncryptionHelper.Encrypt(!this.GetPath().StartsWith("/charge") ? EncryptionHelper.KeyType.APP : EncryptionHelper.KeyType.DLC, this.UnencryptedPayload, this.GetPath()) : this.UnencryptedPayload;
    }

    public virtual System.Type GetErrorResponseType() => typeof (ErrorResponse);

    public virtual WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      return result;
    }

    IWebTask IRequest.Cast() => (IWebTask) this.Cast();

    IWebTask IRequest.Send() => (IWebTask) this.Send();

    public void Retry()
    {
      if (this.___task == null)
        return;
      this.___task.Retry();
    }

    public WebTask<TRequest, TResponse> Cast()
    {
      return this.ToWebTask(WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
    }

    public WebTask<TRequest, TResponse> Send()
    {
      return this.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Parallel);
    }

    public WebTask<TRequest, TResponse> SerialSend() => this.ToWebTask(WebTaskAttribute.Reliable);

    public WebTask<TRequest, TResponse> ToWebTask(WebTaskAttribute attributes)
    {
      WebTask<TRequest, TResponse> webTask = WebTask<TRequest, TResponse>.Send((IRequest<TRequest, TResponse>) this, attributes);
      this.___task = (IWebTask<TRequest, TResponse>) webTask;
      return webTask;
    }

    public TResponse GetResponse()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.Response;
    }

    public IErrorResponse GetError()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.ErrorResponse;
    }

    public WebTaskResult GetResult()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.Result;
    }
  }
}
