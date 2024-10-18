// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCapabilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.BasicApi.Video
{
  public class VideoCapabilities
  {
    private bool mIsCameraSupported;
    private bool mIsMicSupported;
    private bool mIsWriteStorageSupported;
    private bool[] mCaptureModesSupported;
    private bool[] mQualityLevelsSupported;

    internal VideoCapabilities(bool isCameraSupported, bool isMicSupported, bool isWriteStorageSupported, bool[] captureModesSupported, bool[] qualityLevelsSupported)
    {
      this.mIsCameraSupported = isCameraSupported;
      this.mIsMicSupported = isMicSupported;
      this.mIsWriteStorageSupported = isWriteStorageSupported;
      this.mCaptureModesSupported = captureModesSupported;
      this.mQualityLevelsSupported = qualityLevelsSupported;
    }

    public bool IsCameraSupported
    {
      get
      {
        return this.mIsCameraSupported;
      }
    }

    public bool IsMicSupported
    {
      get
      {
        return this.mIsMicSupported;
      }
    }

    public bool IsWriteStorageSupported
    {
      get
      {
        return this.mIsWriteStorageSupported;
      }
    }

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
