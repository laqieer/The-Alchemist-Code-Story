// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGalleryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqGalleryItem : WebAPI
  {
    public ReqGalleryItem(List<ItemParam> items, Network.ResponseCallback response)
    {
      this.name = "gallery/item";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"inames\":[");
      if (items != null && items.Count > 0)
      {
        for (int index = 0; index < items.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(items[index].iname);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
