// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionRewardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChallengeMissionRewardItem : MonoBehaviour
  {
    public GameObject ImageItem;
    public GameObject ImageExp;
    public GameObject ImageGold;
    public GameObject ImageStamina;
    public Text TextReward;
    public ConceptCardIcon ConceptCardObject;
    public GameObject ImageUnit;

    private void ResetAll()
    {
      if (Object.op_Inequality((Object) this.ImageItem, (Object) null))
        this.ImageItem.SetActive(false);
      if (Object.op_Inequality((Object) this.ImageExp, (Object) null))
        this.ImageExp.SetActive(false);
      if (Object.op_Inequality((Object) this.ImageGold, (Object) null))
        this.ImageGold.SetActive(false);
      if (Object.op_Inequality((Object) this.ImageStamina, (Object) null))
        this.ImageStamina.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardObject, (Object) null))
        ((Component) this.ConceptCardObject).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.ImageUnit, (Object) null))
        return;
      this.ImageUnit.SetActive(false);
    }

    public void SetGold(int num)
    {
      this.ResetAll();
      string formatedText = CurrencyBitmapText.CreateFormatedText(num.ToString());
      this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) formatedText);
      if (!Object.op_Inequality((Object) this.ImageGold, (Object) null))
        return;
      this.ImageGold.SetActive(true);
    }

    public void SetEXP(int exp)
    {
      this.ResetAll();
      this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) exp);
      if (!Object.op_Inequality((Object) this.ImageExp, (Object) null))
        return;
      this.ImageExp.SetActive(true);
    }

    public void SetStamina(int stamina)
    {
      this.ResetAll();
      this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) stamina);
      if (!Object.op_Inequality((Object) this.ImageStamina, (Object) null))
        return;
      this.ImageStamina.SetActive(true);
    }

    public void SetCoin(int coin)
    {
      this.ResetAll();
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
      if (itemParam == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) coin);
      if (!Object.op_Inequality((Object) this.ImageItem, (Object) null))
        return;
      this.ImageItem.SetActive(true);
    }

    public void SetItem(string iname, int num)
    {
      this.ResetAll();
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(iname);
      if (itemParam == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      if (itemParam.type == EItemType.Unit)
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
        if (unitParam != null)
          this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT"), (object) ((int) unitParam.rare + 1), (object) unitParam.name);
        if (!Object.op_Inequality((Object) this.ImageUnit, (Object) null))
          return;
        this.ImageUnit.SetActive(true);
      }
      else
      {
        this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD"), (object) itemParam.name, (object) num);
        if (!Object.op_Inequality((Object) this.ImageItem, (Object) null))
          return;
        this.ImageItem.SetActive(true);
      }
    }

    public void SetConceptCard(string iname, int num)
    {
      this.ResetAll();
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(iname);
      if (conceptCardParam == null)
        return;
      this.TextReward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) num);
      if (!Object.op_Inequality((Object) this.ConceptCardObject, (Object) null))
        return;
      this.ConceptCardObject.Setup(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname));
      ((Component) this.ConceptCardObject).gameObject.SetActive(true);
    }
  }
}
