// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCaptureState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace GooglePlayGames.BasicApi.Video
{
  public class VideoCaptureState
  {
    private bool mIsCapturing;
    private VideoCaptureMode mCaptureMode;
    private VideoQualityLevel mQualityLevel;
    private bool mIsOverlayVisible;
    private bool mIsPaused;

    internal VideoCaptureState(
      bool isCapturing,
      VideoCaptureMode captureMode,
      VideoQualityLevel qualityLevel,
      bool isOverlayVisible,
      bool isPaused)
    {
      this.mIsCapturing = isCapturing;
      this.mCaptureMode = captureMode;
      this.mQualityLevel = qualityLevel;
      this.mIsOverlayVisible = isOverlayVisible;
      this.mIsPaused = isPaused;
    }

    public bool IsCapturing => this.mIsCapturing;

    public VideoCaptureMode CaptureMode => this.mCaptureMode;

    public VideoQualityLevel QualityLevel => this.mQualityLevel;

    public bool IsOverlayVisible => this.mIsOverlayVisible;

    public bool IsPaused => this.mIsPaused;

    public override string ToString()
    {
      return string.Format("[VideoCaptureState: mIsCapturing={0}, mCaptureMode={1}, mQualityLevel={2}, mIsOverlayVisible={3}, mIsPaused={4}]", (object) this.mIsCapturing, (object) this.mCaptureMode.ToString(), (object) this.mQualityLevel.ToString(), (object) this.mIsOverlayVisible, (object) this.mIsPaused);
    }
  }
}
