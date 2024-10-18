// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSellCheckList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "表示設定", FlowNode.PinTypes.Input, 1)]
  public class ConceptCardSellCheckList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_SETUP = 1;
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private ScrollRect mScrollRect;
    [SerializeField]
    private Text TextSellZeny;
    [SerializeField]
    private Text TextSellCoin;
    private MultiConceptCard mSelectedMaterials = new MultiConceptCard();
    private List<ConceptCardData> mSelectedCardDatas = new List<ConceptCardData>();
    private List<GameObject> ListObj = new List<GameObject>();
    private ConceptCardManager mCCManager;

    private ConceptCardManager CCManager
    {
      get
      {
        if (Object.op_Equality((Object) this.mCCManager, (Object) null))
        {
          this.mCCManager = ((Component) this).GetComponentInParent<ConceptCardManager>();
          if (Object.op_Equality((Object) this.mCCManager, (Object) null))
            DebugUtility.LogError("Not found ConceptCardManager.");
        }
        return this.mCCManager;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.RefreshList();
    }

    public void Init()
    {
      if (Object.op_Equality((Object) this.mCardObjectTemplate, (Object) null))
        return;
      this.mCardObjectTemplate.SetActive(false);
    }

    private void RefreshList()
    {
      if (Object.op_Equality((Object) this.mCardObjectTemplate, (Object) null) || Object.op_Equality((Object) this.CCManager, (Object) null))
        return;
      this.mSelectedMaterials = this.CCManager.SelectedMaterials;
      this.mSelectedCardDatas = this.mSelectedMaterials.GetList();
      foreach (Object @object in this.ListObj)
        Object.Destroy(@object);
      this.ListObj.Clear();
      this.mScrollRect.verticalNormalizedPosition = 1f;
      ConceptCardListSortWindow.Sort(this.CCManager.SortType, this.CCManager.SortOrderType, this.mSelectedCardDatas, true);
      List<MultiConceptCard> multiConceptCardList = new List<MultiConceptCard>();
      int num = 0;
      int index1 = 0;
      for (int index2 = 0; index2 < this.mSelectedCardDatas.Count; ++index2)
      {
        if ((int) this.mSelectedCardDatas[index2].Rarity != num)
        {
          if (num != 0)
            ++index1;
          num = (int) this.mSelectedCardDatas[index2].Rarity;
          multiConceptCardList.Add(new MultiConceptCard());
        }
        multiConceptCardList[index1].Add(this.mSelectedCardDatas[index2]);
      }
      foreach (MultiConceptCard card in multiConceptCardList)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.mCardObjectTemplate.gameObject);
        gameObject.transform.SetParent((Transform) this.mCardObjectParent, false);
        this.ListObj.Add(gameObject);
        gameObject.SetActive(true);
        ConceptCardSellRarityList component = gameObject.GetComponent<ConceptCardSellRarityList>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Init(card);
      }
      this.RefreshTextSellZeny();
      this.RefreshTextSellCoin();
    }

    private void RefreshTextSellZeny()
    {
      if (Object.op_Equality((Object) this.TextSellZeny, (Object) null))
        return;
      int totalSellZeny = 0;
      ConceptCardManager.GalcTotalSellZeny(this.mSelectedMaterials, out totalSellZeny);
      this.TextSellZeny.text = totalSellZeny.ToString();
    }

    private void RefreshTextSellCoin()
    {
      if (Object.op_Equality((Object) this.TextSellCoin, (Object) null))
        return;
      int totalSellCoin = 0;
      ConceptCardManager.CalcTotalSellCoin(this.mSelectedMaterials, out totalSellCoin);
      this.TextSellCoin.text = totalSellCoin.ToString();
    }
  }
}
