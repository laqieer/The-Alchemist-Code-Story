// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckHomeWorldRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("WorldRaid/CheckHomeWorldRaid", 32741)]
  [FlowNode.Pin(1, "演出発生確認", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "演出発生", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "発生しない", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_CheckHomeWorldRaid : FlowNode
  {
    private const int CHECK_START = 1;
    private const int CHECK_TRUE = 101;
    private const int CHECK_FALSE = 102;
    [SerializeField]
    private bool IsSaveFlag;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (!MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.WorldRaid))
      {
        this.ActivateOutputLinks(102);
      }
      else
      {
        WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
        if (currentWorldRaidParam == null)
        {
          this.ActivateOutputLinks(102);
        }
        else
        {
          WorldRaidNoticeData notice_data = WorldRaidNoticeData.Load(currentWorldRaidParam);
          if (notice_data == null)
            this.ActivateOutputLinks(102);
          else if (notice_data.IsNeedNotice_Home())
          {
            if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null) && HomeWindow.Current.IsSceneChanging)
              this.ActivateOutputLinks(102);
            else if ((CriticalSection.GetActive() & CriticalSections.SceneChange) == CriticalSections.SceneChange)
              this.ActivateOutputLinks(102);
            else if (Object.op_Equality((Object) GameObject.Find("HomeBG"), (Object) null) && Object.op_Equality((Object) GameObject.Find("SRPG_WORLDRAID"), (Object) null))
              this.ActivateOutputLinks(102);
            else if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null) && HomeWindow.Current.mWorldRaidLastBossStatus == HomeWindow.WorldRaidLastBossStatus.Dead || !currentWorldRaidParam.IsWithinChallenge())
            {
              notice_data.SetHome(true);
              WorldRaidNoticeData.Save(notice_data);
              this.ActivateOutputLinks(102);
            }
            else
            {
              if (this.IsSaveFlag)
              {
                notice_data.SetHome(true);
                WorldRaidNoticeData.Save(notice_data);
              }
              this.ActivateOutputLinks(101);
            }
          }
          else
            this.ActivateOutputLinks(102);
        }
      }
    }
  }
}
