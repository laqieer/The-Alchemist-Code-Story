// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StreamingMovie
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/StreamingMovie", 32741)]
  [FlowNode.Pin(1000, "Play", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(4, "Failed", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(5, "Finished", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_StreamingMovie : FlowNode
  {
    private const int PIN_ID_SUCCESS = 3;
    private const int PIN_ID_FAILED = 4;
    private const int PIN_ID_FINISHED = 5;
    private const int PIN_ID_PLAY = 1000;
    public string FileName;
    private MySound.VolumeHandle hBGMVolume;
    private MySound.VolumeHandle hVoiceVolume;
    public string ReplayText;
    public string RetryText;
    public bool AutoFade;
    public Color FadeColor = Color.black;
    private const float FadeTime = 1f;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1000)
      {
        if (pinID == 3 || pinID == 4)
          ;
      }
      else if (Application.internetReachability == null)
      {
        if (!string.IsNullOrEmpty(this.RetryText))
        {
          UIUtility.ConfirmBox(LocalizedText.Get(this.RetryText), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelRetry), systemModal: true);
        }
        else
        {
          this.ActivateOutputLinks(4);
          this.ActivateOutputLinks(5);
        }
      }
      else
        this.Play(this.FileName);
    }

    private void Play(string fileName)
    {
      ((Behaviour) this).enabled = true;
      this.hBGMVolume = new MySound.VolumeHandle(MySound.EType.BGM);
      this.hBGMVolume.SetVolume(0.0f, 0.0f);
      this.hVoiceVolume = new MySound.VolumeHandle(MySound.EType.VOICE);
      this.hVoiceVolume.SetVolume(0.0f, 0.0f);
      if (this.AutoFade)
      {
        SRPG_TouchInputModule.LockInput();
        CriticalSection.Enter();
        FadeController.Instance.FadeTo(this.FadeColor, 1f);
        this.StartCoroutine(this.PlayDelayed(fileName, new StreamingMovie.OnFinished(this.OnFinished)));
      }
      else
        MonoSingleton<StreamingMovie>.Instance.Play(fileName, new StreamingMovie.OnFinished(this.OnFinished));
    }

    public void OnFinished(bool is_replay = false)
    {
      if (this.hBGMVolume != null)
      {
        this.hBGMVolume.Discard();
        this.hBGMVolume = (MySound.VolumeHandle) null;
      }
      if (this.hVoiceVolume != null)
      {
        this.hVoiceVolume.Discard();
        this.hVoiceVolume = (MySound.VolumeHandle) null;
      }
      if (this.AutoFade)
        FadeController.Instance.FadeTo(Color.clear, 1f);
      if (!string.IsNullOrEmpty(this.ReplayText) && !is_replay)
      {
        ((Behaviour) this).enabled = false;
        UIUtility.ConfirmBox(LocalizedText.Get(this.ReplayText), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelReplay), systemModal: true);
      }
      else
      {
        this.ActivateOutputLinks(3);
        this.ActivateOutputLinks(5);
        ((Behaviour) this).enabled = false;
      }
    }

    private void OnRetry(GameObject go) => this.OnActivate(1000);

    private void OnCancelRetry(GameObject go)
    {
      this.ActivateOutputLinks(4);
      this.ActivateOutputLinks(5);
    }

    private void OnCancelReplay(GameObject go)
    {
      this.ActivateOutputLinks(3);
      this.ActivateOutputLinks(5);
    }

    [DebuggerHidden]
    private IEnumerator PlayDelayed(string filename, StreamingMovie.OnFinished callback)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StreamingMovie.\u003CPlayDelayed\u003Ec__Iterator0()
      {
        filename = filename,
        callback = callback
      };
    }
  }
}
