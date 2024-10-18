// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "ボス選択", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ボス設定完了", FlowNode.PinTypes.Output, 2)]
  public class WorldRaidBossIcon : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_SELECT = 1;
    private const int PIN_OUTPUT_SELECT = 2;
    [SerializeField]
    private GameObject BossChallenge;
    [SerializeField]
    private GameObject BossClear;
    [SerializeField]
    private PolyImage ChallengeBossIcon;
    [SerializeField]
    private PolyImage ClearBossIcon;
    [SerializeField]
    private BitmapText HpText;
    [SerializeField]
    private Image LifeImage;
    [SerializeField]
    private GameObject BossLifeBgZero;
    [Description("割合の低い方からリストに追加をお願いします")]
    [SerializeField]
    private List<GameObject> BossLifeBgList = new List<GameObject>();
    private WorldRaidBossChallengedData mBossData;
    private int mBossIndex;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.SelectBoss();
    }

    public void SetUp(WorldRaidBossChallengedData _data)
    {
      this.mBossData = _data;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return;
      List<WorldRaidParam.BossInfo> bossInfoList = currentWorldRaidParam.BossInfoList;
      WorldRaidParam.BossInfo bossInfo = bossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == this.mBossData.BossIname));
      this.mBossIndex = bossInfoList.FindIndex((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == this.mBossData.BossIname));
      if (bossInfo == null || this.mBossIndex < 0)
        return;
      if (this.mBossData.CurrentHP > 0L)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChallengeBossIcon, (UnityEngine.Object) null))
        {
          Sprite worldRaidBossIcon = WorldRaidBossManager.GetWorldRaidBossIcon(bossInfo.BossId);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) worldRaidBossIcon, (UnityEngine.Object) null))
            this.ChallengeBossIcon.sprite = worldRaidBossIcon;
        }
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearBossIcon, (UnityEngine.Object) null))
      {
        Sprite worldRaidBossIcon = WorldRaidBossManager.GetWorldRaidBossIcon(bossInfo.BossId);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) worldRaidBossIcon, (UnityEngine.Object) null))
          this.ClearBossIcon.sprite = worldRaidBossIcon;
      }
      GameUtility.SetGameObjectActive(this.BossChallenge, false);
      GameUtility.SetGameObjectActive(this.BossClear, false);
      if (this.mBossData.CurrentHP > 0L)
        GameUtility.SetGameObjectActive(this.BossChallenge, true);
      else
        GameUtility.SetGameObjectActive(this.BossClear, true);
      double currentHp = (double) this.mBossData.CurrentHP;
      double num1 = (double) this.mBossData.CalcMaxHP();
      if (num1 <= 0.0)
        num1 = 1.0;
      int num2 = (int) (currentHp / num1 * 100.0);
      if (num2 == 100 && currentHp < num1)
        num2 = 99;
      if (num2 == 0 && currentHp > 0.0)
        num2 = 1;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LifeImage, (UnityEngine.Object) null))
        this.LifeImage.fillAmount = (float) num2 / 100f;
      if (currentHp <= 0.0)
        GameUtility.SetGameObjectActive(this.BossLifeBgZero, true);
      else
        GameUtility.SetGameObjectActive(this.BossLifeBgZero, false);
      if (this.BossLifeBgList.Count > 0)
      {
        int num3 = 100 / this.BossLifeBgList.Count;
        int num4 = 0;
        bool flag = false;
        foreach (GameObject bossLifeBg in this.BossLifeBgList)
        {
          num4 += num3;
          if (!flag && currentHp != 0.0 && num2 <= num4)
          {
            flag = true;
            GameUtility.SetGameObjectActive(bossLifeBg, true);
          }
          else
            GameUtility.SetGameObjectActive(bossLifeBg, false);
        }
        if (!flag && currentHp != 0.0)
          GameUtility.SetGameObjectActive(this.BossLifeBgList[this.BossLifeBgList.Count - 1], true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HpText, (UnityEngine.Object) null))
        return;
      ((Text) this.HpText).text = num2.ToString() + "%";
    }

    private void SelectBoss()
    {
      WorldRaidBossManager.SetBossIndex(this.mBossIndex);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
