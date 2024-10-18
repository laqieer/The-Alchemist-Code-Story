// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckJukePlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/PlayerPrefs/CheckJukePlayerPrefs", 32741)]
  [FlowNode.Pin(0, "False", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "True", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Check", FlowNode.PinTypes.Input, 2)]
  public class FlowNode_CheckJukePlayerPrefs : FlowNode
  {
    private const int GET_FALSE = 0;
    private const int GET_TRUE = 1;
    private const int CHECK = 2;

    public override void OnActivate(int pinID)
    {
      if (pinID == 2)
      {
        bool flag = false;
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.JUKEBOX_UNLOCK_BADGE_INFO))
        {
          string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.JUKEBOX_UNLOCK_BADGE_INFO, string.Empty);
          if (!string.IsNullOrEmpty(str))
          {
            JukeBoxWindow.PrefsUnlockBadgeInfo prefsUnlockBadgeInfo = new JukeBoxWindow.PrefsUnlockBadgeInfo();
            flag = JsonUtility.FromJson<JukeBoxWindow.PrefsUnlockBadgeInfo>(str).list.Count > 0;
          }
        }
        this.ActivateOutputLinks(!flag ? 0 : 1);
      }
      else
        this.ActivateOutputLinks(0);
    }
  }
}
