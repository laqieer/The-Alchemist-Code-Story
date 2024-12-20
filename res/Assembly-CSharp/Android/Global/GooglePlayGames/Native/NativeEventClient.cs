﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.NativeEventClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.Native
{
  internal class NativeEventClient : IEventsClient
  {
    private readonly EventManager mEventManager;

    internal NativeEventClient(EventManager manager)
    {
      this.mEventManager = Misc.CheckNotNull<EventManager>(manager);
    }

    public void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback)
    {
      Misc.CheckNotNull<Action<ResponseStatus, List<IEvent>>>(callback);
      callback = CallbackUtils.ToOnGameThread<ResponseStatus, List<IEvent>>(callback);
      this.mEventManager.FetchAll(ConversionUtils.AsDataSource(source), (Action<EventManager.FetchAllResponse>) (response =>
      {
        ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
        if (!response.RequestSucceeded())
          callback(responseStatus, new List<IEvent>());
        else
          callback(responseStatus, Enumerable.Cast<IEvent>(response.Data()).ToList<IEvent>());
      }));
    }

    public void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback)
    {
      Misc.CheckNotNull<string>(eventId);
      Misc.CheckNotNull<Action<ResponseStatus, IEvent>>(callback);
      this.mEventManager.Fetch(ConversionUtils.AsDataSource(source), eventId, (Action<EventManager.FetchResponse>) (response =>
      {
        ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
        if (!response.RequestSucceeded())
          callback(responseStatus, (IEvent) null);
        else
          callback(responseStatus, (IEvent) response.Data());
      }));
    }

    public void IncrementEvent(string eventId, uint stepsToIncrement)
    {
      Misc.CheckNotNull<string>(eventId);
      this.mEventManager.Increment(eventId, stepsToIncrement);
    }
  }
}
