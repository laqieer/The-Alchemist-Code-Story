// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEnhanceCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "イメージ拡大", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "イメージが閉じられた", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "イメージ拡大設定完了", FlowNode.PinTypes.Output, 100)]
  public class ConceptCardEnhanceCardDetail : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OPEN_IN_IMAGE = 0;
    public const int PIN_CLOSE_IMAGE = 1;
    public const int PIN_OPEN_OUT_IMAGE = 100;
    [SerializeField]
    private RawImage mIllustImage;
    [SerializeField]
    private ImageArray mIllustFrame;
    [SerializeField]
    private Text mCardNameText;
    [SerializeField]
    private Text mFlavorText;
    [SerializeField]
    private StarGauge mStarGauge;
    [SerializeField]
    private GameObject mMessage;
    [SerializeField]
    private Text mMessageText;
    [SerializeField]
    private GameObject mOverlayImageTemplate;
    private ConceptCardData mConceptCardData;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.mOverlayImageTemplate, (Object) null))
        this.mOverlayImageTemplate.gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.mMessage, (Object) null))
        return;
      this.mMessage.gameObject.SetActive(false);
    }

    private void Start()
    {
      if (FlowNode_ButtonEvent.currentValue == null || !(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      ConceptCardIcon component = currentValue.GetComponent<ConceptCardIcon>("_self");
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.mConceptCardData = component.ConceptCard;
      if (this.mConceptCardData == null)
        return;
      ConceptCardUnitImageSettings.ComposeUnitConceptCardImage(this.mConceptCardData.Param, this.mIllustImage, this.mOverlayImageTemplate, this.mMessage, this.mMessageText);
      if (Object.op_Inequality((Object) this.mIllustFrame, (Object) null))
        this.mIllustFrame.ImageIndex = Mathf.Min(Mathf.Max((int) this.mConceptCardData.Rarity, 0), this.mIllustFrame.Images.Length - 1);
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      this.SetFlavorTextText();
      if (Object.op_Inequality((Object) this.mStarGauge, (Object) null))
      {
        this.mStarGauge.Max = (int) this.mConceptCardData.Rarity + 1;
        this.mStarGauge.Value = (int) this.mConceptCardData.Rarity + 1;
      }
      foreach (Scrollbar componentsInChild in ((Component) this).GetComponentsInChildren<Scrollbar>())
        componentsInChild.value = 1f;
    }

    private void SetFlavorTextText()
    {
      this.SetText(this.mFlavorText, this.mConceptCardData.Param.GetLocalizedTextFlavor());
    }

    private void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = str;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (Object.op_Inequality((Object) ConceptCardManager.Instance, (Object) null))
          {
            ConceptCardManager.Instance.SelectedConceptCardMaterialData = this.mConceptCardData;
          }
          else
          {
            SerializeValueList serializeValueList = new SerializeValueList();
            serializeValueList.SetObject("conceptcard_data", (object) this.mConceptCardData);
            FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 1:
          if (!Object.op_Inequality((Object) ConceptCardManager.Instance, (Object) null))
            break;
          ConceptCardManager.Instance.SelectedConceptCardMaterialData = (ConceptCardData) null;
          break;
      }
    }
  }
}
