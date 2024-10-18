// Decompiled with JetBrains decompiler
// Type: Gsc.Network.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Json;

namespace Gsc.Network
{
  public class ErrorResponse : Response<ErrorResponse>, IErrorResponse, IResponse
  {
    public ErrorResponse(WebInternalResponse response)
    {
      if (response.Payload.Length <= 0)
        return;
      Document document = Document.Parse(response.Payload);
      this.data = (IDocument) document;
      this.ErrorCode = document.Root.GetValueByPointer("/error_code", (string) null);
    }

    public IDocument data { get; private set; }

    public string ErrorCode { get; private set; }
  }
}
