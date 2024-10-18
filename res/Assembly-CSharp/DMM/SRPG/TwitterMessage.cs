// Decompiled with JetBrains decompiler
// Type: SRPG.TwitterMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TwitterMessage : MonoBehaviour
  {
    [SerializeField]
    private eTwitterMessageId mId;
    [SerializeField]
    private string mAppendImagePath = "Twitter/twitter_logo";
    private string mConditionsKey;

    private void Awake()
    {
      GameUtility.SetGameObjectActive((Component) ((Component) this).transform.parent, SNSController.IsInstalled_Twitter);
    }

    public void SetConditionsKey(string key) => this.mConditionsKey = key;

    public void Post()
    {
      if (SNSController.Instance.IsProcessingCapture)
        return;
      SNSController.RefreshInstalled_Twitter();
      if (!SNSController.IsInstalled_Twitter)
      {
        DebugUtility.LogError("Twitterがインストールされていません");
      }
      else
      {
        TwitterMessageParam[] twitterMessageParams = MonoSingleton<GameManager>.Instance.MasterParam.TwitterMessageParams;
        if (twitterMessageParams == null || twitterMessageParams.Length <= 0)
        {
          DebugUtility.LogError("MasterParam > TwitterMessage is not found!!");
        }
        else
        {
          TwitterMessageParam twitterMessageParam = Array.Find<TwitterMessageParam>(twitterMessageParams, (Predicate<TwitterMessageParam>) (p => p.Id == this.mId));
          if (twitterMessageParam == null)
          {
            twitterMessageParam = Array.Find<TwitterMessageParam>(twitterMessageParams, (Predicate<TwitterMessageParam>) (p => p.Id == eTwitterMessageId.Common));
            if (twitterMessageParam == null)
              return;
          }
          TwitterMessageDetailParam messageDetailParam = Array.Find<TwitterMessageDetailParam>(twitterMessageParam.Detail, (Predicate<TwitterMessageDetailParam>) (d => d.CndsKey == this.mConditionsKey)) ?? Array.Find<TwitterMessageDetailParam>(twitterMessageParam.Detail, (Predicate<TwitterMessageDetailParam>) (d => string.IsNullOrEmpty(d.CndsKey)));
          if (messageDetailParam == null)
            return;
          string str = string.Empty;
          if (messageDetailParam.HashTag != null)
          {
            for (int index = 0; index < messageDetailParam.HashTag.Length; ++index)
            {
              str = str + "#" + messageDetailParam.HashTag[index];
              if (index < messageDetailParam.HashTag.Length - 1)
                str += " ";
            }
          }
          SNSController.Instance.ScreenCapture(string.Format(LocalizedText.Get("sys.TWITTER_POST_MESSAGE_FORMAT"), (object) messageDetailParam.Text, (object) str), this.mAppendImagePath, ((Component) this).gameObject);
        }
      }
    }
  }
}
