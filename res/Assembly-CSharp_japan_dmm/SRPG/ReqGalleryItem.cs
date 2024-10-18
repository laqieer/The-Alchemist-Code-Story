// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGalleryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGalleryItem : WebAPI
  {
    public ReqGalleryItem(List<ItemParam> items, Network.ResponseCallback response)
    {
      this.name = "gallery/item";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"itype\":\"");
      stringBuilder.Append("item");
      stringBuilder.Append("\",");
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

    public ReqGalleryItem(List<ConceptCardParam> cards, Network.ResponseCallback response)
    {
      this.name = "gallery/item";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"itype\":\"");
      stringBuilder.Append("concept_card");
      stringBuilder.Append("\",");
      stringBuilder.Append("\"inames\":[");
      if (cards != null && cards.Count > 0)
      {
        for (int index = 0; index < cards.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(cards[index].iname);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqGalleryItem(List<ArtifactParam> artifacts, Network.ResponseCallback response)
    {
      this.name = "gallery/item";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"itype\":\"");
      stringBuilder.Append("artifact");
      stringBuilder.Append("\",");
      stringBuilder.Append("\"inames\":[");
      if (artifacts != null && artifacts.Count > 0)
      {
        for (int index = 0; index < artifacts.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(artifacts[index].iname);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
