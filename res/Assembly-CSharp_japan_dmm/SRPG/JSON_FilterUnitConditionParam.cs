// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_FilterUnitConditionParam
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
  public class JSON_FilterUnitConditionParam
  {
    public string cnds_iname;
    public string name;
    public int rarity_ini;
    public int birth;
    public int sex;
    public string[] af_tags;
    public string[] un_groups;
    public string[] job_groups;
  }
}
