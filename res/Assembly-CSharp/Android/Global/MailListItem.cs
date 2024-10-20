﻿// Decompiled with JetBrains decompiler
// Type: MailListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MailListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject limit;
  [SerializeField]
  private GameObject button;
  [SerializeField]
  private Text timeText;
  private ListItemEvents _listItemEvents;

  public ListItemEvents listItemEvents
  {
    get
    {
      return this.GetComponent<ListItemEvents>();
    }
  }

  public GameObject Button
  {
    get
    {
      return this.button;
    }
  }

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
    {
      this.limit.gameObject.SetActive(false);
      this.button.gameObject.SetActive(false);
      this.SetTime(read);
    }
    else
    {
      this.limit.gameObject.SetActive(isPeriod);
      this.button.gameObject.SetActive(true);
      this.SetTime(post_at);
    }
  }

  private void SetTime(long time)
  {
    DateTime serverTime = TimeManager.ServerTime;
    DateTime localTime = GameUtility.UnixtimeToLocalTime(time);
    TimeSpan timeSpan = serverTime - localTime;
    string empty = string.Empty;
    string str;
    if (timeSpan.Days >= 1)
    {
      string format = "yyyy/MM/dd";
      str = localTime.ToString(format);
    }
    else if (timeSpan.Hours >= 1)
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_HOURS", new object[1]
      {
        (object) timeSpan.Hours
      });
    else if (timeSpan.Minutes >= 1)
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_MINUTES", new object[1]
      {
        (object) timeSpan.Minutes
      });
    else
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_SECONDS", new object[1]
      {
        (object) timeSpan.Seconds
      });
    this.timeText.text = str;
  }
}