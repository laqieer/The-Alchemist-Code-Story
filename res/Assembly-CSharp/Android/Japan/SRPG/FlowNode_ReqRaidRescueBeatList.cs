// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRaidRescueBeatList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("Raid/Req/Rescue/BeatList", 32741)]
  public class FlowNode_ReqRaidRescueBeatList : FlowNode_ReqRaidBase
  {
    public override WebAPI GenerateWebAPI()
    {
      return (WebAPI) new ReqRaidBeatList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
    }

    public override bool Success(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<ReqRaidBeatList.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRaidBeatList.Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new Exception("Response is NULL : /raidboss/rescue/beatlist");
        if ((UnityEngine.Object) RaidStampRallyWindow.Instance == (UnityEngine.Object) null)
          throw new Exception("RaidStampRallyWindow not exists : /raidboss/rescue/beatlist");
        RaidStampRallyWindow.Instance.SetParam(jsonObject.body);
        List<RaidBossParam> raidBossAll = RaidStampRallyWindow.GetRaidBossAll(RaidManager.Instance.RaidPeriodId);
        for (int index = 0; index < raidBossAll.Count; ++index)
          DownloadUtility.PrepareRaidBossAsset(raidBossAll[index]);
        this.StartCoroutine(this.DownloadUnitImage());
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return false;
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqRaidRescueBeatList.\u003CDownloadUnitImage\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
