// Decompiled with JetBrains decompiler
// Type: FlowNode_PlaySE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

[FlowNode.NodeType("Sound/PlaySE", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlaySE : FlowNode
{
  public MySound.EType Type = MySound.EType.SE;
  public float FadeOutSec = 0.1f;
  public string sheetName;
  public string cueID;
  public int RepeatCount;
  public float RepeatWait;
  public float StopSec;
  public bool FadeOutOnDestroy;
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
      if ((Object) component == (Object) null)
      {
        Object.Destroy((Object) gameObject);
      }
      else
      {
        component.Setup(this.mSheetName, this.cueID, this.Type, this.RepeatCount, this.RepeatWait, this.StopSec, this.FadeOutSec, this.UseCustomPlay, this.CustomLoopFlag, this.CustomDelaySec);
        if (this.FadeOutOnDestroy)
          gameObject.transform.parent = this.gameObject.transform;
        else
          Object.DontDestroyOnLoad((Object) gameObject);
      }
    }
    else if (!this.FadeOutOnDestroy)
    {
      MonoSingleton<MySound>.Instance.PlayOneShot(this.mSheetName, this.cueID, this.Type, 0.0f);
    }
    else
    {
      this.mHandle = MySound.CueSheetHandle.Create(this.mSheetName, this.Type, true, true, false, false);
      if (this.mHandle != null)
      {
        if (this.UseCustomPlay)
        {
          this.mPlayHandle = this.mHandle.Play(this.cueID, this.CustomLoopFlag, true, this.CustomDelaySec);
        }
        else
        {
          this.mHandle.CreateDefaultOneShotSource();
          this.mHandle.PlayDefaultOneShot(this.cueID, false, 0.0f, false);
        }
      }
    }
    this.ActivateOutputLinks(1);
  }
}
