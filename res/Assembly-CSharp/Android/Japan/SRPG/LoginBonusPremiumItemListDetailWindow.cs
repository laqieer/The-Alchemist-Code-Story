// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusPremiumItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LoginBonusPremiumItemListDetailWindow : MonoBehaviour
  {
    [SerializeField]
    private string IconNamePath = "txt_name";
    [SerializeField]
    private Transform LimitedItemList;
    [SerializeField]
    private GameObject ItemBase;
    private Json_PremiumLoginBonus mPremiumLoginBonus;
    private int mLoginBonusIndex;

    public void Refresh()
    {
      Json_PremiumLoginBonus[] premiumBonuses = MonoSingleton<GameManager>.Instance.Player.PremiumLoginBonus.premium_bonuses;
      this.mLoginBonusIndex = int.Parse(GlobalVars.SelectedItemID);
      this.mPremiumLoginBonus = premiumBonuses[this.mLoginBonusIndex];
      this.MakeTopItemIcon();
      this.MakeItemIconInPacking();
      this.enabled = true;
    }

    private void MakeTopItemIcon()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string str = this.mPremiumLoginBonus.icon;
      bool flag = false;
      if (str == null)
      {
        if (this.mPremiumLoginBonus.item != null && this.mPremiumLoginBonus.item.Length > 0)
          str = this.mPremiumLoginBonus.item[0].iname;
        else if (this.mPremiumLoginBonus.coin > 0)
          str = "$COIN";
        else if (this.mPremiumLoginBonus.gold > 0)
          flag = true;
      }
      SetItemObject component = this.GetComponent<SetItemObject>();
      if (str != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(str, false);
        if (itemParam != null)
        {
          DataSource.Bind<ItemParam>(this.gameObject, itemParam, false);
          component.SetIconActive(GiftTypes.Item);
        }
        ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
        if (conceptCardParam != null)
        {
          DataSource.Bind<ConceptCardParam>(this.gameObject, conceptCardParam, false);
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(str);
          component.SetupConceptCard(cardDataForDisplay);
          component.SetIconActive(GiftTypes.ConceptCard);
        }
        if (instance.MasterParam.GetArtifactParam(str, false) != null)
        {
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(str);
          if (artifactParam != null)
          {
            DataSource.Bind<ArtifactParam>(this.gameObject, artifactParam, false);
            component.SetIconActive(GiftTypes.Artifact);
            component.ArtifactIcon.transform.Find(this.IconNamePath).GetComponent<Text>().text = artifactParam.name;
          }
        }
        if (instance.MasterParam.ContainsUnitID(str))
        {
          UnitParam unitParam = instance.MasterParam.GetUnitParam(str);
          if (unitParam != null)
          {
            DataSource.Bind<UnitParam>(this.gameObject, unitParam, false);
            component.SetIconActive(GiftTypes.Unit);
            component.UnitIcon.transform.Find(this.IconNamePath).GetComponent<Text>().text = unitParam.name;
          }
        }
        if (instance.MasterParam.ContainsAwardID(str))
        {
          AwardParam awardParam = instance.MasterParam.GetAwardParam(str);
          if (awardParam != null)
          {
            DataSource.Bind<AwardParam>(this.gameObject, awardParam, false);
            component.SetIconActive(GiftTypes.Award);
            component.AwardIcon.transform.Find(this.IconNamePath).GetComponent<Text>().text = awardParam.name;
          }
        }
      }
      else if (flag)
      {
        component.SetIconActive(GiftTypes.Gold);
        component.GoldIcon.transform.Find(this.IconNamePath).GetComponent<Text>().text = this.mPremiumLoginBonus.gold.ToString() + LocalizedText.Get("sys.GOLD");
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void MakeItemIconInPacking()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.ItemBase.SetActive(false);
      if (this.mPremiumLoginBonus.item != null)
      {
        for (int index = 0; index < this.mPremiumLoginBonus.item.Length; ++index)
        {
          GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.ItemBase);
          root.transform.SetParent(this.LimitedItemList, false);
          root.SetActive(true);
          bool flag = false;
          SetItemObject component = root.GetComponent<SetItemObject>();
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mPremiumLoginBonus.item[index].iname, false);
          if (itemParam != null)
          {
            DataSource.Bind<ItemParam>(root, itemParam, false);
            component.SetItemDesc(GiftTypes.Item, itemParam.name, this.mPremiumLoginBonus.item[index].num);
            flag = true;
          }
          ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(this.mPremiumLoginBonus.item[index].iname);
          if (conceptCardParam != null)
          {
            DataSource.Bind<ConceptCardParam>(root, conceptCardParam, false);
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(this.mPremiumLoginBonus.item[index].iname);
            component.SetupConceptCard(cardDataForDisplay);
            component.SetItemDesc(GiftTypes.ConceptCard, conceptCardParam.name, this.mPremiumLoginBonus.item[index].num);
            flag = true;
          }
          if (!flag && instance.MasterParam.GetArtifactParam(this.mPremiumLoginBonus.item[index].iname, false) != null)
          {
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(this.mPremiumLoginBonus.item[index].iname);
            if (artifactParam != null)
            {
              DataSource.Bind<ArtifactParam>(root, artifactParam, false);
              component.SetItemDesc(GiftTypes.Artifact, artifactParam.name, this.mPremiumLoginBonus.item[index].num);
              flag = true;
            }
          }
          if (!flag && instance.MasterParam.ContainsUnitID(this.mPremiumLoginBonus.item[index].iname))
          {
            UnitParam unitParam = instance.MasterParam.GetUnitParam(this.mPremiumLoginBonus.item[index].iname);
            if (unitParam != null)
            {
              DataSource.Bind<UnitParam>(root, unitParam, false);
              component.SetItemDesc(GiftTypes.Unit, unitParam.name, this.mPremiumLoginBonus.item[index].num);
              flag = true;
            }
          }
          if (!flag && instance.MasterParam.ContainsAwardID(this.mPremiumLoginBonus.item[index].iname))
          {
            AwardParam awardParam = instance.MasterParam.GetAwardParam(this.mPremiumLoginBonus.item[index].iname);
            if (awardParam != null)
            {
              DataSource.Bind<AwardParam>(root, awardParam, false);
              component.SetItemDesc(GiftTypes.Award, awardParam.name, this.mPremiumLoginBonus.item[index].num);
            }
          }
          GameParameter.UpdateAll(root);
        }
      }
      if (this.mPremiumLoginBonus.coin > 0)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam("$COIN", false);
        if (itemParam != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemBase);
          gameObject.transform.SetParent(this.LimitedItemList, false);
          gameObject.SetActive(true);
          SetItemObject component = gameObject.GetComponent<SetItemObject>();
          DataSource.Bind<ItemParam>(gameObject, itemParam, false);
          component.SetItemDesc(GiftTypes.Coin, string.Empty, this.mPremiumLoginBonus.coin);
        }
      }
      if (this.mPremiumLoginBonus.gold <= 0)
        return;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ItemBase);
      gameObject1.transform.SetParent(this.LimitedItemList, false);
      gameObject1.SetActive(true);
      gameObject1.GetComponent<SetItemObject>().SetItemDesc(GiftTypes.Gold, string.Empty, this.mPremiumLoginBonus.gold);
    }
  }
}
