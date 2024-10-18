// Decompiled with JetBrains decompiler
// Type: SRPG.StreamingMovie
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class StreamingMovie : MonoSingleton<StreamingMovie>
  {
    private const string PREFAB_PATH = "UI/FullScreenMovie";
    private StreamingMovie.OnFinished onFinished;
    private bool skip;
    private StreamingMovie.Result playing_movie_info;
    private bool m_NotReplay;
    private MySound.VolumeHandle hBGMVolume;
    private MySound.VolumeHandle hVoiceVolume;
    private bool isSoundStop;

    private string URL => AssetDownloader.ExDownloadURL;

    private string PlatformMovieSuffix => "_win";

    private string Extention => ".mp4";

    public bool IsNotReplay
    {
      get => this.m_NotReplay;
      set => this.m_NotReplay = value;
    }

    public static bool IsPlaying
    {
      get
      {
        return !UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<StreamingMovie>.Instance, (UnityEngine.Object) null) && MonoSingleton<StreamingMovie>.Instance.playing_movie_info != null && !MonoSingleton<StreamingMovie>.Instance.playing_movie_info.isEnd;
      }
    }

    protected override void Initialize() => base.Initialize();

    protected override void Release() => base.Release();

    public void Skip() => this.skip = true;

    public void StopSound()
    {
      this.hBGMVolume = new MySound.VolumeHandle(MySound.EType.BGM);
      this.hBGMVolume.SetVolume(0.0f, 0.0f);
      this.hVoiceVolume = new MySound.VolumeHandle(MySound.EType.VOICE);
      this.hVoiceVolume.SetVolume(0.0f, 0.0f);
      this.isSoundStop = true;
    }

    public bool Play(string fileName, StreamingMovie.OnFinished callback = null, string prefabPath = null)
    {
      if (StreamingMovie.IsPlaying)
        return false;
      this.onFinished = callback;
      this.m_NotReplay = false;
      string url = this.MakePlatformMoviePath(fileName);
      DebugUtility.Log("Play streaming movie " + url);
      this.StartCoroutine(this.PlayInternal(url, prefabPath));
      return true;
    }

    private string MakePlatformMoviePath(string fileName)
    {
      return string.Format("{0}{1}{2}{3}", (object) this.URL, (object) fileName, (object) this.PlatformMovieSuffix, (object) this.Extention);
    }

    [DebuggerHidden]
    private IEnumerator PlayInternal(string url, string prefabPath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new StreamingMovie.\u003CPlayInternal\u003Ec__Iterator0()
      {
        url = url,
        prefabPath = prefabPath,
        \u0024this = this
      };
    }

    private void Finish()
    {
      if (this.isSoundStop)
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
      }
      if (this.onFinished != null)
      {
        try
        {
          this.onFinished(this.m_NotReplay);
        }
        catch (Exception ex)
        {
          Debug.Log((object) ex.ToString());
        }
        this.onFinished = (StreamingMovie.OnFinished) null;
      }
      this.playing_movie_info = (StreamingMovie.Result) null;
    }

    private static StreamingMovie.Result PlayFullScreenMovie(string url, string prefabPath)
    {
      GameObject gameObject1 = AssetManager.Load<GameObject>(!string.IsNullOrEmpty(prefabPath) ? prefabPath : "UI/FullScreenMovie");
      GameObject gameObject2 = (GameObject) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      FlowNode_MediaPlayerController playerController = (FlowNode_MediaPlayerController) null;
      FlowNode_MediaPlayerDispatchFinishEvent finishEventDispatcher = (FlowNode_MediaPlayerDispatchFinishEvent) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
      {
        playerController = gameObject2.GetComponent<FlowNode_MediaPlayerController>();
        finishEventDispatcher = gameObject2.GetComponent<FlowNode_MediaPlayerDispatchFinishEvent>();
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) playerController, (UnityEngine.Object) null))
        DebugUtility.LogError("FlowNode_MediaPlayerControllerが見つかりませんでした");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) finishEventDispatcher, (UnityEngine.Object) null))
        DebugUtility.LogError("FlowNode_MediaPlayerNotifyFinishが見つかりませんでした");
      playerController.SetVolume(GameUtility.Config_MusicVolume);
      playerController.Load(url);
      return new StreamingMovie.Result(finishEventDispatcher);
    }

    public delegate void OnFinished(bool is_replay);

    private class Result
    {
      public bool isEnd;

      public Result(
        FlowNode_MediaPlayerDispatchFinishEvent finishEventDispatcher)
      {
        finishEventDispatcher.onEnd += new FlowNode_MediaPlayerDispatchFinishEvent.OnEnd(this.OnEnd);
      }

      private void OnEnd() => this.isEnd = true;
    }
  }
}
