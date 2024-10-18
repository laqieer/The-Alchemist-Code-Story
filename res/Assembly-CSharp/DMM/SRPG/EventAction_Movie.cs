// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Movie
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/Movie", "指定のムービーをストリーミング再生します", 5592405, 4473992)]
  public class EventAction_Movie : EventAction
  {
    private static readonly string PREFIX = "movies/";
    public string Filename;
    public float FadeTime = 1f;
    public bool AutoFade;
    public Color FadeColor = Color.black;
    private bool Played;
    private string PlayFilename;
    private MySound.VolumeHandle hBGMVolume;
    private MySound.VolumeHandle hVoiceVolume;
    private const string PREFAB_PATH = "UI/FullScreenMovieDemo";

    public override void OnActivate()
    {
      NetworkReachability internetReachability = Application.internetReachability;
      if (internetReachability != 2 && internetReachability != 1)
      {
        if (internetReachability != null)
          return;
        this.ActivateNext();
      }
      else
        this.PlayMovie(EventAction_Movie.PREFIX + this.Filename);
    }

    private void PlayMovie(string filename)
    {
      this.hBGMVolume = new MySound.VolumeHandle(MySound.EType.BGM);
      this.hBGMVolume.SetVolume(0.0f, 0.0f);
      this.hVoiceVolume = new MySound.VolumeHandle(MySound.EType.VOICE);
      this.hVoiceVolume.SetVolume(0.0f, 0.0f);
      if (this.AutoFade)
      {
        SRPG_TouchInputModule.LockInput();
        CriticalSection.Enter();
        FadeController.Instance.FadeTo(this.FadeColor, this.FadeTime);
        this.PlayFilename = filename;
      }
      else
      {
        MonoSingleton<StreamingMovie>.Instance.Play(filename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
        this.Played = true;
      }
    }

    public override void Update()
    {
      if (this.Played || FadeController.Instance.IsFading())
        return;
      MonoSingleton<StreamingMovie>.Instance.Play(this.PlayFilename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
      this.Played = true;
    }

    public void Finished(bool is_replay = false)
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
      {
        SRPG_TouchInputModule.UnlockInput();
        CriticalSection.Leave();
        FadeController.Instance.FadeTo(Color.clear, this.FadeTime);
      }
      this.ActivateNext();
    }

    public override bool ReplaySkipButtonEnable() => false;

    public override void GoToEndState() => MonoSingleton<StreamingMovie>.Instance.Skip();

    public override void SkipImmediate() => MonoSingleton<StreamingMovie>.Instance.Skip();
  }
}
