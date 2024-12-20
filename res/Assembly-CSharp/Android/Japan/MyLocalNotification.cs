﻿// Decompiled with JetBrains decompiler
// Type: MyLocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using gu3;
using SRPG;
using System;
using System.Collections.Generic;

public static class MyLocalNotification
{
  private static List<LocalNotificationInfo> mLocalNotifications = new List<LocalNotificationInfo>();
  private static readonly string LOCAL_NOTIFICATION_CATEGORY_STAMINA = "stamina";
  private const string LocalNoficationParamPath = "Data/Localnotification";

  public static List<LocalNotificationInfo> LocaloNotifications
  {
    get
    {
      return MyLocalNotification.mLocalNotifications;
    }
  }

  public static bool Setup(string path)
  {
    path = !string.IsNullOrEmpty(path) ? path : "Data/Localnotification";
    string src = AssetManager.LoadTextData(path);
    if (string.IsNullOrEmpty(src))
      return false;
    try
    {
      JSON_LocalNotificationInfo[] jsonArray = JSONParser.parseJSONArray<JSON_LocalNotificationInfo>(src);
      if (jsonArray == null)
        throw new InvalidJSONException();
      MyLocalNotification.mLocalNotifications.Clear();
      foreach (JSON_LocalNotificationInfo json in jsonArray)
      {
        LocalNotificationInfo notificationInfo = new LocalNotificationInfo();
        if (notificationInfo.Deserialize(json))
          MyLocalNotification.mLocalNotifications.Add(notificationInfo);
      }
    }
    catch (Exception ex)
    {
      DebugUtility.LogException(ex);
      return false;
    }
    return true;
  }

  public static void Accept()
  {
    EnableLocalNotification.Exec();
  }

  public static void CancelStamina()
  {
    LocalNotification.CancelNotificationsWithCategory(MyLocalNotification.LOCAL_NOTIFICATION_CATEGORY_STAMINA);
  }

  public static void SetStamina(LocalNotificationParam param, PlayerData player)
  {
    if (param == null || player == null)
      return;
    MyLocalNotification.CancelStamina();
    if (!GameUtility.Config_UsePushStamina.Value)
      return;
    LocalNotification.Notification notification = new LocalNotification.Notification();
    notification.message = LocalizedText.Get("sys.LOCAL_NOTIFICATION_STAMINA");
    notification.category = MyLocalNotification.LOCAL_NOTIFICATION_CATEGORY_STAMINA;
    long num = (long) (player.StaminaMax - player.Stamina) * player.StaminaRecverySec;
    if (num < (long) param.limitSec_stamina)
      return;
    LocalNotification.ScheduleWithTimeInterval(notification, (int) num);
  }

  public static void SetRegular(RegularLocalNotificationParam param, PlayerData player)
  {
    if (param == null || player == null)
      return;
    DateTime utc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, param.mHour, param.mMinute, param.mSecond, DateTimeKind.Local));
    LocalNotification.ScheduleWithRepeat(new LocalNotification.Notification()
    {
      message = param.mMessage,
      category = param.mCategory
    }, utc.Hour, utc.Minute, utc.Second, LocalNotification.Weekday.Everyday);
  }
}
