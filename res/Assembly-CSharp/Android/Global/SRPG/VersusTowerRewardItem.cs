// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusTowerRewardItem : MonoBehaviour
  {
    private int mSeasonIdx = -1;
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

    private void Start()
    {
    }

    public void Refresh(VersusTowerRewardItem.REWARD_TYPE type = VersusTowerRewardItem.REWARD_TYPE.Arrival, int idx = 0)
    {
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(this.gameObject, (VersusTowerParam) null);
      if (dataOfClass == null)
        return;
      if ((UnityEngine.Object) this.rewardFloor != (UnityEngine.Object) null)
        this.rewardFloor.text = GameUtility.HalfNum2FullNum(dataOfClass.Floor.ToString()) + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      this.SetData(type, idx);
    }

    public void SetData(VersusTowerRewardItem.REWARD_TYPE type, int idx = 0)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(this.gameObject, (VersusTowerParam) null);
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
      if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
        this.itemObj.SetActive(true);
      if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
        this.amountObj.SetActive(true);
      if ((UnityEngine.Object) this.unitObj != (UnityEngine.Object) null)
        this.unitObj.SetActive(false);
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
          if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null && (UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          {
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
              componentInChildren.enabled = false;
            this.itemObj.SetActive(true);
            DataSource component1 = this.itemObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
              component1.Clear();
            DataSource component2 = this.amountObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
              component2.Clear();
            ItemParam itemParam = instance.GetItemParam(str);
            DataSource.Bind<ItemParam>(this.itemObj, itemParam);
            ItemData data = new ItemData();
            data.Setup(0L, itemParam, num);
            DataSource.Bind<ItemData>(this.amountObj, data);
            Transform child = this.itemObj.transform.FindChild("icon");
            if ((UnityEngine.Object) child != (UnityEngine.Object) null)
            {
              GameParameter component3 = child.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                component3.enabled = true;
            }
            GameParameter.UpdateAll(this.itemObj);
            if ((UnityEngine.Object) this.iconParam != (UnityEngine.Object) null)
              this.iconParam.UpdateValue();
            if ((UnityEngine.Object) this.frameParam != (UnityEngine.Object) null)
              this.frameParam.UpdateValue();
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
            {
              this.rewardName.text = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
              break;
            }
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.gold:
          if ((UnityEngine.Object) this.itemTex != (UnityEngine.Object) null)
          {
            GameParameter component = this.itemTex.GetComponent<GameParameter>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.enabled = false;
            this.itemTex.texture = this.goldTex;
          }
          if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.goldBase != (UnityEngine.Object) null)
            this.frameTex.sprite = this.goldBase;
          if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
            this.rewardName.text = num.ToString() + LocalizedText.Get("sys.GOLD");
          if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          {
            this.amountObj.SetActive(false);
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.coin:
          if ((UnityEngine.Object) this.itemTex != (UnityEngine.Object) null)
          {
            GameParameter component = this.itemTex.GetComponent<GameParameter>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.enabled = false;
            this.itemTex.texture = this.coinTex;
          }
          if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.coinBase != (UnityEngine.Object) null)
            this.frameTex.sprite = this.coinBase;
          if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
            this.rewardName.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          {
            this.amountObj.SetActive(false);
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.unit:
          if ((UnityEngine.Object) this.unitObj != (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
              this.itemObj.SetActive(false);
            this.unitObj.SetActive(true);
            UnitParam unitParam = instance.GetUnitParam(str);
            DebugUtility.Assert(unitParam != null, "Invalid unit:" + str);
            UnitData data = new UnitData();
            data.Setup(str, 0, 1, 0, (string) null, 1, EElement.None);
            DataSource.Bind<UnitData>(this.unitObj, data);
            GameParameter.UpdateAll(this.unitObj);
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
            {
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
              break;
            }
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.artifact:
          if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
          {
            DataSource component = this.itemObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.Clear();
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(str);
            DataSource.Bind<ArtifactParam>(this.itemObj, artifactParam);
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
            {
              componentInChildren.enabled = true;
              componentInChildren.UpdateValue();
              if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
                this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
              if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
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
          if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null && (UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          {
            ArtifactIcon componentInChildren = this.itemObj.GetComponentInChildren<ArtifactIcon>();
            if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
              componentInChildren.enabled = false;
            this.itemObj.SetActive(true);
            AwardParam awardParam = instance.GetAwardParam(str);
            Transform child = this.itemObj.transform.FindChild("icon");
            if ((UnityEngine.Object) child != (UnityEngine.Object) null)
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(child.gameObject);
              if (!string.IsNullOrEmpty(awardParam.icon))
                iconLoader.ResourcePath = AssetPath.ItemIcon(awardParam.icon);
              GameParameter component = child.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                component.enabled = false;
            }
            if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.coinBase != (UnityEngine.Object) null)
              this.frameTex.sprite = this.coinBase;
            if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
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
        if ((UnityEngine.Object) this.currentMark != (UnityEngine.Object) null)
          this.currentMark.SetActive((int) dataOfClass.Floor - 1 == versusTowerFloor);
        if ((UnityEngine.Object) this.current_fil != (UnityEngine.Object) null)
          this.current_fil.SetActive((int) dataOfClass.Floor - 1 == versusTowerFloor);
      }
      else
      {
        if ((UnityEngine.Object) this.currentMark != (UnityEngine.Object) null)
          this.currentMark.SetActive((int) dataOfClass.Floor == versusTowerFloor);
        if ((UnityEngine.Object) this.current_fil != (UnityEngine.Object) null)
          this.current_fil.SetActive((int) dataOfClass.Floor == versusTowerFloor);
      }
      if ((UnityEngine.Object) this.clearMark != (UnityEngine.Object) null)
        this.clearMark.SetActive((int) dataOfClass.Floor - 1 < versusTowerFloor);
      if (!((UnityEngine.Object) this.cleared_fil != (UnityEngine.Object) null))
        return;
      this.cleared_fil.SetActive((int) dataOfClass.Floor - 1 < versusTowerFloor);
    }

    public void OnDetailClick()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(this.gameObject, (VersusTowerParam) null);
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
            str2 = itemParam.expr;
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
          str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) instance.GetUnitParam(key).name);
          break;
        case VERSUS_ITEM_TYPE.artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(key);
          if (artifactParam != null)
          {
            str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            str2 = artifactParam.expr;
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
      if ((UnityEngine.Object) this.rewardDetailName != (UnityEngine.Object) null)
        this.rewardDetailName.text = str1;
      if ((UnityEngine.Object) this.rewardDetailInfo != (UnityEngine.Object) null)
        this.rewardDetailInfo.text = str2;
      if ((UnityEngine.Object) this.pos != (UnityEngine.Object) null)
        this.pos.position = this.gameObject.transform.position;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "OPEN_DETAIL");
    }

    public enum REWARD_TYPE
    {
      Arrival,
      Season,
    }
  }
}
