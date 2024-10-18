// Decompiled with JetBrains decompiler
// Type: RegularLocalNotificationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public class RegularLocalNotificationParam
{
  public static readonly string CATEGORY_MORNING = "local_ordinary_morning";
  public static readonly string CATEGORY_NOON = "local_ordinary_noon";
  public static readonly string CATEGORY_AFTERNOON = "local_ordinary_afternoon";
  public string mMessage = string.Empty;
  public string mCategory = string.Empty;
  public int mHour;
  public int mMinute;
  public int mSecond;

  public RegularLocalNotificationParam(string message, string category, int hour, int min, int sec)
  {
    this.mMessage = message;
    this.mCategory = category;
    this.mHour = hour;
    this.mMinute = min;
    this.mSecond = sec;
  }
}
