// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardDetailImage : MonoBehaviour
  {
    [SerializeField]
    private RawImage_Transparent Image;
    [SerializeField]
    private GameObject Message;
    [SerializeField]
    private Text MessageText;
    [SerializeField]
    private GameObject OverlayImageTemplate;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.OverlayImageTemplate, (Object) null))
        this.OverlayImageTemplate.gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.Message, (Object) null))
        return;
      this.Message.gameObject.SetActive(false);
    }

    private void Start()
    {
      ConceptCardData conceptCardData;
      if (Object.op_Inequality((Object) ConceptCardManager.Instance, (Object) null))
      {
        ConceptCardManager instance = ConceptCardManager.Instance;
        conceptCardData = instance.SelectedConceptCardMaterialData == null ? instance.SelectedConceptCardData : instance.SelectedConceptCardMaterialData;
      }
      else
      {
        if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
          return;
        conceptCardData = currentValue.GetObject<ConceptCardData>("conceptcard_data");
      }
      if (!Object.op_Inequality((Object) this.Image, (Object) null) || conceptCardData == null)
        return;
      ConceptCardUnitImageSettings.ComposeUnitConceptCardImage(conceptCardData.Param, (RawImage) this.Image, this.OverlayImageTemplate, this.Message, this.MessageText);
    }
  }
}
