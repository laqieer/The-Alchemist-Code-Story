// Decompiled with JetBrains decompiler
// Type: MyLocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using gu3;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
public static class MyLocalNotification
{
  private static readonly string CATEGORY_COMEBACK = "comeback";
  private static List<LocalNotificationInfo> mLocalNotifications = new List<LocalNotificationInfo>();
  private const string LocalNoficationParamPath = "Data/Localnotification";
  private static readonly string LOCAL_NOTIFICATION_CATEGORY_STAMINA = "stamina";
  private static readonly string CATEGORY_AUTOREPEAT_QUEST = "auto_repeat_quest";

  public static List<LocalNotificationInfo> LocaloNotifications
  {
    get => MyLocalNotification.mLocalNotifications;
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

  public static void Accept() => EnableLocalNotification.Exec();

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
    }, utc.Hour, utc.Minute, utc.Second, (LocalNotification.Weekday) (int) sbyte.MaxValue);
  }

  public static void SetComeBack(string message, long interval_sec)
  {
    LocalNotification.ScheduleWithTimeInterval(new LocalNotification.Notification()
    {
      message = message,
      category = MyLocalNotification.CATEGORY_COMEBACK
    }, (int) interval_sec);
  }

  public static void ResetComeBack()
  {
    LocalNotification.CancelNotificationsWithCategory(MyLocalNotification.CATEGORY_COMEBACK);
  }

  public static void SetAutoRepeatQuest(string message, DateTime date)
  {
    LocalNotification.Notification notification = new LocalNotification.Notification();
    notification.message = message;
    notification.category = MyLocalNotification.CATEGORY_AUTOREPEAT_QUEST;
    DateTime dateTime = date.AddSeconds((double) -TimeManager.UTC2LOCAL);
    DebugUtility.Log("*** AutoRepeatQuest EndTime: " + date.ToString("yyyy/MM/dd HH:mm:ss") + " ***");
    DebugUtility.Log("*** AutoRepeatQuest Notification: " + dateTime.ToString("yyyy/MM/dd HH:mm:ss") + " ***");
    LocalNotification.Schedule(notification, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
  }

  public static void ResetAutoRepeatQuest()
  {
    LocalNotification.CancelNotificationsWithCategory(MyLocalNotification.CATEGORY_AUTOREPEAT_QUEST);
  }
}
