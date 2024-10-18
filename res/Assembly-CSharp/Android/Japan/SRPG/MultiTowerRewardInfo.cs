// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerRewardInfo : MonoBehaviour
  {
    public GameObject unitObj;
    public GameObject itemObj;
    public GameObject amountObj;
    public GameObject artifactObj;
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
    public GameObject amountOther;
    public Text amountCount;
    public bool amountDisp;
    public Text Artifactamount;

    private void Start()
    {
    }

    public void Refresh()
    {
      this.SetData();
    }

    private void SetData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(this.gameObject, (MultiTowerRewardItem) null);
      if (dataOfClass == null)
      {
        if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
          this.itemObj.SetActive(false);
        if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          this.amountObj.SetActive(false);
        if ((UnityEngine.Object) this.unitObj != (UnityEngine.Object) null)
          this.unitObj.SetActive(false);
        if (!((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null))
          return;
        this.rewardName.gameObject.SetActive(false);
      }
      else
      {
        MultiTowerRewardItem.RewardType type = dataOfClass.type;
        string itemname = dataOfClass.itemname;
        int num = dataOfClass.num;
        if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
          this.itemObj.SetActive(true);
        if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
          this.amountObj.SetActive(true);
        if ((UnityEngine.Object) this.unitObj != (UnityEngine.Object) null)
          this.unitObj.SetActive(false);
        if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
          this.rewardName.gameObject.SetActive(true);
        switch (type)
        {
          case MultiTowerRewardItem.RewardType.Item:
            if ((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null)
              this.artifactObj.SetActive(false);
            if (!((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null) || !((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null))
              break;
            this.itemObj.SetActive(true);
            DataSource component1 = this.itemObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
              component1.Clear();
            DataSource component2 = this.amountObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
              component2.Clear();
            ItemParam itemParam = instance.GetItemParam(itemname);
            DataSource.Bind<ItemParam>(this.itemObj, itemParam, false);
            ItemData data1 = new ItemData();
            data1.Setup(0L, itemParam, num);
            DataSource.Bind<ItemData>(this.amountObj, data1, false);
            Transform transform1 = this.itemObj.transform.Find("icon");
            if ((UnityEngine.Object) transform1 != (UnityEngine.Object) null)
            {
              GameParameter component3 = transform1.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                component3.enabled = true;
            }
            GameParameter.UpdateAll(this.itemObj);
            if ((UnityEngine.Object) this.iconParam != (UnityEngine.Object) null)
              this.iconParam.UpdateValue();
            if ((UnityEngine.Object) this.frameParam != (UnityEngine.Object) null)
              this.frameParam.UpdateValue();
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
              this.rewardName.text = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
            if (!((UnityEngine.Object) this.amountOther != (UnityEngine.Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Coin:
            if ((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null)
              this.artifactObj.SetActive(false);
            if ((UnityEngine.Object) this.itemTex != (UnityEngine.Object) null)
            {
              GameParameter component3 = this.itemTex.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                component3.enabled = false;
              this.itemTex.texture = this.coinTex;
            }
            if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.coinBase != (UnityEngine.Object) null)
              this.frameTex.sprite = this.coinBase;
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
              this.rewardName.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
            if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
              this.amountObj.SetActive(false);
            if (!((UnityEngine.Object) this.amountOther != (UnityEngine.Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!((UnityEngine.Object) this.amountCount != (UnityEngine.Object) null))
                break;
              this.amountCount.text = dataOfClass.num.ToString();
              break;
            }
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Artifact:
            if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
              this.itemObj.SetActive(false);
            if (!((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null))
              break;
            this.artifactObj.SetActive(true);
            DataSource component4 = this.artifactObj.GetComponent<DataSource>();
            if ((UnityEngine.Object) component4 != (UnityEngine.Object) null)
              component4.Clear();
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
            DataSource.Bind<ArtifactParam>(this.artifactObj, artifactParam, false);
            ArtifactIcon componentInChildren = this.artifactObj.GetComponentInChildren<ArtifactIcon>();
            if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
              break;
            componentInChildren.enabled = true;
            componentInChildren.UpdateValue();
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
              this.amountObj.SetActive(false);
            if (!((UnityEngine.Object) this.Artifactamount != (UnityEngine.Object) null))
              break;
            this.Artifactamount.text = dataOfClass.num.ToString();
            break;
          case MultiTowerRewardItem.RewardType.Award:
            if ((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null)
              this.artifactObj.SetActive(false);
            if (!((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null) || !((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null))
              break;
            this.itemObj.SetActive(true);
            AwardParam awardParam = instance.GetAwardParam(itemname);
            Transform transform2 = this.itemObj.transform.Find("icon");
            if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(transform2.gameObject);
              if (!string.IsNullOrEmpty(awardParam.icon))
                iconLoader.ResourcePath = AssetPath.ItemIcon(awardParam.icon);
              GameParameter component3 = transform2.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                component3.enabled = false;
              if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
                this.rewardName.text = awardParam.name;
            }
            if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.coinBase != (UnityEngine.Object) null)
              this.frameTex.sprite = this.coinBase;
            if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
              this.amountObj.SetActive(false);
            if (!((UnityEngine.Object) this.amountOther != (UnityEngine.Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Unit:
            if (!((UnityEngine.Object) this.unitObj != (UnityEngine.Object) null))
              break;
            if ((UnityEngine.Object) this.itemObj != (UnityEngine.Object) null)
              this.itemObj.SetActive(false);
            if ((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null)
              this.artifactObj.SetActive(false);
            this.unitObj.SetActive(true);
            UnitParam unitParam = instance.GetUnitParam(itemname);
            DebugUtility.Assert(unitParam != null, "Invalid unit:" + itemname);
            UnitData data2 = new UnitData();
            data2.Setup(itemname, 0, 1, 0, (string) null, 1, EElement.None, 0);
            DataSource.Bind<UnitData>(this.unitObj, data2, false);
            GameParameter.UpdateAll(this.unitObj);
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
            if (!((UnityEngine.Object) this.amountOther != (UnityEngine.Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Gold:
            if ((UnityEngine.Object) this.artifactObj != (UnityEngine.Object) null)
              this.artifactObj.SetActive(false);
            if ((UnityEngine.Object) this.itemTex != (UnityEngine.Object) null)
            {
              GameParameter component3 = this.itemTex.GetComponent<GameParameter>();
              if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
                component3.enabled = false;
              this.itemTex.texture = this.goldTex;
            }
            if ((UnityEngine.Object) this.frameTex != (UnityEngine.Object) null && (UnityEngine.Object) this.goldBase != (UnityEngine.Object) null)
              this.frameTex.sprite = this.goldBase;
            if ((UnityEngine.Object) this.rewardName != (UnityEngine.Object) null)
              this.rewardName.text = num.ToString() + LocalizedText.Get("sys.GOLD");
            if ((UnityEngine.Object) this.amountObj != (UnityEngine.Object) null)
              this.amountObj.SetActive(false);
            if (!((UnityEngine.Object) this.amountOther != (UnityEngine.Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!((UnityEngine.Object) this.amountCount != (UnityEngine.Object) null))
                break;
              this.amountCount.text = dataOfClass.num.ToString();
              break;
            }
            this.amountOther.SetActive(false);
            break;
        }
      }
    }

    public void OnDetailClick()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(this.gameObject, (MultiTowerRewardItem) null);
      if (dataOfClass == null)
        return;
      MultiTowerRewardItem.RewardType type = dataOfClass.type;
      string itemname = dataOfClass.itemname;
      int num = 0;
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (type)
      {
        case MultiTowerRewardItem.RewardType.Item:
          ItemParam itemParam = instance.GetItemParam(itemname);
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
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
          if (artifactParam != null)
          {
            str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            str2 = artifactParam.Expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(itemname);
          if (awardParam != null)
          {
            str1 = awardParam.name;
            str2 = awardParam.expr;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Unit:
          str1 = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) instance.GetUnitParam(itemname).name);
          break;
        case MultiTowerRewardItem.RewardType.Gold:
          str1 = LocalizedText.Get("sys.GOLD");
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
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
  }
}
