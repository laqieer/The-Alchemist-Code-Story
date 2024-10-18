// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCaptureState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Video
{
  public class VideoCaptureState
  {
    private bool mIsCapturing;
    private VideoCaptureMode mCaptureMode;
    private VideoQualityLevel mQualityLevel;
    private bool mIsOverlayVisible;
    private bool mIsPaused;

    internal VideoCaptureState(bool isCapturing, VideoCaptureMode captureMode, VideoQualityLevel qualityLevel, bool isOverlayVisible, bool isPaused)
    {
      this.mIsCapturing = isCapturing;
      this.mCaptureMode = captureMode;
      this.mQualityLevel = qualityLevel;
      this.mIsOverlayVisible = isOverlayVisible;
      this.mIsPaused = isPaused;
    }

    public bool IsCapturing
    {
      get
      {
        return this.mIsCapturing;
      }
    }

    public VideoCaptureMode CaptureMode
    {
      get
      {
        return this.mCaptureMode;
      }
    }

    public VideoQualityLevel QualityLevel
    {
      get
      {
        return this.mQualityLevel;
      }
    }

    public bool IsOverlayVisible
    {
      get
      {
        return this.mIsOverlayVisible;
      }
    }

    public bool IsPaused
    {
      get
      {
        return this.mIsPaused;
      }
    }

    public override string ToString()
    {
      return string.Format("[VideoCaptureState: mIsCapturing={0}, mCaptureMode={1}, mQualityLevel={2}, mIsOverlayVisible={3}, mIsPaused={4}]", (object) this.mIsCapturing, (object) this.mCaptureMode.ToString(), (object) this.mQualityLevel.ToString(), (object) this.mIsOverlayVisible, (object) this.mIsPaused);
    }
  }
}
