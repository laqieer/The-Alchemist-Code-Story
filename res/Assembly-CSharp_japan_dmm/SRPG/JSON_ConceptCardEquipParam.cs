// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ConceptCardEquipParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_ConceptCardEquipParam
  {
    public string cnds_iname;
    public string card_skill;
    public string add_card_skill_buff_awake;
    public string add_card_skill_buff_lvmax;
    public string abil_iname;
    public string abil_iname_lvmax;
    public string statusup_skill;
    public string skin;
    public int is_decrease_eff;
  }
}
