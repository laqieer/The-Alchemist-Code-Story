// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalTask`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Network
{
  public class WebInternalTask<TRequest, TResponse> : WebInternalTask
    where TRequest : IRequest<TRequest, TResponse>
    where TResponse : IResponse<TResponse>
  {
    private readonly TRequest _request;
    private TResponse _response;
    private IErrorResponse _error;

    public WebInternalTask(IRequest<TRequest, TResponse> request)
      : base(request.GetMethod(), request.GetUrl(), request.GetPayload(), request.UnencryptedPayload, request.CustomHeaders)
    {
      this._request = (TRequest) request;
    }

    public TResponse Response => this._response;

    public IErrorResponse ErrorResponse => this._error;

    public byte[] error { get; private set; }

    protected override WebTaskResult ProcessResponse(WebInternalResponse response)
    {
      WebTaskResult response1 = WebTask<TRequest, TResponse>.TryGetResponse(this._request, response, out this._response, out this._error);
      if ((response1 & WebTaskResult.kCreticalError) != WebTaskResult.None)
        this.error = response.Payload;
      return response1;
    }
  }
}
