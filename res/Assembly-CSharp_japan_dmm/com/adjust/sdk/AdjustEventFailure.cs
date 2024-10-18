// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.AdjustEventFailure
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace com.adjust.sdk
{
  public class AdjustEventFailure
  {
    public AdjustEventFailure()
    {
    }

    public AdjustEventFailure(Dictionary<string, string> eventFailureDataMap)
    {
      if (eventFailureDataMap == null)
        return;
      this.Adid = AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyAdid);
      this.Message = AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyMessage);
      this.Timestamp = AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyTimestamp);
      this.EventToken = AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyEventToken);
      this.CallbackId = AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyCallbackId);
      bool result;
      if (bool.TryParse(AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyWillRetry), out result))
        this.WillRetry = result;
      JSONNode jsonNode = JSON.Parse(AdjustUtils.TryGetValue(eventFailureDataMap, AdjustUtils.KeyJsonResponse));
      if (!(jsonNode != (object) null) || !((JSONNode) jsonNode.AsObject != (object) null))
        return;
      this.JsonResponse = new Dictionary<string, object>();
      AdjustUtils.WriteJsonResponseDictionary(jsonNode.AsObject, this.JsonResponse);
    }

    public AdjustEventFailure(string jsonString)
    {
      JSONNode node = JSON.Parse(jsonString);
      if (node == (object) null)
        return;
      this.Adid = AdjustUtils.GetJsonString(node, AdjustUtils.KeyAdid);
      this.Message = AdjustUtils.GetJsonString(node, AdjustUtils.KeyMessage);
      this.Timestamp = AdjustUtils.GetJsonString(node, AdjustUtils.KeyTimestamp);
      this.EventToken = AdjustUtils.GetJsonString(node, AdjustUtils.KeyEventToken);
      this.CallbackId = AdjustUtils.GetJsonString(node, AdjustUtils.KeyCallbackId);
      this.WillRetry = Convert.ToBoolean(AdjustUtils.GetJsonString(node, AdjustUtils.KeyWillRetry));
      JSONNode jsonNode = node[AdjustUtils.KeyJsonResponse];
      if (jsonNode == (object) null || (JSONNode) jsonNode.AsObject == (object) null)
        return;
      this.JsonResponse = new Dictionary<string, object>();
      AdjustUtils.WriteJsonResponseDictionary(jsonNode.AsObject, this.JsonResponse);
    }

    public string Adid { get; set; }

    public string Message { get; set; }

    public string Timestamp { get; set; }

    public string EventToken { get; set; }

    public string CallbackId { get; set; }

    public bool WillRetry { get; set; }

    public Dictionary<string, object> JsonResponse { get; set; }

    public void BuildJsonResponseFromString(string jsonResponseString)
    {
      JSONNode jsonNode = JSON.Parse(jsonResponseString);
      if (jsonNode == (object) null)
        return;
      this.JsonResponse = new Dictionary<string, object>();
      AdjustUtils.WriteJsonResponseDictionary(jsonNode.AsObject, this.JsonResponse);
    }

    public string GetJsonResponse() => AdjustUtils.GetJsonResponseCompact(this.JsonResponse);
  }
}
