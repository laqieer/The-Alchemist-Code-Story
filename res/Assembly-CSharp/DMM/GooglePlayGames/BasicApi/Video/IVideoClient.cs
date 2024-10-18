// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.IVideoClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Video
{
  public interface IVideoClient
  {
    void GetCaptureCapabilities(Action<ResponseStatus, VideoCapabilities> callback);

    void ShowCaptureOverlay();

    void GetCaptureState(Action<ResponseStatus, VideoCaptureState> callback);

    void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback);

    bool IsCaptureSupported();

    void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener);

    void UnregisterCaptureOverlayStateChangedListener();
  }
}
