// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleQuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
