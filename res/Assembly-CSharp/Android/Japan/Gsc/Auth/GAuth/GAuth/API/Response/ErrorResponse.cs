// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network;

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

    IDocument IErrorResponse.data
    {
      get
      {
        return (IDocument) this.data;
      }
    }
  }
}
