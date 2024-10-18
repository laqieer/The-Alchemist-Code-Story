// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResultTreasureNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class QuestResultTreasureNode : ContentNode
  {
    [SerializeField]
    private GameObject TreasureListItem;
    [SerializeField]
    private GameObject TreasureListUnit;
    [SerializeField]
    private GameObject TreasureListArtifact;
    [SerializeField]
    private GameObject TreasureListConceptCard;
    [SerializeField]
    private GameObject NewIcon;
    private QuestResultTreasureParam mParam;
    private GameObject mDispNewIcon;

    public void Setup(QuestResultTreasureParam _param)
    {
      GameUtility.SetGameObjectActive(this.TreasureListItem, false);
      GameUtility.SetGameObjectActive(this.TreasureListUnit, false);
      GameUtility.SetGameObjectActive(this.TreasureListArtifact, false);
      GameUtility.SetGameObjectActive(this.TreasureListConceptCard, false);
      this.mParam = _param;
      GameObject root = (GameObject) null;
      if (this.mParam.ItemData != null)
      {
        if (this.mParam.ItemData.IsConceptCard && Object.op_Inequality((Object) this.TreasureListConceptCard, (Object) null))
        {
          root = this.TreasureListConceptCard;
          root.SetActive(true);
          DataSource.Bind<QuestResult.DropItemData>(root, this.mParam.ItemData);
          if (this.mParam.ItemData.mIsSecret)
          {
            ItemIcon component = (ItemIcon) root.GetComponent<DropItemIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.IsSecret = true;
          }
          GameParameter.UpdateAll(root);
        }
        else if (this.mParam.ItemData.IsItem && Object.op_Inequality((Object) this.TreasureListItem, (Object) null) && Object.op_Inequality((Object) this.TreasureListUnit, (Object) null))
        {
          root = this.mParam.ItemData.ItemType != EItemType.Unit ? this.TreasureListItem : this.TreasureListUnit;
          root.SetActive(true);
          DataSource.Bind<ItemData>(root, (ItemData) this.mParam.ItemData);
          DataSource.Bind<RuneData>(root, this.mParam.ItemData.runeData);
          if (this.mParam.ItemData.mIsSecret)
          {
            ItemIcon component = root.GetComponent<ItemIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.IsSecret = true;
          }
          GameParameter.UpdateAll(root);
        }
        else
          DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (object) this.mParam.ItemData.BattleRewardType));
      }
      else if (this.mParam.ArtfactParam != null && Object.op_Inequality((Object) this.TreasureListArtifact, (Object) null))
      {
        root = this.TreasureListArtifact;
        root.SetActive(true);
        DataSource.Bind<ArtifactParam>(root, this.mParam.ArtfactParam);
        DataSource.Bind<int>(root, this.mParam.ArtfactNum);
        GameParameter.UpdateAll(root);
      }
      else if (this.mParam.GoldNum > 0 && Object.op_Inequality((Object) this.TreasureListItem, (Object) null))
      {
        root = this.TreasureListItem;
        root.SetActive(true);
        Transform transform1 = root.transform.Find("BODY/frame");
        if (Object.op_Inequality((Object) transform1, (Object) null))
        {
          Image_Transparent component = ((Component) transform1).GetComponent<Image_Transparent>();
          if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) this.mParam.GoldFrame, (Object) null))
            component.sprite = this.mParam.GoldFrame;
        }
        Transform transform2 = root.transform.Find("BODY/itemicon");
        if (Object.op_Inequality((Object) transform2, (Object) null))
        {
          RawImage_Transparent component = ((Component) transform2).GetComponent<RawImage_Transparent>();
          if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) this.mParam.GoldTex, (Object) null))
            component.texture = (Texture) this.mParam.GoldTex;
        }
        Transform transform3 = root.transform.Find("BODY/amount/Text_amount");
        if (Object.op_Inequality((Object) transform3, (Object) null))
        {
          BitmapText component = ((Component) transform3).GetComponent<BitmapText>();
          if (Object.op_Inequality((Object) component, (Object) null))
            ((Text) component).text = CurrencyBitmapText.CreateFormatedText(this.mParam.GoldNum.ToString());
        }
      }
      if (Object.op_Inequality((Object) this.mDispNewIcon, (Object) null))
      {
        Object.Destroy((Object) this.mDispNewIcon);
        this.mDispNewIcon = (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.NewIcon, (Object) null) && this.mParam.ItemData != null && this.mParam.ItemData.IsNew && Object.op_Inequality((Object) root, (Object) null))
      {
        this.mDispNewIcon = Object.Instantiate<GameObject>(this.NewIcon);
        RectTransform transform = this.mDispNewIcon.transform as RectTransform;
        ((Component) transform).gameObject.SetActive(true);
        transform.anchoredPosition = Vector2.zero;
        ((Transform) transform).SetParent(root.transform, false);
      }
      this.mParam.mSelectObj = root;
      if (!this.mParam.IsEndAnim)
        return;
      Object.Destroy((Object) root.GetComponent<Animator>());
    }
  }
}
