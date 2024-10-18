// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopAdventQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuerrillaShopAdventQuestParam
  {
    public int id;
    public string qid;

    public bool Deserialize(JSON_GuerrillaShopAdventQuestParam json)
    {
      this.id = json.id;
      this.qid = json.qid;
      return true;
    }
  }
}
