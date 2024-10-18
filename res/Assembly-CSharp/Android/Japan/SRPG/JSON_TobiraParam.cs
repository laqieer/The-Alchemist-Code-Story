// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TobiraParam
  {
    public string unit_iname;
    public int enable;
    public int category;
    public string recipe_id;
    public string skill_iname;
    public JSON_TobiraLearnAbilityParam[] learn_abils;
    public string overwrite_ls_iname;
    public int priority;
  }
}
