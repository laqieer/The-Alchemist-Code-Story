// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleQuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class SimpleQuestDropParam
  {
    public string item_iname;
    public string[] questlist;

    public bool Deserialize(JSON_SimpleQuestDropParam json)
    {
      this.item_iname = json.iname;
      this.questlist = json.questlist;
      return true;
    }
  }
}
