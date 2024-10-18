// Decompiled with JetBrains decompiler
// Type: SRPG.TowerFeatureDescriptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "次のページボタン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  public class TowerFeatureDescriptionDetail : MonoBehaviour, IFlowInterface
  {
    private List<Toggle> mToggleIconList = new List<Toggle>();
    private const int PIN_NEXT_BUTTON = 1;
    private const int PIN_PREV_BUTTON = 2;
    [SerializeField]
    private ImageArray ImageData;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private GameObject ParentPageIcon;
    [SerializeField]
    private GameObject TemplatePageIcon;

    private void Start()
    {
      if ((UnityEngine.Object) this.ImageData == (UnityEngine.Object) null && this.ImageData.Images.Length == 0)
      {
        Debug.LogError((object) "ImageData not data.");
      }
      else
      {
        this.ImageData.ImageIndex = 0;
        if (this.ImageData.Images.Length == 1)
        {
          this.NextButton.gameObject.SetActive(false);
          this.PrevButton.gameObject.SetActive(false);
        }
        else
          this.PrevButton.interactable = false;
        this.TemplatePageIcon.SetActive(false);
        for (int index = 0; index < this.ImageData.Images.Length; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TemplatePageIcon);
          Vector2 localScale = (Vector2) gameObject.transform.localScale;
          gameObject.transform.SetParent(this.ParentPageIcon.transform);
          gameObject.transform.localScale = (Vector3) localScale;
          gameObject.gameObject.SetActive(true);
          gameObject.name = this.TemplatePageIcon.name + (index + 1).ToString();
          this.mToggleIconList.Add(gameObject.GetComponent<Toggle>());
        }
        this.mToggleIconList[0].isOn = true;
      }
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.ImageData.ImageIndex >= this.ImageData.Images.Length - 1)
            break;
          ++this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
        case 2:
          if (this.ImageData.ImageIndex <= 0)
            break;
          --this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
      }
    }

    private void SetButtonInteractable()
    {
      if (this.ImageData.Images.Length == 1)
        return;
      if (this.ImageData.ImageIndex == this.ImageData.Images.Length - 1)
      {
        this.NextButton.interactable = false;
        this.PrevButton.interactable = true;
      }
      else
      {
        if (this.ImageData.ImageIndex != 0)
          return;
        this.NextButton.interactable = true;
        this.PrevButton.interactable = false;
      }
    }

    private void SetTobbleIcon()
    {
      for (int index = 0; index < this.mToggleIconList.Count; ++index)
        this.mToggleIconList[index].isOn = index == this.ImageData.ImageIndex;
    }
  }
}
