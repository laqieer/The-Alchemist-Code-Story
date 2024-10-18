// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AppealChargeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
