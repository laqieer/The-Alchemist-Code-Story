﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Json_RaidRankingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Json_RaidRankingData
  {
    public string uid;
    public string name;
    public int lv;
    public int rank;
    public int score;
    public Json_Unit unit;
    public string selected_award;
    public JSON_ViewGuild guild;
  }
}
