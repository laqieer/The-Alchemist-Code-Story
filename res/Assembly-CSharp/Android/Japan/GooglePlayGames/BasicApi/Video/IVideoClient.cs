// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.IVideoClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
