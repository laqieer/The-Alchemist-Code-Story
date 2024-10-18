// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStampParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ChatStampParam
  {
    public int id;
    public string img_id;
    public string iname;
    public bool IsPrivate;

    public bool Deserialize(JSON_ChatStampParam json)
    {
      if (json == null || json.fields == null)
        return false;
      this.id = json.fields.id;
      this.img_id = json.fields.img_id;
      this.iname = json.fields.iname;
      this.IsPrivate = json.fields.is_private == 1;
      return true;
    }
  }
}
