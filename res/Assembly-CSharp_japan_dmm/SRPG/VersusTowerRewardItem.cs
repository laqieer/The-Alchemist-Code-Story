﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusTowerRewardItem : MonoBehaviour
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
    private VersusTowerRewardItem.REWARD_TYPE mType;
    private int mSeasonIdx = -1;

    private void Start()
    {
    }

    public void Refresh(VersusTowerRewardItem.REWARD_TYPE type = VersusTowerRewardItem.REWARD_TYPE.Arrival, int idx = 0)
    {
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).gameObject, (VersusTowerParam) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.rewardFloor, (Object) null))
        this.rewardFloor.text = GameUtility.HalfNum2FullNum(dataOfClass.Floor.ToString()) + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      this.SetData(type, idx);
    }

    public void SetData(VersusTowerRewardItem.REWARD_TYPE type, int idx = 0)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).gameObject, (VersusTowerParam) null);
      int versusTowerFloor = instance.Player.VersusTowerFloor;
      if (dataOfClass == null)
        return;
      VERSUS_ITEM_TYPE versusItemType = VERSUS_ITEM_TYPE.item;
      string str = string.Empty;
      int num = 0;
      if (type == VersusTowerRewardItem.REWARD_TYPE.Arrival)
      {
        versusItemType = dataOfClass.ArrivalItemType;
        str = (string) dataOfClass.ArrivalIteminame;
        num = (int) dataOfClass.ArrivalItemNum;
      }
      else if (idx >= 0 && idx < dataOfClass.SeasonIteminame.Length)
      {
        versusItemType = dataOfClass.SeasonItemType[idx];
        str = (string) dataOfClass.SeasonIteminame[idx];
        num = (int) dataOfClass.SeasonItemnum[idx];
      }
      if (Object.op_Inequality((Object) this.itemObj, (Object) null))
        this.itemObj.SetActive(true);
      if (Object.op_Inequality((Object) this.amountObj, (Object) null))
        this.amountObj.SetActive(true);
      if (Object.op_Inequality((Object) this.unitObj, (Object) null))
        this.unitObj.SetActive(false);
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
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
        case VERSUS_ITEM_TYPE.gold:
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
        case VERSUS_ITEM_TYPE.coin:
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
        case VERSUS_ITEM_TYPE.unit:
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
        case VERSUS_ITEM_TYPE.artifact:
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
        case VERSUS_ITEM_TYPE.award:
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
      }
      this.mType = type;
      this.mSeasonIdx = idx;
      if (type == VersusTowerRewardItem.REWARD_TYPE.Arrival)
      {
        if (Object.op_Inequality((Object) this.currentMark, (Object) null))
          this.currentMark.SetActive((int) dataOfClass.Floor - 1 == versusTowerFloor);
        if (Object.op_Inequality((Object) this.current_fil, (Object) null))
          this.current_fil.SetActive((int) dataOfClass.Floor - 1 == versusTowerFloor);
      }
      else
      {
        if (Object.op_Inequality((Object) this.currentMark, (Object) null))
          this.currentMark.SetActive((int) dataOfClass.Floor == versusTowerFloor);
        if (Object.op_Inequality((Object) this.current_fil, (Object) null))
          this.current_fil.SetActive((int) dataOfClass.Floor == versusTowerFloor);
      }
      if (Object.op_Inequality((Object) this.clearMark, (Object) null))
        this.clearMark.SetActive((int) dataOfClass.Floor - 1 < versusTowerFloor);
      if (!Object.op_Inequality((Object) this.cleared_fil, (Object) null))
        return;
      this.cleared_fil.SetActive((int) dataOfClass.Floor - 1 < versusTowerFloor);
    }

    public void OnDetailClick()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).gameObject, (VersusTowerParam) null);
      if (dataOfClass == null)
        return;
      VERSUS_ITEM_TYPE versusItemType = VERSUS_ITEM_TYPE.item;
      string key = string.Empty;
      int num = 0;
      if (this.mType == VersusTowerRewardItem.REWARD_TYPE.Arrival)
      {
        versusItemType = dataOfClass.ArrivalItemType;
        key = (string) dataOfClass.ArrivalIteminame;
      }
      else if (this.mSeasonIdx >= 0 && this.mSeasonIdx < dataOfClass.SeasonIteminame.Length)
      {
        versusItemType = dataOfClass.SeasonItemType[this.mSeasonIdx];
        key = (string) dataOfClass.SeasonIteminame[this.mSeasonIdx];
        num = (int) dataOfClass.SeasonItemnum[this.mSeasonIdx];
      }
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
          ItemParam itemParam = instance.GetItemParam(key);
          if (itemParam != null)
          {
            str1 = itemParam.name;
            str2 = itemParam.Expr;
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.gold:
          str1 = LocalizedText.Get("sys.GOLD");
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
        case VERSUS_ITEM_TYPE.coin:
          str1 = LocalizedText.Get("sys.COIN");
          str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case VERSUS_ITEM_TYPE.unit:
          UnitParam unitParam = instance.GetUnitParam(key);
          str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
          break;
        case VERSUS_ITEM_TYPE.artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(key);
          if (artifactParam != null)
          {
            str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            str2 = artifactParam.Expr;
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.award:
          AwardParam awardParam = instance.GetAwardParam(key);
          if (awardParam != null)
          {
            str1 = awardParam.name;
            str2 = awardParam.expr;
            break;
          }
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

    public enum REWARD_TYPE
    {
      Arrival,
      Season,
    }
  }
}
