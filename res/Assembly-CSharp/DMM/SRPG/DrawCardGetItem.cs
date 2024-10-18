// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardGetItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class DrawCardGetItem : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private GameObject mUnitObject;
    [SerializeField]
    private GameObject mItemObject;
    [SerializeField]
    private GameObject mArtifactObject;
    [SerializeField]
    private GameObject mGoldObject;
    [SerializeField]
    private Text mGoldAmount;
    [SerializeField]
    private GameObject mCoinObject;
    [SerializeField]
    private Text mCoinAmount;
    [SerializeField]
    private GameObject mConceptCardObject;
    [SerializeField]
    private GameObject mJokerObject;

    public void UpdateValue()
    {
      if (Object.op_Inequality((Object) this.mUnitObject, (Object) null))
        this.mUnitObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mItemObject, (Object) null))
        this.mItemObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mArtifactObject, (Object) null))
        this.mArtifactObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mGoldObject, (Object) null))
        this.mGoldObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mCoinObject, (Object) null))
        this.mCoinObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mConceptCardObject, (Object) null))
        this.mConceptCardObject.SetActive(false);
      if (Object.op_Inequality((Object) this.mJokerObject, (Object) null))
        this.mJokerObject.SetActive(false);
      DrawCardParam.CardData dataOfClass = DataSource.FindDataOfClass<DrawCardParam.CardData>(((Component) this).gameObject, (DrawCardParam.CardData) null);
      if (dataOfClass != null && !dataOfClass.IsMiss)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        switch (dataOfClass.ItemType)
        {
          case 0:
            if (Object.op_Equality((Object) this.mItemObject, (Object) null))
              break;
            ItemParam itemParam = instance.MasterParam.GetItemParam(dataOfClass.ItemIname);
            if (itemParam == null)
              break;
            ItemData data = new ItemData();
            data.Setup(0L, itemParam, dataOfClass.ItemNum);
            this.mItemObject.SetActive(true);
            DataSource.Bind<ItemData>(this.mItemObject, data);
            break;
          case 1:
            if (Object.op_Equality((Object) this.mGoldObject, (Object) null) || Object.op_Equality((Object) this.mGoldAmount, (Object) null))
              break;
            this.mGoldAmount.text = dataOfClass.ItemNum.ToString();
            this.mGoldObject.SetActive(true);
            break;
          case 2:
            if (Object.op_Equality((Object) this.mCoinObject, (Object) null) || Object.op_Equality((Object) this.mCoinAmount, (Object) null))
              break;
            this.mCoinAmount.text = dataOfClass.ItemNum.ToString();
            this.mCoinObject.SetActive(true);
            break;
          case 4:
            if (Object.op_Equality((Object) this.mUnitObject, (Object) null))
              break;
            DataSource.Bind<UnitParam>(this.mUnitObject, instance.MasterParam.GetUnitParam(dataOfClass.ItemIname));
            this.mUnitObject.SetActive(true);
            break;
          case 5:
            if (Object.op_Equality((Object) this.mConceptCardObject, (Object) null))
              break;
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(dataOfClass.ItemIname);
            if (cardDataForDisplay == null)
              break;
            ConceptCardIcon component = this.mConceptCardObject.GetComponent<ConceptCardIcon>();
            if (!Object.op_Inequality((Object) component, (Object) null))
              break;
            component.Setup(cardDataForDisplay);
            component.SetCardNum(dataOfClass.ItemNum);
            this.mConceptCardObject.SetActive(true);
            break;
          case 6:
            if (Object.op_Equality((Object) this.mArtifactObject, (Object) null))
              break;
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(dataOfClass.ItemIname);
            if (artifactParam == null)
              break;
            DataSource.Bind<ArtifactParam>(this.mArtifactObject, artifactParam);
            this.mArtifactObject.SetActive(true);
            break;
        }
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mJokerObject, (Object) null))
          return;
        this.mJokerObject.SetActive(true);
      }
    }
  }
}
