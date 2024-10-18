// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "DraftMatching", FlowNode.PinTypes.Input, 2)]
  public class VersusMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public void Start()
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
      GlobalVars.IsVersusDraftMode = false;
    }

    public void Activated(int pinID)
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
      if (pinID == 1)
      {
        GlobalVars.IsVersusDraftMode = false;
        this.StartCoroutine(ProgressWindow.OpenVersusLoadScreenAsync());
      }
      else
      {
        if (pinID != 2)
          return;
        GlobalVars.IsVersusDraftMode = true;
        this.StartCoroutine(ProgressWindow.OpenVersusDraftLoadScreenAsync());
      }
    }
  }
}
