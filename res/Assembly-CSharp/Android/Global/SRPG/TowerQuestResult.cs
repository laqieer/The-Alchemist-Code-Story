// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(32, "クリア演出終了", FlowNode.PinTypes.Output, 32)]
  [FlowNode.Pin(11, "クリア演出開始", FlowNode.PinTypes.Input, 11)]
  public class TowerQuestResult : QuestResult
  {
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    public float TowerItem_TriggerInterval = 1f;
    protected List<GameObject> mTowerListItems = new List<GameObject>();
    private List<HpBarSlider> mHpBar = new List<HpBarSlider>();
    public float DeadAlpha = 0.5f;
    private List<TowerUnitIsDead> canvas_group = new List<TowerUnitIsDead>();
    [Description("塔報酬画面用入手アイテムのゲームオブジェクト")]
    public GameObject TowerTreasureListItem;
    [Description("塔リザルト画面入手アイテムのゲームオブジェクト")]
    public GameObject TowerItemObject;
    private bool mContinueTowerItemAnimation;
    private bool mContinueTowerItem;
    public TowerClear TowerClear;
    public GameObject Window;

    public override void Activated(int pinID)
    {
      base.Activated(pinID);
      if (pinID != 11)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestName).tower_id);
      if (towerResuponse.clear == 0 || towerResuponse.clear == 1 && towerResuponse.arrived_num == 0 || !tower.is_view_ranking)
      {
        this.EndTowerClear();
      }
      else
      {
        this.Window.SetActive(false);
        this.TowerClear.Refresh();
        this.TowerClear.gameObject.SetActive(true);
      }
    }

    public void EndTowerClear()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 32);
    }

    private void SetTowerResult(Transform parent, GameObject ItemObject, List<ItemData> data = null)
    {
      if (this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      List<TowerRewardItem> towerRewardItemList = new List<TowerRewardItem>((IEnumerable<TowerRewardItem>) MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID).reward_id).GetTowerRewardItem());
      if (data != null)
      {
        for (int index = 0; index < data.Count; ++index)
        {
          ItemData itemData = data[index];
          if (itemData != null)
            towerRewardItemList.Add(new TowerRewardItem()
            {
              iname = itemData.Param.iname,
              type = TowerRewardItem.RewardType.Item,
              num = data[index].Num,
              visible = true,
              is_new = data[index].IsNew
            });
        }
      }
      for (int index = 0; index < towerRewardItemList.Count; ++index)
      {
        TowerRewardItem item = towerRewardItemList[index];
        if (item.visible && item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ItemObject);
          gameObject.transform.SetParent(parent, false);
          this.mTowerListItems.Add(gameObject);
          gameObject.transform.localScale = ItemObject.transform.localScale;
          DataSource.Bind<TowerRewardItem>(gameObject, towerRewardItemList[index]);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = gameObject.GetComponentInChildren<TowerRewardUI>();
          if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
            componentInChildren1.Refresh();
          if (item.type == TowerRewardItem.RewardType.Artifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = gameObject.GetComponentInChildren<ArtifactIcon>();
            if (!((UnityEngine.Object) componentInChildren2 == (UnityEngine.Object) null))
            {
              componentInChildren2.enabled = true;
              componentInChildren2.UpdateValue();
              if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == item.iname)) == null)
              {
                item.is_new = true;
                break;
              }
              break;
            }
            break;
          }
          if ((UnityEngine.Object) this.Prefab_NewItemBadge != (UnityEngine.Object) null && item.is_new)
          {
            RectTransform transform = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).transform as RectTransform;
            transform.gameObject.SetActive(true);
            transform.anchoredPosition = Vector2.zero;
            transform.SetParent(gameObject.transform, false);
          }
        }
      }
      ItemObject.SetActive(false);
    }

    [DebuggerHidden]
    private IEnumerator RecvHealAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CRecvHealAnimation\u003Ec__Iterator124() { \u003C\u003Ef__this = this };
    }

    private bool IsHealEnd(List<TowerQuestResult.HealParanm> param)
    {
      for (int index = 0; index < param.Count; ++index)
      {
        if (param[index].hp_heal > param[index].hpGained)
          return false;
      }
      return true;
    }

    public override void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
    {
      this.SetTowerResult(this.TowerTreasureListItem.transform.parent, this.TowerTreasureListItem, (List<ItemData>) null);
    }

    public override void AddExpPlayer()
    {
      Transform parent = !((UnityEngine.Object) this.UnitList != (UnityEngine.Object) null) ? this.UnitListItem.transform.parent : this.UnitList.transform;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
        gameObject.transform.SetParent(parent, false);
        this.canvas_group.Add(gameObject.GetComponentInChildren<TowerUnitIsDead>());
        this.mUnitListItems.Add(gameObject);
        if (this.mCurrentQuest.type == QuestTypes.Tower)
          this.mHpBar.Add(gameObject.GetComponentInChildren<HpBarSlider>());
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
      if (this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      for (int i = 0; i < this.mHpBar.Count; ++i)
      {
        Unit unit = SceneBattle.Instance.Battle.Player.Find((Predicate<Unit>) (x => x.UnitData.UniqueID == this.mUnits[i].UniqueID));
        if (unit != null && MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null)
        {
          this.mHpBar[i].slider.maxValue = (float) unit.TowerStartHP;
          this.mHpBar[i].slider.minValue = 0.0f;
          this.mHpBar[i].slider.value = (float) (int) unit.CurrentStatus.param.hp;
          TowerResuponse.PlayerUnit playerUnit = MonoSingleton<GameManager>.Instance.TowerResuponse.FindPlayerUnit(this.mUnits[i]);
          if (playerUnit != null && playerUnit.isDied)
            this.mUnitListItems[i].transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = this.DeadAlpha;
          GameParameter.UpdateAll(this.mUnitListItems[i]);
        }
      }
    }

    [DebuggerHidden]
    public override IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CAddExp\u003Ec__Iterator125() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public override IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator126() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public IEnumerator StartTowerTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CStartTowerTreasureAnimation\u003Ec__Iterator127() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected IEnumerator TowerTreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CTowerTreasureAnimation\u003Ec__Iterator128() { ListItems = ListItems, \u003C\u0024\u003EListItems = ListItems, \u003C\u003Ef__this = this };
    }

    public void OnTowerItemAnimationEnd()
    {
      this.mContinueTowerItemAnimation = true;
    }

    public void OnTowerItemEnd()
    {
      this.mContinueTowerItem = true;
    }

    private class HealParanm
    {
      public Unit unit;
      public int hp;
      public int hp_heal;
      public float hp_gainRate;
      public int hpGained;
      public float hpAccumulator;
      public HpBarSlider hp_bar;

      public HealParanm(Unit unit, TowerFloorParam FloorParam, HpBarSlider hp_bar, float GainRate, float GainTimeMax)
      {
        this.unit = unit;
        this.hp = unit.TowerStartHP;
        this.hp_heal = FloorParam.CalcHelaNum(this.hp);
        this.hp_gainRate = GainRate;
        if ((double) ((float) this.hp_heal / GainRate) > (double) GainTimeMax)
          this.hp_gainRate = (float) this.hp_heal / GainTimeMax;
        this.hp_bar = hp_bar;
      }

      public void Update()
      {
        this.hpAccumulator += this.hp_gainRate * Time.deltaTime;
        if ((double) this.hpAccumulator < 1.0)
          return;
        int num = Mathf.FloorToInt(this.hpAccumulator);
        this.hpAccumulator %= 1f;
        this.hpGained += num;
        if (this.hp_heal < this.hpGained)
        {
          num = Math.Max(num - (this.hpGained - this.hp_heal), 0);
          this.hpGained = this.hp_heal;
        }
        this.unit.Heal(num);
        this.hp_bar.slider.value = (float) (int) this.unit.CurrentStatus.param.hp;
        this.hp_bar.color.ChangeValue((int) this.unit.CurrentStatus.param.hp, this.unit.TowerStartHP);
      }
    }
  }
}
