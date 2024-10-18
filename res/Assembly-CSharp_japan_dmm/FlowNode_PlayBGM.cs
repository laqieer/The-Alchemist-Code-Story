// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Sound/PlayBGM", 32741)]
[FlowNode.Pin(100, "Play", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlayBGM : FlowNode
{
  public FlowNode_PlayBGM.EType Type;
  public string BGMName;

  public override string[] GetInfoLines()
  {
    if (string.IsNullOrEmpty(this.BGMName))
      return base.GetInfoLines();
    return new string[1]{ "BGM is " + this.BGMName };
  }

  public override void OnActivate(int pinID)
  {
    if (this.Type == FlowNode_PlayBGM.EType.HOME_BGM)
      FlowNode_PlayBGM.PlayHomeBGM();
    else if (string.IsNullOrEmpty(this.BGMName))
      MonoSingleton<MySound>.Instance.StopBGM();
    else
      MonoSingleton<MySound>.Instance.PlayBGM(this.BGMName);
    this.ActivateOutputLinks(1);
  }

  public static void PlayHomeBGM()
  {
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
    {
      string bgm = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam()?.bgm;
      if (!string.IsNullOrEmpty(bgm))
      {
        MonoSingleton<MySound>.Instance.PlayBGM(bgm);
        return;
      }
    }
    MonoSingleton<MySound>.Instance.PlayBGM("BGM_0027");
  }

  public static string[] GetHomeBGM()
  {
    string str = "BGM_0027";
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
    {
      SectionParam currentSectionParam = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
      if (currentSectionParam != null && !string.IsNullOrEmpty(currentSectionParam.bgm))
        str = currentSectionParam.bgm;
    }
    return new string[2]
    {
      "StreamingAssets/" + str + ".acb",
      "StreamingAssets/" + str + ".awb"
    };
  }

  public enum EType
  {
    DIRECT,
    HOME_BGM,
  }
}
