﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEnhanceCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
    private ConceptCardData mConceptCardData;

    private void Start()
    {
      if (FlowNode_ButtonEvent.currentValue == null)
        return;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      ConceptCardIcon component = currentValue.GetComponent<ConceptCardIcon>("_self");
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      this.mConceptCardData = component.ConceptCard;
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
      if ((UnityEngine.Object) this.mStarGauge != (UnityEngine.Object) null)
      {
        this.mStarGauge.Max = (int) this.mConceptCardData.Rarity + 1;
        this.mStarGauge.Value = (int) this.mConceptCardData.Rarity + 1;
      }
      foreach (Scrollbar componentsInChild in this.GetComponentsInChildren<Scrollbar>())
        componentsInChild.value = 1f;
    }

    private void SetFlavorTextText()
    {
      this.SetText(this.mFlavorText, this.mConceptCardData.Param.GetLocalizedTextFlavor());
    }

    private void SetText(Text text, string str)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = str;
    }

    public void Activated(int pinID)
    {
      if ((UnityEngine.Object) ConceptCardManager.Instance == (UnityEngine.Object) null)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (pinID != 0)
      {
        if (pinID != 1)
          return;
        instance.SelectedConceptCardMaterialData = (ConceptCardData) null;
      }
      else
      {
        instance.SelectedConceptCardMaterialData = this.mConceptCardData;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }
  }
}
