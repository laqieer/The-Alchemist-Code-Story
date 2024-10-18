// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class ErrorResponse : Gsc.Network.Response<ErrorResponse>, IErrorResponse, IResponse
  {
    public ErrorResponse(WebInternalResponse response)
    {
      this.data = Document.Parse(response.Payload);
      this.ErrorCode = this.data.Root.GetValueByPointer("/code", (string) null) ?? this.data.Root.GetValueByPointer("/error_code", (string) null);
    }

    public string ErrorCode { get; private set; }

    public Document data { get; private set; }

    IDocument IErrorResponse.data => (IDocument) this.data;
  }
}
