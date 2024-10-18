// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.AdjustUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace com.adjust.sdk
{
  public class AdjustUtils
  {
    public static string KeyAdid = "adid";
    public static string KeyMessage = "message";
    public static string KeyNetwork = "network";
    public static string KeyAdgroup = "adgroup";
    public static string KeyCampaign = "campaign";
    public static string KeyCreative = "creative";
    public static string KeyWillRetry = "willRetry";
    public static string KeyTimestamp = "timestamp";
    public static string KeyCallbackId = "callbackId";
    public static string KeyEventToken = "eventToken";
    public static string KeyClickLabel = "clickLabel";
    public static string KeyTrackerName = "trackerName";
    public static string KeyTrackerToken = "trackerToken";
    public static string KeyJsonResponse = "jsonResponse";
    public static string KeyTestOptionsBaseUrl = "baseUrl";
    public static string KeyTestOptionsGdprUrl = "gdprUrl";
    public static string KeyTestOptionsBasePath = "basePath";
    public static string KeyTestOptionsGdprPath = "gdprPath";
    public static string KeyTestOptionsDeleteState = "deleteState";
    public static string KeyTestOptionsUseTestConnectionOptions = "useTestConnectionOptions";
    public static string KeyTestOptionsTimerIntervalInMilliseconds = "timerIntervalInMilliseconds";
    public static string KeyTestOptionsTimerStartInMilliseconds = "timerStartInMilliseconds";
    public static string KeyTestOptionsSessionIntervalInMilliseconds = "sessionIntervalInMilliseconds";
    public static string KeyTestOptionsSubsessionIntervalInMilliseconds = "subsessionIntervalInMilliseconds";
    public static string KeyTestOptionsTeardown = "teardown";
    public static string KeyTestOptionsNoBackoffWait = "noBackoffWait";
    public static string KeyTestOptionsiAdFrameworkEnabled = "iAdFrameworkEnabled";

    public static int ConvertLogLevel(AdjustLogLevel? logLevel)
    {
      return !logLevel.HasValue ? -1 : (int) logLevel.Value;
    }

    public static int ConvertBool(bool? value)
    {
      if (!value.HasValue)
        return -1;
      return value.Value ? 1 : 0;
    }

    public static double ConvertDouble(double? value) => !value.HasValue ? -1.0 : value.Value;

    public static long ConvertLong(long? value) => !value.HasValue ? -1L : value.Value;

    public static string ConvertListToJson(List<string> list)
    {
      if (list == null)
        return (string) null;
      JSONArray jsonArray = new JSONArray();
      foreach (string aData in list)
        jsonArray.Add((JSONNode) new JSONData(aData));
      return jsonArray.ToString();
    }

    public static string GetJsonResponseCompact(Dictionary<string, object> dictionary)
    {
      string empty = string.Empty;
      if (dictionary == null)
        return empty;
      int num = 0;
      string str1 = empty + "{";
      foreach (KeyValuePair<string, object> keyValuePair in dictionary)
      {
        if (keyValuePair.Value is string str2)
        {
          if (++num > 1)
            str1 += ",";
          if (str2.StartsWith("{") && str2.EndsWith("}"))
            str1 = str1 + "\"" + keyValuePair.Key + "\":" + str2;
          else
            str1 = str1 + "\"" + keyValuePair.Key + "\":\"" + str2 + "\"";
        }
        else
        {
          Dictionary<string, object> dictionary1 = keyValuePair.Value as Dictionary<string, object>;
          if (++num > 1)
            str1 += ",";
          str1 = str1 + "\"" + keyValuePair.Key + "\":";
          str1 += AdjustUtils.GetJsonResponseCompact(dictionary1);
        }
      }
      return str1 + "}";
    }

    public static string GetJsonString(JSONNode node, string key)
    {
      if (node == (object) null)
        return (string) null;
      JSONData jsonData = node[key] as JSONData;
      if ((JSONNode) jsonData == (object) null)
        return (string) null;
      return (JSONNode) jsonData == (object) string.Empty ? (string) null : jsonData.Value;
    }

    public static void WriteJsonResponseDictionary(
      JSONClass jsonObject,
      Dictionary<string, object> output)
    {
      foreach (KeyValuePair<string, JSONNode> keyValuePair in jsonObject)
      {
        JSONClass asObject = keyValuePair.Value.AsObject;
        string key = keyValuePair.Key;
        if ((JSONNode) asObject == (object) null)
        {
          string str = keyValuePair.Value.Value;
          output.Add(key, (object) str);
        }
        else
        {
          Dictionary<string, object> output1 = new Dictionary<string, object>();
          output.Add(key, (object) output1);
          AdjustUtils.WriteJsonResponseDictionary(asObject, output1);
        }
      }
    }

    public static string TryGetValue(Dictionary<string, string> dictionary, string key)
    {
      string str;
      if (!dictionary.TryGetValue(key, out str))
        return (string) null;
      return str == string.Empty ? (string) null : str;
    }
  }
}
