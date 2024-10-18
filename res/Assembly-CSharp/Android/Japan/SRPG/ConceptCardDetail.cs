// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "通常パラメータ表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "強化パラメータ表示", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "未受取トラストマスター達成", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "一括強化後の処理", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(102, "未受取トラストマスター達成", FlowNode.PinTypes.Output, 102)]
  public class ConceptCardDetail : MonoBehaviour, IFlowInterface
  {
    public const int PIN_REFRESH_PARAM = 10;
    public const int PIN_REFRESH_ENH_PARAM = 11;
    public const int PIN_TRUSTMASTER_START = 12;
    public const int PIN_ENHANCE_BULK_CHECK = 13;
    public const int PIN_TRUSTMASTER_END = 102;
    [SerializeField]
    private RawImage mIllustImage;
    [SerializeField]
    private ImageArray mIllustFrame;
    [SerializeField]
    private Text mCardNameText;
    [SerializeField]
    private Text mFlavorText;
    [SerializeField]
    private Toggle mFavoriteToggle;
    [SerializeField]
    private Button EnhanceButton;
    [SerializeField]
    private Button EnhanceExecButton;
    [SerializeField]
    private StarGauge mStarGauge;
    private ConceptCardDescription mConceptCardDescription;
    [SerializeField]
    private GameObject mConceptCardDescriptionPrefab;
    [SerializeField]
    private Transform mConceptCardDescriptionParent;
    [SerializeField]
    private Button EnhanceBulkButton;
    private ConceptCardData mConceptCardData;

    public ConceptCardDescription Description
    {
      get
      {
        return this.mConceptCardDescription;
      }
    }

    private void Start()
    {
    }

    public void Init()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mConceptCardDescriptionPrefab);
      gameObject.transform.SetParent(this.mConceptCardDescriptionParent, false);
      this.mConceptCardDescription = gameObject.GetComponentInChildren<ConceptCardDescription>();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.SetParam(false);
          this.CheckTrsutMaster();
          break;
        case 11:
          this.SetParam(true);
          break;
        case 12:
          this.StartCoroutine(this.TrustMasterUpdate(this.mConceptCardData));
          break;
        case 13:
          this.SetParam(false);
          break;
      }
    }

    public void CheckTrsutMaster()
    {
      if (this.mConceptCardData == null)
        return;
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      if ((int) this.mConceptCardData.Trust < cardTrustMax || this.mConceptCardData.TrustBonus >= (int) this.mConceptCardData.Trust / cardTrustMax || this.mConceptCardData.GetReward() == null)
        return;
      ConceptCardManager componentInParent = this.GetComponentInParent<ConceptCardManager>();
      if (!((UnityEngine.Object) componentInParent != (UnityEngine.Object) null))
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) componentInParent, "TRUST_MASTER");
    }

    [DebuggerHidden]
    private IEnumerator TrustMasterUpdate(ConceptCardData cardData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetail.\u003CTrustMasterUpdate\u003Ec__Iterator0() { cardData = cardData, \u0024this = this };
    }

    public void RefreshEnhanceButton()
    {
      if ((UnityEngine.Object) this.EnhanceButton == (UnityEngine.Object) null)
        return;
      bool flag = true;
      if ((int) this.mConceptCardData.Lv >= (int) this.mConceptCardData.LvCap && ((int) this.mConceptCardData.Trust >= MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this.mConceptCardData) || this.mConceptCardData.GetReward() == null))
        flag = false;
      this.EnhanceButton.interactable = flag;
    }

    public void RefreshEnhanceExecButton()
    {
      if ((UnityEngine.Object) this.EnhanceExecButton == (UnityEngine.Object) null || this.mConceptCardData == null)
        return;
      ConceptCardManager componentInParent = this.GetComponentInParent<ConceptCardManager>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      bool flag = true;
      if (0 >= componentInParent.SelectedMaterials.Count)
        flag = false;
      this.EnhanceExecButton.interactable = flag;
    }

    public void RefreshEnhanceBulkButton()
    {
      if ((UnityEngine.Object) this.EnhanceBulkButton == (UnityEngine.Object) null)
        return;
      bool flag1 = true;
      if (!MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial() && !MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial())
        flag1 = false;
      ConceptCardManager componentInParent = this.GetComponentInParent<ConceptCardManager>();
      if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null && componentInParent.SelectedConceptCardData != null)
      {
        bool flag2 = false;
        bool flag3 = false;
        if (!MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial())
          flag2 = true;
        if (componentInParent.SelectedConceptCardData.GetReward() == null || !MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial())
          flag3 = true;
        if (flag2 && flag3)
          flag1 = false;
      }
      this.EnhanceBulkButton.interactable = flag1;
    }

    public void SetParam(bool bEnhance)
    {
      ConceptCardManager componentInParent = this.GetComponentInParent<ConceptCardManager>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
        return;
      this.mConceptCardData = componentInParent.SelectedConceptCardData;
      if (this.mConceptCardData == null)
        return;
      this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, this.gameObject, bEnhance, false, (UnitData) null);
      this.Refresh();
      this.RefreshEnhanceButton();
      this.RefreshEnhanceExecButton();
      this.RefreshEnhanceBulkButton();
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      if ((UnityEngine.Object) this.mIllustImage != (UnityEngine.Object) null)
      {
        string path = AssetPath.ConceptCard(this.mConceptCardData.Param);
        if (this.mIllustImage.mainTexture.name != Path.GetFileName(path))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIllustImage, path);
      }
      if ((UnityEngine.Object) this.mIllustFrame != (UnityEngine.Object) null)
        this.mIllustFrame.ImageIndex = Mathf.Min(Mathf.Max((int) this.mConceptCardData.Rarity, 0), this.mIllustFrame.Images.Length - 1);
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      this.SetFlavorTextText();
      this.SetFavoriteToggle(this.mConceptCardData.Favorite);
      if ((UnityEngine.Object) this.mStarGauge != (UnityEngine.Object) null)
      {
        this.mStarGauge.Max = (int) this.mConceptCardData.Rarity + 1;
        this.mStarGauge.Value = (int) this.mConceptCardData.Rarity + 1;
      }
      foreach (Scrollbar componentsInChild in this.GetComponentsInChildren<Scrollbar>())
        componentsInChild.value = 1f;
    }

    public void SetFlavorTextText()
    {
      this.SetText(this.mFlavorText, this.mConceptCardData.Param.GetLocalizedTextFlavor());
    }

    public void SetText(Text text, string str)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = str;
    }

    public void SetFavoriteToggle(bool is_on)
    {
      if (!((UnityEngine.Object) this.mFavoriteToggle != (UnityEngine.Object) null))
        return;
      this.mFavoriteToggle.isOn = is_on;
    }
  }
}
