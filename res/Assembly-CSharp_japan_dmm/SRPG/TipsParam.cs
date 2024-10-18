// Decompiled with JetBrains decompiler
// Type: SRPG.TipsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class TipsParam
  {
    public string iname;
    public ETipsType type;
    public int order;
    public string title;
    public string text;
    public string[] images;
    public bool hide;
    public string cond_text;

    public void Deserialize(JSON_TipsParam json)
    {
      this.iname = json.iname;
      this.type = (ETipsType) Enum.ToObject(typeof (ETipsType), json.type);
      this.order = json.order;
      this.title = json.title;
      this.text = json.text;
      this.images = json.images;
      this.hide = json.hide != 0;
      this.cond_text = json.cond_text;
    }
  }
}
