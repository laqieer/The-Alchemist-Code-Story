// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AppealChargeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_AppealChargeParam
  {
    public JSON_AppealChargeParam.AppealParam fields;

    public class AppealParam
    {
      public string appeal_id = string.Empty;
      public string before_img_id = string.Empty;
      public string after_img_id = string.Empty;
      public string start_at = string.Empty;
      public string end_at = string.Empty;
    }
  }
}
