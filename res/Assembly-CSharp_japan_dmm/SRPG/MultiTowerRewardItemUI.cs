﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardItemUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiTowerRewardItemUI : MonoBehaviour
  {
    public GameObject unitObj;
    public GameObject itemObj;
    public GameObject amountObj;
    public GameParameter iconParam;
    public GameParameter frameParam;
    public RawImage itemTex;
    public Image frameTex;
    public Texture coinTex;
    public Texture goldTex;
    public Sprite coinBase;
    public Sprite goldBase;
    public Text rewardName;
    public Text rewardFloor;
    public RectTransform pos;
    public Text rewardDetailName;
    public Text rewardDetailInfo;
    public GameObject currentMark;
    public GameObject clearMark;
    public GameObject current_fil;
    public GameObject cleared_fil;
    private int mIdx = -1;

    private void Start()
    {
    }

    public void Refresh(int idx = 0)
    {
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.rewardFloor, (Object) null))
        this.rewardFloor.text = GameUtility.HalfNum2FullNum(dataOfClass.floor.ToString()) + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      this.SetData(idx);
    }

    public void SetData(int idx = 0)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      int floor = (int) dataOfClass.floor;
      if (dataOfClass == null)
        return;
      int mtRound = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(dataOfClass.reward_id, mtRound);
      MultiTowerRewardItem multiTowerRewardItem = mtFloorReward[idx];
      MultiTowerRewardItem.RewardType rewardType = MultiTowerRewardItem.RewardType.Item;
      string str = string.Empty;
      int num = 0;
      if (idx >= 0 && idx < mtFloorReward.Count)
      {
        rewardType = multiTowerRewardItem.type;
        str = multiTowerRewardItem.itemname;
        num = multiTowerRewardItem.num;
      }
      if (Object.op_Inequality((Object) this.itemObj, (Object) null))
        this.itemObj.SetActive(true);
      if (Object.op_Inequality((Object) this.amountObj, (Object) null))
        this.amountObj.SetActive(true);
      if (Object.op_Inequality((Object) this.unitObj, (Object) null))
        this.unitObj.SetActive(false);
      switch (rewardType)
      {
        case MultiTowerRewardItem.RewardType.Item:
          if (Object.op_Inequality((Object) this.itemObj, (Object) null) && Object.op_Inequality((Object) this.amountObj, (Object) null))
          {
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              ((Behaviour) componentInChildren).enabled = false;
            this.itemObj.SetActive(true);
            DataSource component1 = this.itemObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component1, (Object) null))
              component1.Clear();
            DataSource component2 = this.amountObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.Clear();
            ItemParam itemParam = instance.GetItemParam(str);
            DataSource.Bind<ItemParam>(this.itemObj, itemParam);
            ItemData data = new ItemData();
            data.Setup(0L, itemParam, num);
            DataSource.Bind<ItemData>(this.amountObj, data);
            Transform transform = this.itemObj.transform.Find("icon");
            if (Object.op_Inequality((Object) transform, (Object) null))
            {
              GameParameter component3 = ((Component) transform).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).enabled = true;
            }
            GameParameter.UpdateAll(this.itemObj);
            if (Object.op_Inequality((Object) this.iconParam, (Object) null))
              this.iconParam.UpdateValue();
            if (Object.op_Inequality((Object) this.frameParam, (Object) null))
              this.frameParam.UpdateValue();
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            {
              this.rewardName.text = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
              break;
            }
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Coin:
          if (Object.op_Inequality((Object) this.itemTex, (Object) null))
          {
            GameParameter component = ((Component) this.itemTex).GetComponent<GameParameter>();
            if (Object.op_Inequality((Object) component, (Object) null))
              ((Behaviour) component).enabled = false;
            this.itemTex.texture = this.coinTex;
          }
          if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
            this.frameTex.sprite = this.coinBase;
          if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            this.rewardName.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          {
            this.amountObj.SetActive(false);
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Artifact:
          if (Object.op_Inequality((Object) this.itemObj, (Object) null))
          {
            DataSource component = this.itemObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Clear();
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(str);
            DataSource.Bind<ArtifactParam>(this.itemObj, artifactParam);
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
            {
              ((Behaviour) componentInChildren).enabled = true;
              componentInChildren.UpdateValue();
              if (Object.op_Inequality((Object) this.rewardName, (Object) null))
                this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
              if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              {
                this.amountObj.SetActive(false);
                break;
              }
              break;
            }
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Award:
          if (Object.op_Inequality((Object) this.itemObj, (Object) null) && Object.op_Inequality((Object) this.amountObj, (Object) null))
          {
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              ((Behaviour) componentInChildren).enabled = false;
            this.itemObj.SetActive(true);
            AwardParam awardParam = instance.GetAwardParam(str);
            Transform transform = this.itemObj.transform.Find("icon");
            if (Object.op_Inequality((Object) transform, (Object) null))
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) transform).gameObject);
              if (!string.IsNullOrEmpty(awardParam.icon))
                iconLoader.ResourcePath = AssetPath.ItemIcon(awardParam.icon);
              GameParameter component = ((Component) transform).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component, (Object) null))
                ((Behaviour) component).enabled = false;
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
              this.frameTex.sprite = this.coinBase;
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
            {
              this.amountObj.SetActive(false);
              break;
            }
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Unit:
          if (Object.op_Inequality((Object) this.unitObj, (Object) null))
          {
            if (Object.op_Inequality((Object) this.itemObj, (Object) null))
              this.itemObj.SetActive(false);
            this.unitObj.SetActive(true);
            UnitParam unitParam = instance.GetUnitParam(str);
            DebugUtility.Assert(unitParam != null, "Invalid unit:" + str);
            UnitData data = new UnitData();
            data.Setup(str, 0, 1, 0);
            DataSource.Bind<UnitData>(this.unitObj, data);
            GameParameter.UpdateAll(this.unitObj);
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            {
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
              break;
            }
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Gold:
          if (Object.op_Inequality((Object) this.itemTex, (Object) null))
          {
            GameParameter component = ((Component) this.itemTex).GetComponent<GameParameter>();
            if (Object.op_Inequality((Object) component, (Object) null))
              ((Behaviour) component).enabled = false;
            this.itemTex.texture = this.goldTex;
          }
          if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.goldBase, (Object) null))
            this.frameTex.sprite = this.goldBase;
          if (Object.op_Inequality((Object) this.rewardName, (Object) null))
            this.rewardName.text = num.ToString() + LocalizedText.Get("sys.GOLD");
          if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          {
            this.amountObj.SetActive(false);
            break;
          }
          break;
      }
      this.mIdx = idx;
      if (Object.op_Inequality((Object) this.currentMark, (Object) null))
        this.currentMark.SetActive((int) dataOfClass.floor == floor);
      if (Object.op_Inequality((Object) this.current_fil, (Object) null))
        this.current_fil.SetActive((int) dataOfClass.floor == floor);
      if (Object.op_Inequality((Object) this.clearMark, (Object) null))
        this.clearMark.SetActive((int) dataOfClass.floor - 1 < floor);
      if (!Object.op_Inequality((Object) this.cleared_fil, (Object) null))
        return;
      this.cleared_fil.SetActive((int) dataOfClass.floor - 1 < floor);
    }

    public void OnDetailClick()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      int mtRound = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(dataOfClass.reward_id, mtRound);
      MultiTowerRewardItem multiTowerRewardItem = mtFloorReward[this.mIdx];
      MultiTowerRewardItem.RewardType rewardType = MultiTowerRewardItem.RewardType.Item;
      string key = string.Empty;
      int num = 0;
      if (this.mIdx >= 0 && this.mIdx < mtFloorReward.Count)
      {
        rewardType = multiTowerRewardItem.type;
        key = multiTowerRewardItem.itemname;
        num = multiTowerRewardItem.num;
      }
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (rewardType)
      {
        case MultiTowerRewardItem.RewardType.Item:
          ItemParam itemParam = instance.GetItemParam(key);
          if (itemParam != null)
          {
            str1 = itemParam.name;
            str2 = itemParam.Expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Coin:
          str1 = LocalizedText.Get("sys.COIN");
          str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case MultiTowerRewardItem.RewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(key);
          if (artifactParam != null)
          {
            str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            str2 = artifactParam.Expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(key);
          if (awardParam != null)
          {
            str1 = awardParam.name;
            str2 = awardParam.expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(key);
          str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
          break;
        case MultiTowerRewardItem.RewardType.Gold:
          str1 = LocalizedText.Get("sys.GOLD");
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
      }
      if (Object.op_Inequality((Object) this.rewardDetailName, (Object) null))
        this.rewardDetailName.text = str1;
      if (Object.op_Inequality((Object) this.rewardDetailInfo, (Object) null))
        this.rewardDetailInfo.text = str2;
      if (Object.op_Inequality((Object) this.pos, (Object) null))
        ((Transform) this.pos).position = ((Component) this).gameObject.transform.position;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "OPEN_DETAIL");
    }
  }
}
