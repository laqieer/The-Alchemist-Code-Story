// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesAchievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
  internal class PlayGamesAchievement : IAchievementDescription, IAchievement
  {
    private string mId = string.Empty;
    private DateTime mLastModifiedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    private string mTitle = string.Empty;
    private string mRevealedImageUrl = string.Empty;
    private string mUnlockedImageUrl = string.Empty;
    private string mDescription = string.Empty;
    private readonly ReportProgress mProgressCallback;
    private double mPercentComplete;
    private bool mCompleted;
    private bool mHidden;
    private WWW mImageFetcher;
    private Texture2D mImage;
    private ulong mPoints;

    internal PlayGamesAchievement()
      : this(new ReportProgress(PlayGamesPlatform.Instance.ReportProgress))
    {
    }

    internal PlayGamesAchievement(ReportProgress progressCallback)
    {
      this.mProgressCallback = progressCallback;
    }

    internal PlayGamesAchievement(Achievement ach)
      : this()
    {
      this.mId = ach.Id;
      this.mPercentComplete = (double) ach.CurrentSteps / (double) ach.TotalSteps;
      this.mCompleted = ach.IsUnlocked;
      this.mHidden = !ach.IsRevealed;
      this.mLastModifiedTime = ach.LastModifiedTime;
      this.mTitle = ach.Name;
      this.mDescription = ach.Description;
      this.mPoints = ach.Points;
      this.mRevealedImageUrl = ach.RevealedImageUrl;
      this.mUnlockedImageUrl = ach.UnlockedImageUrl;
    }

    public void ReportProgress(Action<bool> callback)
    {
      this.mProgressCallback(this.mId, this.mPercentComplete, callback);
    }

    private Texture2D LoadImage()
    {
      if (this.hidden)
        return (Texture2D) null;
      string url = !this.completed ? this.mRevealedImageUrl : this.mUnlockedImageUrl;
      if (!string.IsNullOrEmpty(url))
      {
        if (this.mImageFetcher == null || this.mImageFetcher.url != url)
        {
          this.mImageFetcher = new WWW(url);
          this.mImage = (Texture2D) null;
        }
        if ((UnityEngine.Object) this.mImage != (UnityEngine.Object) null)
          return this.mImage;
        if (this.mImageFetcher.isDone)
        {
          this.mImage = this.mImageFetcher.texture;
          return this.mImage;
        }
      }
      return (Texture2D) null;
    }

    public string id
    {
      get
      {
        return this.mId;
      }
      set
      {
        this.mId = value;
      }
    }

    public double percentCompleted
    {
      get
      {
        return this.mPercentComplete;
      }
      set
      {
        this.mPercentComplete = value;
      }
    }

    public bool completed
    {
      get
      {
        return this.mCompleted;
      }
    }

    public bool hidden
    {
      get
      {
        return this.mHidden;
      }
    }

    public DateTime lastReportedDate
    {
      get
      {
        return this.mLastModifiedTime;
      }
    }

    public string title
    {
      get
      {
        return this.mTitle;
      }
    }

    public Texture2D image
    {
      get
      {
        return this.LoadImage();
      }
    }

    public string achievedDescription
    {
      get
      {
        return this.mDescription;
      }
    }

    public string unachievedDescription
    {
      get
      {
        return this.mDescription;
      }
    }

    public int points
    {
      get
      {
        return (int) this.mPoints;
      }
    }
  }
}
