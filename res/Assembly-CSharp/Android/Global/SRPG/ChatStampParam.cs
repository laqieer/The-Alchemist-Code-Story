// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStampParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
