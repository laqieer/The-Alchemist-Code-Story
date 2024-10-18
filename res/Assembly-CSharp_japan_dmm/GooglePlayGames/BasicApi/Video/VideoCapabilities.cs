// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCapabilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace GooglePlayGames.BasicApi.Video
{
  public class VideoCapabilities
  {
    private bool mIsCameraSupported;
    private bool mIsMicSupported;
    private bool mIsWriteStorageSupported;
    private bool[] mCaptureModesSupported;
    private bool[] mQualityLevelsSupported;

    internal VideoCapabilities(
      bool isCameraSupported,
      bool isMicSupported,
      bool isWriteStorageSupported,
      bool[] captureModesSupported,
      bool[] qualityLevelsSupported)
    {
      this.mIsCameraSupported = isCameraSupported;
      this.mIsMicSupported = isMicSupported;
      this.mIsWriteStorageSupported = isWriteStorageSupported;
      this.mCaptureModesSupported = captureModesSupported;
      this.mQualityLevelsSupported = qualityLevelsSupported;
    }

    public bool IsCameraSupported => this.mIsCameraSupported;

    public bool IsMicSupported => this.mIsMicSupported;

    public bool IsWriteStorageSupported => this.mIsWriteStorageSupported;

    public bool SupportsCaptureMode(VideoCaptureMode captureMode)
    {
      if (captureMode != VideoCaptureMode.Unknown)
        return this.mCaptureModesSupported[(int) captureMode];
      Logger.w("SupportsCaptureMode called with an unknown captureMode.");
      return false;
    }

    public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
    {
      if (qualityLevel != VideoQualityLevel.Unknown)
        return this.mQualityLevelsSupported[(int) qualityLevel];
      Logger.w("SupportsCaptureMode called with an unknown qualityLevel.");
      return false;
    }

    public override string ToString()
    {
      return string.Format("[VideoCapabilities: mIsCameraSupported={0}, mIsMicSupported={1}, mIsWriteStorageSupported={2}, mCaptureModesSupported={3}, mQualityLevelsSupported={4}]", (object) this.mIsCameraSupported, (object) this.mIsMicSupported, (object) this.mIsWriteStorageSupported, (object) string.Join(",", ((IEnumerable<bool>) this.mCaptureModesSupported).Select<bool, string>((Func<bool, string>) (p => p.ToString())).ToArray<string>()), (object) string.Join(",", ((IEnumerable<bool>) this.mQualityLevelsSupported).Select<bool, string>((Func<bool, string>) (p => p.ToString())).ToArray<string>()));
    }
  }
}
