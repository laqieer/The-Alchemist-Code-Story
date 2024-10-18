// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/PlayerPrefs/Utility", 16729156)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_PlayerPrefs : FlowNode
  {
    public FlowNode_PlayerPrefs.Type mType;
    public string mName;
    public bool mIsSave = true;
    public int mIntParam;
    public float mFloatParam;
    public string mStringParam;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      switch (this.mType)
      {
        case FlowNode_PlayerPrefs.Type.Delete:
          PlayerPrefsUtility.DeleteKey(this.mName);
          break;
        case FlowNode_PlayerPrefs.Type.Int:
          PlayerPrefsUtility.SetInt(this.mName, this.mIntParam, this.mIsSave);
          break;
        case FlowNode_PlayerPrefs.Type.Float:
          PlayerPrefsUtility.SetFloat(this.mName, this.mFloatParam, this.mIsSave);
          break;
        case FlowNode_PlayerPrefs.Type.String:
          PlayerPrefsUtility.SetString(this.mName, this.mStringParam, this.mIsSave);
          break;
      }
      this.ActivateOutputLinks(1);
    }

    public enum Type
    {
      Delete,
      Int,
      Float,
      String,
    }
  }
}
