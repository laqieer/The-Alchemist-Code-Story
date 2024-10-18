// Decompiled with JetBrains decompiler
// Type: SRPG.VersusURLCopy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class VersusURLCopy : MonoBehaviour
  {
    public void OnClickURL()
    {
      string format = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
      string msg = string.Empty + "iname=" + GlobalVars.SelectedQuestID + "&type=" + (object) GlobalVars.SelectedMultiPlayRoomType + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID() + "&roomid=" + (object) GlobalVars.SelectedMultiPlayRoomID;
      byte[] inArray = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg, false);
      string str = string.Format(format, (object) WWW.EscapeURL(Convert.ToBase64String(inArray)), (object) GlobalVars.SelectedMultiPlayRoomID);
      DebugUtility.Log("LINE:" + str);
      GUIUtility.systemCopyBuffer = str;
      GlobalVars.VersusRoomReuse = true;
    }
  }
}
