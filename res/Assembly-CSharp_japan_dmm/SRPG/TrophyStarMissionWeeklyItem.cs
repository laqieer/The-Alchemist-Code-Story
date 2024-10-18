// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyStarMissionWeeklyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TrophyStarMissionWeeklyItem : MonoBehaviour
  {
    [SerializeField]
    private Text StarCount;
    [Space(5f)]
    [SerializeField]
    private GameObject GoUnachieved;
    [SerializeField]
    private SRPG_Button ButtonUnachieved;
    [Space(5f)]
    [SerializeField]
    private GameObject GoCanReceive;
    [SerializeField]
    private SRPG_Button ButtonCanReceive;
    [Space(5f)]
    [SerializeField]
    private GameObject GoReceived;
    [SerializeField]
    private SRPG_Button ButtonReceived;
    private int mIndex;

    public int Index => this.mIndex;

    public void SetItem(int index, UnityAction action)
    {
      if (index < 0)
        return;
      this.mIndex = index;
      if (action != null)
      {
        if (Object.op_Implicit((Object) this.ButtonUnachieved))
          ((UnityEvent) this.ButtonUnachieved.onClick).AddListener(action);
        if (Object.op_Implicit((Object) this.ButtonCanReceive))
          ((UnityEvent) this.ButtonCanReceive.onClick).AddListener(action);
        if (Object.op_Implicit((Object) this.ButtonReceived))
          ((UnityEvent) this.ButtonReceived.onClick).AddListener(action);
      }
      this.Refresh();
    }

    public void Refresh()
    {
      int num1 = 0;
      int num2 = 0;
      bool flag = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Implicit((Object) instance) && instance.Player != null && instance.Player.TrophyStarMissionInfo != null && instance.Player.TrophyStarMissionInfo.Weekly != null && instance.Player.TrophyStarMissionInfo.Weekly.TsmParam != null)
      {
        List<TrophyStarMissionParam.StarSetParam> starSetList = instance.Player.TrophyStarMissionInfo.Weekly.TsmParam.StarSetList;
        if (this.mIndex < starSetList.Count)
          num2 = (int) starSetList[this.mIndex].RequireStar;
        if (Object.op_Implicit((Object) this.StarCount))
          this.StarCount.text = num2.ToString();
        PlayerData.TrophyStarMission.StarMission weekly = instance.Player.TrophyStarMissionInfo.Weekly;
        num1 = weekly.StarNum;
        flag = weekly.Rewards != null && this.mIndex < weekly.Rewards.Length && weekly.Rewards[this.mIndex] != 0;
      }
      if (Object.op_Implicit((Object) this.GoUnachieved))
        this.GoUnachieved.SetActive(false);
      if (Object.op_Implicit((Object) this.GoCanReceive))
        this.GoCanReceive.SetActive(false);
      if (Object.op_Implicit((Object) this.GoReceived))
        this.GoReceived.SetActive(false);
      if (flag)
      {
        if (!Object.op_Implicit((Object) this.GoReceived))
          return;
        this.GoReceived.SetActive(true);
      }
      else if (num1 >= num2)
      {
        if (!Object.op_Implicit((Object) this.GoCanReceive))
          return;
        this.GoCanReceive.SetActive(true);
      }
      else
      {
        if (!Object.op_Implicit((Object) this.GoUnachieved))
          return;
        this.GoUnachieved.SetActive(true);
      }
    }
  }
}
