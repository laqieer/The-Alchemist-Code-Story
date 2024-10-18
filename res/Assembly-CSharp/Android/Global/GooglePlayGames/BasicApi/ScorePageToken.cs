// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ScorePageToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public class ScorePageToken
  {
    private string mId;
    private object mInternalObject;
    private LeaderboardCollection mCollection;
    private LeaderboardTimeSpan mTimespan;

    internal ScorePageToken(object internalObject, string id, LeaderboardCollection collection, LeaderboardTimeSpan timespan)
    {
      this.mInternalObject = internalObject;
      this.mId = id;
      this.mCollection = collection;
      this.mTimespan = timespan;
    }

    public LeaderboardCollection Collection
    {
      get
      {
        return this.mCollection;
      }
    }

    public LeaderboardTimeSpan TimeSpan
    {
      get
      {
        return this.mTimespan;
      }
    }

    public string LeaderboardId
    {
      get
      {
        return this.mId;
      }
    }

    internal object InternalObject
    {
      get
      {
        return this.mInternalObject;
      }
    }
  }
}
