// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.LeaderboardManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.Native.PInvoke
{
  internal class LeaderboardManager
  {
    private readonly GameServices mServices;

    internal LeaderboardManager(GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal int LeaderboardMaxResults
    {
      get
      {
        return 25;
      }
    }

    internal void SubmitScore(string leaderboardId, long score, string metadata)
    {
      Misc.CheckNotNull<string>(leaderboardId, nameof (leaderboardId));
      Logger.d("Native Submitting score: " + (object) score + " for lb " + leaderboardId + " with metadata: " + metadata);
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_SubmitScore(this.mServices.AsHandle(), leaderboardId, (ulong) score, metadata ?? string.Empty);
    }

    internal void ShowAllUI(Action<CommonErrorStatus.UIStatus> callback)
    {
      Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_ShowAllUI(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.LeaderboardManager.ShowAllUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    internal void ShowUI(string leaderboardId, GooglePlayGames.BasicApi.LeaderboardTimeSpan span, Action<CommonErrorStatus.UIStatus> callback)
    {
      Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_ShowUI(this.mServices.AsHandle(), leaderboardId, (Types.LeaderboardTimeSpan) span, new GooglePlayGames.Native.Cwrapper.LeaderboardManager.ShowUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr((Delegate) callback));
    }

    public void LoadLeaderboardData(string leaderboardId, GooglePlayGames.BasicApi.LeaderboardStart start, int rowCount, GooglePlayGames.BasicApi.LeaderboardCollection collection, GooglePlayGames.BasicApi.LeaderboardTimeSpan timeSpan, string playerId, Action<LeaderboardScoreData> callback)
    {
      ScorePageToken token = new ScorePageToken((object) new NativeScorePageToken(GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_ScorePageToken(this.mServices.AsHandle(), leaderboardId, (Types.LeaderboardStart) start, (Types.LeaderboardTimeSpan) timeSpan, (Types.LeaderboardCollection) collection)), leaderboardId, collection, timeSpan);
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_Fetch(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, leaderboardId, new GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchCallback(LeaderboardManager.InternalFetchCallback), Callbacks.ToIntPtr<FetchResponse>((Action<FetchResponse>) (rsp => this.HandleFetch(token, rsp, playerId, rowCount, callback)), new Func<IntPtr, FetchResponse>(FetchResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchCallback))]
    private static void InternalFetchCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void HandleFetch(ScorePageToken token, FetchResponse response, string selfPlayerId, int maxResults, Action<LeaderboardScoreData> callback)
    {
      LeaderboardScoreData data = new LeaderboardScoreData(token.LeaderboardId, (GooglePlayGames.BasicApi.ResponseStatus) response.GetStatus());
      if (response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
      {
        Logger.w("Error returned from fetch: " + (object) response.GetStatus());
        callback(data);
      }
      else
      {
        data.Title = response.Leaderboard().Title();
        data.Id = token.LeaderboardId;
        GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScoreSummary(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, token.LeaderboardId, (Types.LeaderboardTimeSpan) token.TimeSpan, (Types.LeaderboardCollection) token.Collection, new GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchScoreSummaryCallback(LeaderboardManager.InternalFetchSummaryCallback), Callbacks.ToIntPtr<FetchScoreSummaryResponse>((Action<FetchScoreSummaryResponse>) (rsp => this.HandleFetchScoreSummary(data, rsp, selfPlayerId, maxResults, token, callback)), new Func<IntPtr, FetchScoreSummaryResponse>(FetchScoreSummaryResponse.FromPointer)));
      }
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchScoreSummaryCallback))]
    private static void InternalFetchSummaryCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchSummaryCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void HandleFetchScoreSummary(LeaderboardScoreData data, FetchScoreSummaryResponse response, string selfPlayerId, int maxResults, ScorePageToken token, Action<LeaderboardScoreData> callback)
    {
      if (response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && response.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
      {
        Logger.w("Error returned from fetchScoreSummary: " + (object) response);
        data.Status = (GooglePlayGames.BasicApi.ResponseStatus) response.GetStatus();
        callback(data);
      }
      else
      {
        NativeScoreSummary scoreSummary = response.GetScoreSummary();
        data.ApproximateCount = scoreSummary.ApproximateResults();
        data.PlayerScore = (IScore) scoreSummary.LocalUserScore().AsScore(data.Id, selfPlayerId);
        if (maxResults <= 0)
          callback(data);
        else
          this.LoadScorePage(data, maxResults, token, callback);
      }
    }

    public void LoadScorePage(LeaderboardScoreData data, int maxResults, ScorePageToken token, Action<LeaderboardScoreData> callback)
    {
      if (data == null)
        data = new LeaderboardScoreData(token.LeaderboardId);
      GooglePlayGames.Native.Cwrapper.LeaderboardManager.LeaderboardManager_FetchScorePage(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, ((BaseReferenceHolder) token.InternalObject).AsPointer(), (uint) maxResults, new GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchScorePageCallback(LeaderboardManager.InternalFetchScorePage), Callbacks.ToIntPtr<FetchScorePageResponse>((Action<FetchScorePageResponse>) (rsp => this.HandleFetchScorePage(data, token, rsp, callback)), new Func<IntPtr, FetchScorePageResponse>(FetchScorePageResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.LeaderboardManager.FetchScorePageCallback))]
    private static void InternalFetchScorePage(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("LeaderboardManager#InternalFetchScorePage", Callbacks.Type.Temporary, response, data);
    }

    internal void HandleFetchScorePage(LeaderboardScoreData data, ScorePageToken token, FetchScorePageResponse rsp, Action<LeaderboardScoreData> callback)
    {
      data.Status = (GooglePlayGames.BasicApi.ResponseStatus) rsp.GetStatus();
      if (rsp.GetStatus() != CommonErrorStatus.ResponseStatus.VALID && rsp.GetStatus() != CommonErrorStatus.ResponseStatus.VALID_BUT_STALE)
        callback(data);
      NativeScorePage scorePage = rsp.GetScorePage();
      if (!scorePage.Valid())
        callback(data);
      if (scorePage.HasNextScorePage())
        data.NextPageToken = new ScorePageToken((object) scorePage.GetNextScorePageToken(), token.LeaderboardId, token.Collection, token.TimeSpan);
      if (scorePage.HasPrevScorePage())
        data.PrevPageToken = new ScorePageToken((object) scorePage.GetPreviousScorePageToken(), token.LeaderboardId, token.Collection, token.TimeSpan);
      foreach (NativeScoreEntry nativeScoreEntry in scorePage)
        data.AddScore(nativeScoreEntry.AsScore(data.Id));
      callback(data);
    }
  }
}
