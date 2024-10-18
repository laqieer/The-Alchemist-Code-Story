// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
