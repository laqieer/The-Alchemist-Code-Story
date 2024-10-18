// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitGetWindow : MonoBehaviour
  {
    public string PopupRarityVar = string.Empty;
    public string PopupShardVar = string.Empty;
    [Space(5f)]
    public string EndShardState = string.Empty;
    public GameObject PopupUnit;
    [Space(5f)]
    public GameObject PopupAnimation;
    [Space(5f)]
    public GameObject ShardNum;
    [Space(10f)]
    public GameObject ShardGauge;
    [Space(5f)]
    public GameObject ShardAnimation;
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

    public bool IsEnd
    {
      get
      {
        return this.mIsEnd;
      }
    }

    public void Init(string unitId, bool isConver)
    {
      if (string.IsNullOrEmpty(unitId) || (UnityEngine.Object) this.PopupUnit == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mUnitData = instance.Player.Units.Find((Predicate<UnitData>) (u => u.UnitID == unitId));
      if (this.mUnitData == null)
      {
        Json_Unit json = new Json_Unit();
        json.iid = -1L;
        json.iname = unitId;
        this.mUnitData = new UnitData();
        this.mUnitData.Deserialize(json);
      }
      DataSource.Bind<UnitData>(this.gameObject, this.mUnitData);
      DataSource.Bind<ItemParam>(this.gameObject, instance.MasterParam.GetItemParam((string) this.mUnitData.UnitParam.piece));
      GameParameter.UpdateAll(this.gameObject);
      bool flag = isConver;
      this.mIsShardEnd = !flag;
      if (flag && (UnityEngine.Object) this.ShardNum != (UnityEngine.Object) null)
      {
        Text component = this.ShardNum.GetComponent<Text>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.text = this.mUnitData.GetChangePieces().ToString();
      }
      if (!((UnityEngine.Object) this.ShardGauge != (UnityEngine.Object) null))
        return;
      this.ShardGauge.SetActive(flag);
      if (!flag)
        return;
      int awakeLv = this.mUnitData.AwakeLv;
      if (awakeLv < this.mUnitData.GetAwakeLevelCap())
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) this.mUnitData.UnitParam.piece);
        this.mShardWindow = this.ShardGauge.GetComponent<GetUnitShard>();
        this.mShardWindow.Refresh(this.mUnitData.UnitParam, itemParam.name, awakeLv, this.mUnitData.GetChangePieces(), 0);
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
      if (!((UnityEngine.Object) this.mShardWindow != (UnityEngine.Object) null))
        return;
      this.mShardWindow.OnClicked();
    }

    private void Update()
    {
      if (this.mIsEnd)
        return;
      if (!this.mIsShardEnd && (UnityEngine.Object) this.mShardWindow != (UnityEngine.Object) null && !this.mShardWindow.IsRunningAnimator)
      {
        this.mIsShardEnd = true;
      }
      else
      {
        if (this.mIsShardEnd && this.mIsClickClose && (this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && !this.mIsShardEnd))
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
        if (this.mIsShardEnd && this.mIsClickClose && (this.mPopupAnimator.GetInteger(this.PopupRarityVar) > 0 && this.isMaxShard) && ((UnityEngine.Object) this.mShardWindow != (UnityEngine.Object) null && !this.mShardWindow.IsRunningAnimator))
        {
          this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
          this.isMaxShard = false;
        }
        if (this.mIsEnd || !this.mIsShardEnd || (!this.mIsClickClose || GameUtility.IsAnimatorRunning((Component) this.mPopupAnimator)))
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
      if ((UnityEngine.Object) this.mCurrentGetEffect == (UnityEngine.Object) null)
        return;
      this.mCurrentGetEffect.SetActive(true);
      this.mCurrentGetEffect.transform.SetParent(this.PopupUnit.transform, false);
    }
  }
}
