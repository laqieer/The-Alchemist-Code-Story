// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Navigation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/Navigation", 32741)]
  [FlowNode.Pin(1, "Show", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Discard", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "Destory", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_Navigation : FlowNode
  {
    public NavigationWindow Template;
    public string TemplatePath = "UI/TutNav";
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
      if (string.IsNullOrEmpty(path) || !Object.op_Equality((Object) FlowNode_Navigation.m_Template, (Object) null))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(path);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      FlowNode_Navigation.m_Template = gameObject.GetComponent<NavigationWindow>();
      if (!Object.op_Equality((Object) FlowNode_Navigation.m_Template, (Object) null))
        return;
      DebugUtility.LogError("Failed is NavigationWindow Class!");
    }
  }
}
