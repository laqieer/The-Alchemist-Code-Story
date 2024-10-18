// Decompiled with JetBrains decompiler
// Type: Gsc.Network.ErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.DOM.Json;

#nullable disable
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
