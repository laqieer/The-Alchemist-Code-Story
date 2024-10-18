// Decompiled with JetBrains decompiler
// Type: FlowNode_LangageChangeEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(1, "Language Selected", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(10, "Language Unchanged", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/OnLanguageSelected", 58751)]
public class FlowNode_LangageChangeEvent : FlowNodePersistent
{
  [SerializeField]
  protected Toggle target;
  [SerializeField]
  protected SGConfigWindow languageSelector;
  [SerializeField]
  protected string buttonLanguage;

  private void Start()
  {
    if ((Object) this.target != (Object) null)
      this.target.onValueChanged.AddListener(new UnityAction<bool>(this.onSelected));
    this.enabled = false;
  }

  private void onSelected(bool value)
  {
    if (value && this.buttonLanguage != GameUtility.Config_Language)
      this.Activate(1);
    this.target.interactable = !value;
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 1)
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      if (pinID != 10)
        return;
      this.languageSelector.resetToggle();
    }
  }
}
