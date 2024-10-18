// Decompiled with JetBrains decompiler
// Type: SRPG.TowerFeatureDescriptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "次のページボタン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  public class TowerFeatureDescriptionDetail : MonoBehaviour, IFlowInterface
  {
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
    private List<Toggle> mToggleIconList = new List<Toggle>();

    private void Start()
    {
      if (Object.op_Equality((Object) this.ImageData, (Object) null) && this.ImageData.Images.Length == 0)
      {
        Debug.LogError((object) "ImageData not data.");
      }
      else
      {
        this.ImageData.ImageIndex = 0;
        if (this.ImageData.Images.Length == 1)
        {
          ((Component) this.NextButton).gameObject.SetActive(false);
          ((Component) this.PrevButton).gameObject.SetActive(false);
        }
        else
          ((Selectable) this.PrevButton).interactable = false;
        this.TemplatePageIcon.SetActive(false);
        for (int index = 0; index < this.ImageData.Images.Length; ++index)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.TemplatePageIcon);
          Vector2 vector2 = Vector2.op_Implicit(gameObject.transform.localScale);
          gameObject.transform.SetParent(this.ParentPageIcon.transform);
          gameObject.transform.localScale = Vector2.op_Implicit(vector2);
          gameObject.gameObject.SetActive(true);
          ((Object) gameObject).name = ((Object) this.TemplatePageIcon).name + (index + 1).ToString();
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
        ((Selectable) this.NextButton).interactable = false;
        ((Selectable) this.PrevButton).interactable = true;
      }
      else
      {
        if (this.ImageData.ImageIndex != 0)
          return;
        ((Selectable) this.NextButton).interactable = true;
        ((Selectable) this.PrevButton).interactable = false;
      }
    }

    private void SetTobbleIcon()
    {
      for (int index = 0; index < this.mToggleIconList.Count; ++index)
        this.mToggleIconList[index].isOn = index == this.ImageData.ImageIndex;
    }
  }
}
