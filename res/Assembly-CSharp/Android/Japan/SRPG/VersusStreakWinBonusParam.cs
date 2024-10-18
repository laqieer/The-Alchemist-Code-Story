﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStreakWinBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class VersusStreakWinBonusParam
  {
    public int id;
    public int wincnt;
    public VersusWinBonusRewardParam[] rewards;

    public bool Deserialize(JSON_VersusStreakWinBonus json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      this.wincnt = json.wincnt;
      if (json.rewards != null)
      {
        int length = json.rewards.Length;
        this.rewards = new VersusWinBonusRewardParam[length];
        if (this.rewards != null)
        {
          for (int index = 0; index < length; ++index)
          {
            this.rewards[index] = new VersusWinBonusRewardParam();
            this.rewards[index].type = (VERSUS_REWARD_TYPE) Enum.ToObject(typeof (VERSUS_REWARD_TYPE), json.rewards[index].item_type);
            this.rewards[index].iname = json.rewards[index].item_iname;
            this.rewards[index].num = json.rewards[index].item_num;
          }
        }
      }
      return true;
    }
  }
}
