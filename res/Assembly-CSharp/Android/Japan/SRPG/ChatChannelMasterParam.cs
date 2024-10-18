// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ChatChannelMasterParam
  {
    public int id;
    public byte category_id;
    public string name;

    public void Deserialize(Json_ChatChannelMasterParam json)
    {
      if (json == null)
        throw new InvalidCastException();
      this.id = json.fields.id;
      this.category_id = json.fields.category_id;
      this.name = json.fields.name;
    }
  }
}
