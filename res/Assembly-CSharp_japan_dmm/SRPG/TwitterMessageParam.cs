// Decompiled with JetBrains decompiler
// Type: SRPG.TwitterMessageParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TwitterMessageParam
  {
    private eTwitterMessageId id;
    private TwitterMessageDetailParam[] detail;

    public eTwitterMessageId Id => this.id;

    public TwitterMessageDetailParam[] Detail => this.detail;

    public void Deserialize(JSON_TwitterMessageParam json)
    {
      this.id = (eTwitterMessageId) json.id;
      this.detail = new TwitterMessageDetailParam[json.detail.Length];
      for (int index = 0; index < json.detail.Length; ++index)
      {
        TwitterMessageDetailParam messageDetailParam = new TwitterMessageDetailParam();
        messageDetailParam.Deserialize(json.detail[index]);
        this.detail[index] = messageDetailParam;
      }
    }

    public static void Deserialize(ref TwitterMessageParam[] param, JSON_TwitterMessageParam[] json)
    {
      if (json == null)
        return;
      param = new TwitterMessageParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        TwitterMessageParam twitterMessageParam = new TwitterMessageParam();
        twitterMessageParam.Deserialize(json[index]);
        param[index] = twitterMessageParam;
      }
    }
  }
}
