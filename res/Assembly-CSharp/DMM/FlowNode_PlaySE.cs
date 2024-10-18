// Decompiled with JetBrains decompiler
// Type: FlowNode_PlaySE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Sound/PlaySE", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlaySE : FlowNode
{
  public string sheetName;
  public string cueID;
  public int RepeatCount;
  public float RepeatWait;
  public MySound.EType Type = MySound.EType.SE;
  public float StopSec;
  public bool FadeOutOnDestroy;
  public float FadeOutSec = 0.1f;
  public bool UseCustomPlay;
  public MySound.CueSheetHandle.ELoopFlag CustomLoopFlag;
  public float CustomDelaySec;
  private string mSheetName;
  private MySound.CueSheetHandle mHandle;
  private MySound.PlayHandle mPlayHandle;

  public override string[] GetInfoLines()
  {
    return new string[1]{ "Oneshot " + this.cueID };
  }

  protected override void Awake()
  {
    base.Awake();
    this.mSheetName = this.sheetName;
    if (!string.IsNullOrEmpty(this.mSheetName))
      return;
    this.mSheetName = "SE";
  }

  protected override void OnDestroy()
  {
    if (this.mPlayHandle != null)
    {
      this.mPlayHandle.Stop(this.FadeOutSec);
      this.mPlayHandle = (MySound.PlayHandle) null;
    }
    if (this.mHandle != null)
    {
      this.mHandle.StopDefaultAll(this.FadeOutSec);
      this.mHandle = (MySound.CueSheetHandle) null;
    }
    base.OnDestroy();
  }

  public override void OnActivate(int pinID)
  {
    if (this.RepeatCount > 0 || (double) this.StopSec > 0.0)
    {
      GameObject gameObject = new GameObject("PlaySERepeat", new System.Type[1]
      {
        typeof (PlaySERepeat)
      });
      PlaySERepeat component = gameObject.GetComponent<PlaySERepeat>();
      if (Object.op_Equality((Object) component, (Object) null))
      {
        Object.Destroy((Object) gameObject);
      }
      else
      {
        component.Setup(this.mSheetName, this.cueID, this.Type, this.RepeatCount, this.RepeatWait, this.StopSec, this.FadeOutSec, this.UseCustomPlay, this.CustomLoopFlag, this.CustomDelaySec);
        if (this.FadeOutOnDestroy)
          gameObject.transform.parent = ((Component) this).gameObject.transform;
        else
          Object.DontDestroyOnLoad((Object) gameObject);
      }
    }
    else if (!this.FadeOutOnDestroy)
    {
      MonoSingleton<MySound>.Instance.PlayOneShot(this.mSheetName, this.cueID, this.Type);
    }
    else
    {
      this.mHandle = MySound.CueSheetHandle.Create(this.mSheetName, this.Type);
      if (this.mHandle != null)
      {
        if (this.UseCustomPlay)
        {
          this.mPlayHandle = this.mHandle.Play(this.cueID, this.CustomLoopFlag, true, this.CustomDelaySec);
        }
        else
        {
          this.mHandle.CreateDefaultOneShotSource();
          this.mHandle.PlayDefaultOneShot(this.cueID, false);
        }
      }
    }
    this.ActivateOutputLinks(1);
  }
}
