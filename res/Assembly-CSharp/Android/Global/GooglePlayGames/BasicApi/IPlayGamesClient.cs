﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.IPlayGamesClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames.BasicApi
{
  public interface IPlayGamesClient
  {
    void Authenticate(Action<bool> callback, bool silent);

    bool IsAuthenticated();

    void SignOut();

    string GetToken();

    string GetUserId();

    void LoadFriends(Action<bool> callback);

    string GetUserDisplayName();

    string GetAccessToken();

    string GetIdToken();

    string GetUserEmail();

    string GetUserImageUrl();

    void GetPlayerStats(Action<CommonStatusCodes, PlayGamesLocalUser.PlayerStats> callback);

    void LoadUsers(string[] userIds, Action<IUserProfile[]> callback);

    Achievement GetAchievement(string achievementId);

    void LoadAchievements(Action<Achievement[]> callback);

    void UnlockAchievement(string achievementId, Action<bool> successOrFailureCalllback);

    void RevealAchievement(string achievementId, Action<bool> successOrFailureCalllback);

    void IncrementAchievement(string achievementId, int steps, Action<bool> successOrFailureCalllback);

    void SetStepsAtLeast(string achId, int steps, Action<bool> callback);

    void ShowAchievementsUI(Action<UIStatus> callback);

    void ShowLeaderboardUI(string leaderboardId, LeaderboardTimeSpan span, Action<UIStatus> callback);

    void LoadScores(string leaderboardId, LeaderboardStart start, int rowCount, LeaderboardCollection collection, LeaderboardTimeSpan timeSpan, Action<LeaderboardScoreData> callback);

    void LoadMoreScores(ScorePageToken token, int rowCount, Action<LeaderboardScoreData> callback);

    int LeaderboardMaxResults();

    void SubmitScore(string leaderboardId, long score, Action<bool> successOrFailureCalllback);

    void SubmitScore(string leaderboardId, long score, string metadata, Action<bool> successOrFailureCalllback);

    IRealTimeMultiplayerClient GetRtmpClient();

    ITurnBasedMultiplayerClient GetTbmpClient();

    ISavedGameClient GetSavedGameClient();

    IEventsClient GetEventsClient();

    IQuestsClient GetQuestsClient();

    void RegisterInvitationDelegate(InvitationReceivedDelegate invitationDelegate);

    IUserProfile[] GetFriends();

    IntPtr GetApiClient();
  }
}
