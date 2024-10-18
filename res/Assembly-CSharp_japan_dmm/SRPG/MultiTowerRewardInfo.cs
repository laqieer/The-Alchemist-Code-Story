// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

    public void Refresh() => this.SetData();

    private void SetData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(((Component) this).gameObject, (MultiTowerRewardItem) null);
      if (dataOfClass == null)
      {
        if (Object.op_Inequality((Object) this.itemObj, (Object) null))
          this.itemObj.SetActive(false);
        if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          this.amountObj.SetActive(false);
        if (Object.op_Inequality((Object) this.unitObj, (Object) null))
          this.unitObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.rewardName, (Object) null))
          return;
        ((Component) this.rewardName).gameObject.SetActive(false);
      }
      else
      {
        MultiTowerRewardItem.RewardType type = dataOfClass.type;
        string itemname = dataOfClass.itemname;
        int num = dataOfClass.num;
        if (Object.op_Inequality((Object) this.itemObj, (Object) null))
          this.itemObj.SetActive(true);
        if (Object.op_Inequality((Object) this.amountObj, (Object) null))
          this.amountObj.SetActive(true);
        if (Object.op_Inequality((Object) this.unitObj, (Object) null))
          this.unitObj.SetActive(false);
        if (Object.op_Inequality((Object) this.rewardName, (Object) null))
          ((Component) this.rewardName).gameObject.SetActive(true);
        switch (type)
        {
          case MultiTowerRewardItem.RewardType.Item:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.itemObj, (Object) null) || !Object.op_Inequality((Object) this.amountObj, (Object) null))
              break;
            this.itemObj.SetActive(true);
            DataSource component1 = this.itemObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component1, (Object) null))
              component1.Clear();
            DataSource component2 = this.amountObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.Clear();
            ItemParam itemParam = instance.GetItemParam(itemname);
            DataSource.Bind<ItemParam>(this.itemObj, itemParam);
            ItemData data1 = new ItemData();
            data1.Setup(0L, itemParam, num);
            DataSource.Bind<ItemData>(this.amountObj, data1);
            Transform transform1 = this.itemObj.transform.Find("icon");
            if (Object.op_Inequality((Object) transform1, (Object) null))
            {
              GameParameter component3 = ((Component) transform1).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component3, (Object) null))
                ((Behaviour) component3).enabled = true;
            }
            GameParameter.UpdateAll(this.itemObj);
            if (Object.op_Inequality((Object) this.iconParam, (Object) null))
              this.iconParam.UpdateValue();
            if (Object.op_Inequality((Object) this.frameParam, (Object) null))
              this.frameParam.UpdateValue();
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.text = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Coin:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (Object.op_Inequality((Object) this.itemTex, (Object) null))
            {
              GameParameter component4 = ((Component) this.itemTex).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component4, (Object) null))
                ((Behaviour) component4).enabled = false;
              this.itemTex.texture = this.coinTex;
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
              this.frameTex.sprite = this.coinBase;
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!Object.op_Inequality((Object) this.amountCount, (Object) null))
                break;
              this.amountCount.text = dataOfClass.num.ToString();
              break;
            }
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Artifact:
            if (Object.op_Inequality((Object) this.itemObj, (Object) null))
              this.itemObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.artifactObj, (Object) null))
              break;
            this.artifactObj.SetActive(true);
            DataSource component5 = this.artifactObj.GetComponent<DataSource>();
            if (Object.op_Inequality((Object) component5, (Object) null))
              component5.Clear();
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
            DataSource.Bind<ArtifactParam>(this.artifactObj, artifactParam);
            ArtifactIcon componentInChildren = this.artifactObj.GetComponentInChildren<ArtifactIcon>();
            if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
              break;
            ((Behaviour) componentInChildren).enabled = true;
            componentInChildren.UpdateValue();
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_ARTIFACT"), (object) artifactParam.name);
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.Artifactamount, (Object) null))
              break;
            this.Artifactamount.text = dataOfClass.num.ToString();
            break;
          case MultiTowerRewardItem.RewardType.Award:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.itemObj, (Object) null) || !Object.op_Inequality((Object) this.amountObj, (Object) null))
              break;
            this.itemObj.SetActive(true);
            AwardParam awardParam = instance.GetAwardParam(itemname);
            Transform transform2 = this.itemObj.transform.Find("icon");
            if (Object.op_Inequality((Object) transform2, (Object) null))
            {
              IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(((Component) transform2).gameObject);
              if (!string.IsNullOrEmpty(awardParam.icon))
                iconLoader.ResourcePath = AssetPath.ItemIcon(awardParam.icon);
              GameParameter component6 = ((Component) transform2).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component6, (Object) null))
                ((Behaviour) component6).enabled = false;
              if (Object.op_Inequality((Object) this.rewardName, (Object) null))
                this.rewardName.text = awardParam.name;
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.coinBase, (Object) null))
              this.frameTex.sprite = this.coinBase;
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Unit:
            if (!Object.op_Inequality((Object) this.unitObj, (Object) null))
              break;
            if (Object.op_Inequality((Object) this.itemObj, (Object) null))
              this.itemObj.SetActive(false);
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            this.unitObj.SetActive(true);
            UnitParam unitParam = instance.GetUnitParam(itemname);
            DebugUtility.Assert(unitParam != null, "Invalid unit:" + itemname);
            UnitData data2 = new UnitData();
            data2.Setup(itemname, 0, 1, 0);
            DataSource.Bind<UnitData>(this.unitObj, data2);
            GameParameter.UpdateAll(this.unitObj);
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_UNIT"), (object) unitParam.name);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            this.amountOther.SetActive(false);
            break;
          case MultiTowerRewardItem.RewardType.Gold:
            if (Object.op_Inequality((Object) this.artifactObj, (Object) null))
              this.artifactObj.SetActive(false);
            if (Object.op_Inequality((Object) this.itemTex, (Object) null))
            {
              GameParameter component7 = ((Component) this.itemTex).GetComponent<GameParameter>();
              if (Object.op_Inequality((Object) component7, (Object) null))
                ((Behaviour) component7).enabled = false;
              this.itemTex.texture = this.goldTex;
            }
            if (Object.op_Inequality((Object) this.frameTex, (Object) null) && Object.op_Inequality((Object) this.goldBase, (Object) null))
              this.frameTex.sprite = this.goldBase;
            if (Object.op_Inequality((Object) this.rewardName, (Object) null))
              this.rewardName.text = CurrencyBitmapText.CreateFormatedText(num.ToString()) + LocalizedText.Get("sys.GOLD");
            if (Object.op_Inequality((Object) this.amountObj, (Object) null))
              this.amountObj.SetActive(false);
            if (!Object.op_Inequality((Object) this.amountOther, (Object) null))
              break;
            if (this.amountDisp)
            {
              this.amountOther.SetActive(true);
              if (!Object.op_Inequality((Object) this.amountCount, (Object) null))
                break;
              this.amountCount.text = CurrencyBitmapText.CreateFormatedText(dataOfClass.num.ToString());
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
      MultiTowerRewardItem dataOfClass = DataSource.FindDataOfClass<MultiTowerRewardItem>(((Component) this).gameObject, (MultiTowerRewardItem) null);
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
          UnitParam unitParam = instance.GetUnitParam(itemname);
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
