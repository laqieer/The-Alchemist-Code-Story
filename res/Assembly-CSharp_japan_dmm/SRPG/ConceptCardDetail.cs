// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    private GameObject GroupIconParent;
    [SerializeField]
    private StarGauge mStarGauge;
    private ConceptCardDescription mConceptCardDescription;
    [SerializeField]
    private GameObject mConceptCardDescriptionPrefab;
    [SerializeField]
    private Transform mConceptCardDescriptionParent;
    private ConceptCardData mConceptCardData;

    public ConceptCardDescription Description => this.mConceptCardDescription;

    private void Start()
    {
    }

    public void Init()
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.mConceptCardDescriptionPrefab);
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
      ConceptCardManager componentInParent = ((Component) this).GetComponentInParent<ConceptCardManager>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) componentInParent, "TRUST_MASTER");
    }

    [DebuggerHidden]
    private IEnumerator TrustMasterUpdate(ConceptCardData cardData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetail.\u003CTrustMasterUpdate\u003Ec__Iterator0()
      {
        cardData = cardData,
        \u0024this = this
      };
    }

    public void RefreshEnhanceButton()
    {
      if (Object.op_Equality((Object) this.EnhanceButton, (Object) null))
        return;
      bool flag = true;
      if ((int) this.mConceptCardData.Lv >= (int) this.mConceptCardData.LvCap && ((int) this.mConceptCardData.Trust >= MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this.mConceptCardData) || this.mConceptCardData.GetReward() == null))
        flag = false;
      ((Selectable) this.EnhanceButton).interactable = flag;
    }

    public void RefreshEnhanceExecButton()
    {
      if (Object.op_Equality((Object) this.EnhanceExecButton, (Object) null) || this.mConceptCardData == null)
        return;
      ConceptCardManager componentInParent = ((Component) this).GetComponentInParent<ConceptCardManager>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      bool flag = true;
      if (0 >= componentInParent.SelectedMaterials.Count)
        flag = false;
      ((Selectable) this.EnhanceExecButton).interactable = flag;
    }

    public void SetParam(bool bEnhance)
    {
      ConceptCardManager componentInParent = ((Component) this).GetComponentInParent<ConceptCardManager>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      this.mConceptCardData = componentInParent.SelectedConceptCardData;
      if (this.mConceptCardData == null)
        return;
      this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, ((Component) this).gameObject, bEnhance);
      if (Object.op_Inequality((Object) this.GroupIconParent, (Object) null))
      {
        DataSource.Bind<ConceptCardData>(this.GroupIconParent, this.mConceptCardData);
        GameParameter.UpdateAll(this.GroupIconParent);
        ContentSizeFitter component = this.GroupIconParent.GetComponent<ContentSizeFitter>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          Canvas.ForceUpdateCanvases();
          ((Behaviour) component).enabled = !((Behaviour) component).enabled;
          ((Behaviour) component).enabled = !((Behaviour) component).enabled;
        }
      }
      this.Refresh();
      this.RefreshEnhanceButton();
      this.RefreshEnhanceExecButton();
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      if (Object.op_Inequality((Object) this.mIllustImage, (Object) null))
      {
        string path = AssetPath.ConceptCard(this.mConceptCardData.Param);
        if (((Object) ((Graphic) this.mIllustImage).mainTexture).name != Path.GetFileName(path))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIllustImage, path);
      }
      if (Object.op_Inequality((Object) this.mIllustFrame, (Object) null))
        this.mIllustFrame.ImageIndex = Mathf.Min(Mathf.Max((int) this.mConceptCardData.Rarity, 0), this.mIllustFrame.Images.Length - 1);
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      this.SetFlavorTextText();
      this.SetFavoriteToggle(this.mConceptCardData.Favorite);
      if (Object.op_Inequality((Object) this.mStarGauge, (Object) null))
      {
        this.mStarGauge.Max = (int) this.mConceptCardData.Rarity + 1;
        this.mStarGauge.Value = (int) this.mConceptCardData.Rarity + 1;
      }
      foreach (Scrollbar componentsInChild in ((Component) this).GetComponentsInChildren<Scrollbar>())
        componentsInChild.value = 1f;
    }

    public void SetFlavorTextText()
    {
      this.SetText(this.mFlavorText, this.mConceptCardData.Param.GetLocalizedTextFlavor());
    }

    public void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = str;
    }

    public void SetFavoriteToggle(bool is_on)
    {
      if (!Object.op_Inequality((Object) this.mFavoriteToggle, (Object) null))
        return;
      this.mFavoriteToggle.isOn = is_on;
    }
  }
}
