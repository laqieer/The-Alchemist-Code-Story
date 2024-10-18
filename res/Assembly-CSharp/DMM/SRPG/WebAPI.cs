// Decompiled with JetBrains decompiler
// Type: SRPG.WebAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  public abstract class WebAPI
  {
    public string name;
    public string body;
    public SRPG.Network.ResponseCallback callback;
    public readonly string GumiTransactionId = Guid.NewGuid().ToString();
    public WebAPI.RequestType reqtype;
    public DownloadHandler dlHandler;
    private EncodingTypes.ESerializeCompressMethod ___serializeCompressMethod;
    private static StringBuilder mSB = new StringBuilder(512);

    public EncodingTypes.ESerializeCompressMethod serializeCompressMethod
    {
      get
      {
        return GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? GlobalVars.SelectedSerializeCompressMethod : this.___serializeCompressMethod;
      }
      set => this.___serializeCompressMethod = value;
    }

    protected static StringBuilder GetStringBuilder()
    {
      WebAPI.mSB.Length = 0;
      return WebAPI.mSB;
    }

    public static string EscapeString(string s)
    {
      s = s.Replace("\\", "\\\\");
      s = s.Replace("\"", "\\\"");
      return s;
    }

    protected static string GetRequestString<T>(T param)
    {
      return JsonUtility.ToJson((object) new WebAPI.RequestParamWithTicketId<T>(SRPG.Network.TicketID, param));
    }

    protected static string GetRequestString(string body)
    {
      string str = "{\"ticket\":" + (object) SRPG.Network.TicketID;
      if (!string.IsNullOrEmpty(body))
        str = str + ",\"param\":{" + body + "}";
      return str + "}";
    }

    protected static string GetBtlEndParamString(BattleCore.Record record, bool multi = false)
    {
      string btlEndParamString = (string) null;
      if (record != null)
      {
        int num = 0;
        string str1 = "win";
        if (multi && record.result == BattleCore.QuestResult.Pending)
          str1 = "retire";
        else if (record.result != BattleCore.QuestResult.Win)
          str1 = "lose";
        int[] numArray1 = new int[record.drops.Length];
        for (int index = 0; index < record.drops.Length; ++index)
          numArray1[index] = (int) record.drops[index];
        int[] numArray2 = new int[record.item_steals.Length];
        for (int index = 0; index < record.item_steals.Length; ++index)
          numArray2[index] = (int) record.item_steals[index];
        int[] numArray3 = new int[record.gold_steals.Length];
        for (int index = 0; index < record.gold_steals.Length; ++index)
          numArray3[index] = (int) record.gold_steals[index];
        int[] numArray4 = new int[record.bonusCount];
        for (int index = 0; index < numArray4.Length; ++index)
          numArray4[index] = (record.bonusFlags & 1 << index) == 0 ? 0 : 1;
        string str2 = btlEndParamString + "\"btlendparam\":{" + "\"time\":" + (object) num + "," + "\"result\":\"" + str1 + "\"," + "\"beats\":[";
        for (int index = 0; index < numArray1.Length; ++index)
        {
          str2 += numArray1[index].ToString();
          if (index != numArray1.Length - 1)
            str2 += ",";
        }
        string str3 = str2 + "]," + "\"steals\":{" + "\"items\":[";
        for (int index = 0; index < numArray2.Length; ++index)
        {
          str3 += numArray2[index].ToString();
          if (index != numArray1.Length - 1)
            str3 += ",";
        }
        string str4 = str3 + "]," + "\"golds\":[";
        for (int index = 0; index < numArray3.Length; ++index)
        {
          str4 += numArray3[index].ToString();
          if (index != numArray1.Length - 1)
            str4 += ",";
        }
        string str5 = str4 + "]" + "}," + "\"missions\":[";
        for (int index = 0; index < numArray4.Length; ++index)
        {
          str5 += numArray4[index].ToString();
          if (index != numArray4.Length - 1)
            str5 += ",";
        }
        string str6 = str5 + "]";
        if (multi)
          str6 = str6 + ",\"token\":\"" + JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName) + "\"";
        btlEndParamString = str6 + "}";
      }
      return btlEndParamString;
    }

    public static string KeyValueToString(string key, string value)
    {
      return string.Format("\"{0}\":\"{1}\"", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, bool value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) (!value ? 0 : 1));
    }

    public static string KeyValueToString(string key, byte value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, short value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, int value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, long value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, ushort value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, uint value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string KeyValueToString(string key, ulong value)
    {
      return string.Format("\"{0}\":{1}", (object) key, (object) value);
    }

    public static string ConvBtlResultTypesToStatus(BtlResultTypes result)
    {
      string status = "retire";
      switch (result)
      {
        case BtlResultTypes.Win:
          status = "win";
          break;
        case BtlResultTypes.Lose:
          status = "lose";
          break;
        case BtlResultTypes.Cancel:
          status = "cancel";
          break;
      }
      return status;
    }

    public enum RequestType
    {
      REQ_GSC,
      REQ_STREAM,
    }

    [MessagePackObject(true)]
    public class JSON_BaseResponse
    {
      public int stat;
      public string stat_msg;
      public string stat_code;
      public long time;
      public int ticket;
    }

    [MessagePackObject(true)]
    public class JSON_BodyResponse<T> : WebAPI.JSON_BaseResponse
    {
      public T body;
    }

    protected class RequestParamWithTicketId<T>
    {
      public int ticket;
      public T param;

      public RequestParamWithTicketId(int _ticket, T _param)
      {
        this.ticket = _ticket;
        this.param = _param;
      }
    }

    internal class JSON_BodyResponse
    {
    }
  }
}
