// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Navigation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/Navigation", 32741)]
  [FlowNode.Pin(1, "Show", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Discard", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "Destory", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_Navigation : FlowNode
  {
    public string TemplatePath = "UI/TutNav";
    public NavigationWindow Template;
    [StringIsTextID(false)]
    public string TextID;
    public NavigationWindow.Alignment Alignment;
    private static NavigationWindow m_Template;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      NavigationWindow.DiscardCurrent();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
          return;
        NavigationWindow.DiscardCurrent();
        this.ActivateOutputLinks(10);
        this.ActivateOutputLinks(11);
      }
      else
      {
        this.LoadTemplate(this.TemplatePath);
        NavigationWindow.Show(FlowNode_Navigation.m_Template, LocalizedText.Get(this.TextID), this.Alignment);
        this.ActivateOutputLinks(10);
      }
    }

    private void LoadTemplate(string path)
    {
      if (string.IsNullOrEmpty(path) || !((UnityEngine.Object) FlowNode_Navigation.m_Template == (UnityEngine.Object) null))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(path);
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      FlowNode_Navigation.m_Template = gameObject.GetComponent<NavigationWindow>();
      if (!((UnityEngine.Object) FlowNode_Navigation.m_Template == (UnityEngine.Object) null))
        return;
      DebugUtility.LogError("Failed is NavigationWindow Class!");
    }
  }
}
