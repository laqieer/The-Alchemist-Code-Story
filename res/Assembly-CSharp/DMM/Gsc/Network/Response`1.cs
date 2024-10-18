// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Response`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.DOM.Json;
using Gsc.Network.Data;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Network
{
  public abstract class Response<T> : IResponse<T>, IResponse where T : IResponse<T>
  {
    protected virtual Dictionary<string, object> GetResult(byte[] payload)
    {
      if (payload == null || payload.Length <= 0)
        return (Dictionary<string, object>) null;
      Document document = Document.Parse(payload);
      document.SetRoot((Value) this.GetResponseRoot((IDocument) document));
      return (Dictionary<string, object>) Gsc.DOM.MiniJSON.Json.Deserialize((IValue) document.Root);
    }

    protected virtual IValue GetResponseRoot(IDocument document)
    {
      Gsc.DOM.IObject root = document.Root.GetObject();
      IValue responseRoot;
      if (!root.TryGetValue("response", out responseRoot))
        return document.Root;
      EntityRepository.Update(root);
      return responseRoot;
    }
  }
}
