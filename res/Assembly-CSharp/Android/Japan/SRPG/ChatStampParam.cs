// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStampParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
