// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerLevelUpList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class PlayerLevelUpList : MonoBehaviour
  {
    [Description("プレイヤーレベルの行")]
    public GameObject Level;
    [Description("現在の出撃ポイントの行")]
    public GameObject StaminaCurrent;
    [Description("最大出撃ポイントの行")]
    public GameObject StaminaMax;
    [Description("最大フレンド枠の行")]
    public GameObject FriendSlotMax;
    [Description("アンロック情報")]
    public GameObject[] UnlockInfo;

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerLevelUpInfo data = new PlayerLevelUpInfo();
      PlayerData player = instance.Player;
      data.LevelNext = PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpNew);
      data.LevelPrev = PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpOld);
      PlayerParam playerParam1 = instance.MasterParam.GetPlayerParam(data.LevelPrev);
      PlayerParam playerParam2 = instance.MasterParam.GetPlayerParam(data.LevelNext);
      data.StaminaNext = player.Stamina;
      data.StaminaMaxPrev = (int) playerParam1.pt;
      data.StaminaMaxNext = (int) playerParam2.pt;
      data.MaxFriendNumPrev = (int) playerParam1.fcap;
      data.MaxFriendNumNext = (int) playerParam2.fcap;
      List<UnlockParam> unlockParamList = new List<UnlockParam>();
      for (int index1 = data.LevelPrev + 1; index1 <= data.LevelNext; ++index1)
      {
        for (int index2 = 0; index2 < instance.MasterParam.Unlocks.Length; ++index2)
        {
          UnlockParam unlock = instance.MasterParam.Unlocks[index2];
          if (unlock.UnlockTarget != UnlockTargets.Tower && unlock.VipRank <= player.VipRank && unlock.PlayerLevel == index1 && !unlockParamList.Contains(unlock))
            unlockParamList.Add(unlock);
        }
      }
      if (unlockParamList != null)
      {
        data.UnlockInfo = new string[unlockParamList.Count];
        for (int index = 0; index < unlockParamList.Count; ++index)
          data.UnlockInfo[index] = LocalizedText.Get("sys.UNLOCK_" + unlockParamList[index].iname.ToUpper());
      }
      else
        data.UnlockInfo = new string[0];
      DataSource.Bind<PlayerLevelUpInfo>(((Component) this).gameObject, data);
      if (Object.op_Inequality((Object) this.StaminaMax, (Object) null))
        this.StaminaMax.SetActive(data.StaminaMaxPrev != data.StaminaMaxNext);
      if (Object.op_Inequality((Object) this.FriendSlotMax, (Object) null))
        this.FriendSlotMax.SetActive(data.MaxFriendNumPrev != data.MaxFriendNumNext);
      for (int index = 0; index < this.UnlockInfo.Length; ++index)
        this.UnlockInfo[index].SetActive(index < data.UnlockInfo.Length);
    }
  }
}
