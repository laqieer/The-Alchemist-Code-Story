// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.SavedGameMetadataUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GooglePlayGames.OurUtils;
using System;

#nullable disable
namespace GooglePlayGames.BasicApi.SavedGame
{
  public struct SavedGameMetadataUpdate
  {
    private readonly bool mDescriptionUpdated;
    private readonly string mNewDescription;
    private readonly bool mCoverImageUpdated;
    private readonly byte[] mNewPngCoverImage;
    private readonly TimeSpan? mNewPlayedTime;

    private SavedGameMetadataUpdate(SavedGameMetadataUpdate.Builder builder)
    {
      this.mDescriptionUpdated = builder.mDescriptionUpdated;
      this.mNewDescription = builder.mNewDescription;
      this.mCoverImageUpdated = builder.mCoverImageUpdated;
      this.mNewPngCoverImage = builder.mNewPngCoverImage;
      this.mNewPlayedTime = builder.mNewPlayedTime;
    }

    public bool IsDescriptionUpdated => this.mDescriptionUpdated;

    public string UpdatedDescription => this.mNewDescription;

    public bool IsCoverImageUpdated => this.mCoverImageUpdated;

    public byte[] UpdatedPngCoverImage => this.mNewPngCoverImage;

    public bool IsPlayedTimeUpdated => this.mNewPlayedTime.HasValue;

    public TimeSpan? UpdatedPlayedTime => this.mNewPlayedTime;

    public struct Builder
    {
      internal bool mDescriptionUpdated;
      internal string mNewDescription;
      internal bool mCoverImageUpdated;
      internal byte[] mNewPngCoverImage;
      internal TimeSpan? mNewPlayedTime;

      public SavedGameMetadataUpdate.Builder WithUpdatedDescription(string description)
      {
        this.mNewDescription = Misc.CheckNotNull<string>(description);
        this.mDescriptionUpdated = true;
        return this;
      }

      public SavedGameMetadataUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
      {
        this.mCoverImageUpdated = true;
        this.mNewPngCoverImage = newPngCoverImage;
        return this;
      }

      public SavedGameMetadataUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
      {
        this.mNewPlayedTime = newPlayedTime.TotalMilliseconds <= 1.8446744073709552E+19 ? new TimeSpan?(newPlayedTime) : throw new InvalidOperationException("Timespans longer than ulong.MaxValue milliseconds are not allowed");
        return this;
      }

      public SavedGameMetadataUpdate Build() => new SavedGameMetadataUpdate(this);
    }
  }
}
