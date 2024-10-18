// Decompiled with JetBrains decompiler
// Type: AnalyticsReporterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public abstract class AnalyticsReporterBase
{
  public abstract void Setup(string inUserID, bool inIsPayingPlayer, string inGCMID, bool inIsAnalyticsDebugMode, bool inIsAnalyticsReportToDev);

  public abstract void RecordNewPlayerLogin(string inUserID);

  public abstract void RecordGuestLogin(string inUserID);

  public abstract void RecordFacebookLogin(string inUserID);

  public abstract void TrackEvent(string inEventName, Dictionary<string, object> inTrackInfo);

  public abstract void TrackLevelUpEvent(int inCurrentLevel);

  public abstract void TrackPurchaseEvent(string inProductID, string inCurrencyCode, double inPrice);

  public abstract void TrackSummonData(string inSummonMainType, string inSummonSubType, string inCurrencyUsed, int inCurrencyAmount, string inGateID, int inStepID, string inStepRewardID, int inStepRewardAmount);

  public abstract void TrackFreePremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount);

  public abstract void TrackPaidPremiumCurrencyTransaction(string inTransactionName, string inTransactionSource, long inAmount);

  public abstract void TrackNonPremiumCurrencyTransaction(string inTransactionEventName, string inTransactionSource, string inUniqueCurrencyKey, string inUniqueCurrencyID, long inAmount);

  public abstract bool HasOfferwallToShow();

  public abstract void AttemptToShowPlacement(string inMileStoneName, Action inCallbackForPlacementBeingShown);

  public abstract void AddPlacementFlow(Action<string> inPlacementWantedFlowChangeHandler);

  public abstract void RemovePlacementFlow(Action<string> inPlacementWantedFlowChangeHandler);

  public abstract void OnApplicationFocus(bool inIsFocus);
}
