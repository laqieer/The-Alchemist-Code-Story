// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ConceptCardParam
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
  public class JSON_ConceptCardParam
  {
    public string iname;
    public string name;
    public string expr;
    public int type;
    public string icon;
    public int rare;
    public int lvcap;
    public int sell;
    public int coin_item;
    public int en_cost;
    public int en_exp;
    public int en_trust;
    public string trust_reward;
    public string first_get_unit;
    public JSON_ConceptCardEquipParam[] effects;
    public int not_sale;
    public int birth_id;
    public string[] concept_card_groups;
    public string leader_skill;
    public int gallery_view;
    public int is_other;
    public string bg_image;
    public string[] unit_images;
  }
}
