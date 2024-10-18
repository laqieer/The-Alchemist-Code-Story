// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraLearnAbilityParam
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
  public class JSON_TobiraLearnAbilityParam
  {
    public string abil_iname;
    public int learn_lv;
    public int add_type;
    public string target_job;
    public string abil_overwrite;
  }
}
