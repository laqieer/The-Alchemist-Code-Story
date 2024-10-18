// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitGetWindow : MonoBehaviour
  {
    public GameObject PopupUnit;
    [Space(5f)]
    public GameObject PopupAnimation;
    public string PopupRarityVar = string.Empty;
    public string PopupShardVar = string.Empty;
    [Space(5f)]
    public GameObject ShardNum;
    [Space(10f)]
    public GameObject ShardGauge;
    [Space(5f)]
    public GameObject ShardAnimation;
    [Space(5f)]
    public string EndShardState = string.Empty;
    [Space(10f)]
    public GameObject NormalGetEffect;
    public GameObject RareGetEffect;
    public GameObject SRareGetEffect;
    private UnitData mUnitData;
    private Animator mPopupAnimator;
    private GetUnitShard mShardWindow;
    private GameObject mCurrentGetEffect;
    private bool mIsEnd;
    public bool isMaxShard;
    private bool mIsShardEnd;
    private bool mIsEffectEnd;
    private bool mIsClickClose;

    public bool IsEnd => this.mIsEnd;

    public void Init(string unitId, bool isConver, int covertPieces)
    {
      string uid = unitId;
      if (string.IsNullOrEmpty(uid) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PopupUnit, (UnityEngine.Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mUnitData = instance.Player.Units.Find((Predicate<UnitData>) (u => u.UnitID == uid));
      if (this.mUnitData != null)
      {
        UnitData unitData = new UnitData();
        unitData.Setup(this.mUnitData);
        unitData.ResetJobSkinAll();
        this.mUnitData = unitData;
      }
      else
        this.mUnitData = UnitData.CreateUnitDataForDisplay(unitId);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mUnitData);
      DataSource.Bind<ItemParam>(((Component) this).gameObject, instance.MasterParam.GetItemParam(this.mUnitData.UnitParam.piece));
      GameParameter.UpdateAll(((Component) this).gameObject);
      bool flag = isConver;
      this.mIsShardEnd = !flag;
      int get_shard;
      if (covertPieces > 0)
      {
        get_shard = covertPieces;
      }
      else
      {
        get_shard = this.mUnitData.GetChangePieces();
        if (GlobalVars.SelectUnitTicketDataValue != null)
          GlobalVars.SelectUnitTicketDataValue.ConvertPieceNum = get_shard;
      }
      if (flag && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardNum, (UnityEngine.Object) null))
      {
        Text component = this.ShardNum.GetComponent<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.text = get_shard.ToString();
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardGauge, (UnityEngine.Object) null))
        return;
      this.ShardGauge.SetActive(flag);
      if (!flag)
        return;
      int awakeLv = this.mUnitData.AwakeLv;
      if (awakeLv < this.mUnitData.GetAwakeLevelCap())
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mUnitData.UnitParam.piece);
        this.mShardWindow = this.ShardGauge.GetComponent<GetUnitShard>();
        this.mShardWindow.Refresh(this.mUnitData.UnitParam, itemParam.name, awakeLv, get_shard);
      }
      else
      {
        this.ShardGauge.gameObject.SetActive(false);
        this.mIsShardEnd = true;
      }
    }

    public void PlayAnim(bool isConver)
    {
      int rare = (int) this.mUnitData.UnitParam.rare;
      bool flag = isConver;
      this.PopupUnit.SetActive(true);
      this.SpawnGetEffect(rare);
      this.mPopupAnimator = this.PopupAnimation.GetComponent<Animator>();
      this.mPopupAnimator.SetInteger(this.PopupRarityVar, rare + 1);
      this.mPopupAnimator.SetBool(this.PopupShardVar, flag);
    }

    public void OnCloseClick()
    {
      this.mIsClickClose = true;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mShardWindow, (UnityEngine.Object) null))
        return;
      this.mShardWindow.OnClicked();
    }

    private void Update()
    {
      if (this.mIsEnd)
        return;
      if (!this.mIsShardEnd && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mShardWindow, (UnityEngine.Object) null) && !this.mShardWindow.IsRunningAnimator)
      {
        this.mIsShardEnd = true;
      }
      else
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPopupAnimator, (UnityEngine.Object) null))
          return;
        if (this.mIsShardEnd && this.mIsClickClose && this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && !this.mIsShardEnd)
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
        if (this.mIsShardEnd && this.mIsClickClose && this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && this.isMaxShard && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mShardWindow, (UnityEngine.Object) null) && !this.mShardWindow.IsRunningAnimator)
        {
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
          this.isMaxShard = false;
        }
        if (this.mIsEnd || !this.mIsShardEnd || !this.mIsClickClose || GameUtility.IsAnimatorRunning((Component) this.mPopupAnimator))
          return;
        this.mIsEnd = true;
      }
    }

    private void SpawnGetEffect(int rarity)
    {
      switch (rarity)
      {
        case 0:
        case 1:
        case 2:
          this.mCurrentGetEffect = this.NormalGetEffect;
          break;
        case 3:
          this.mCurrentGetEffect = this.RareGetEffect;
          break;
        default:
          this.mCurrentGetEffect = this.SRareGetEffect;
          break;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentGetEffect, (UnityEngine.Object) null))
        return;
      this.mCurrentGetEffect.SetActive(true);
      this.mCurrentGetEffect.transform.SetParent(this.PopupUnit.transform, false);
    }
  }
}
