// Decompiled with JetBrains decompiler
// Type: SRPG.VersusURLCopy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  public class VersusURLCopy : MonoBehaviour
  {
    public void OnClickURL()
    {
      string format = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
      string msg = string.Empty + "iname=" + GlobalVars.SelectedQuestID + "&type=" + (object) (int) GlobalVars.SelectedMultiPlayRoomType + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetCreatorFUID() + "&roomid=" + (object) GlobalVars.SelectedMultiPlayRoomID;
      byte[] inArray = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg);
      string str = string.Format(format, (object) UnityWebRequest.EscapeURL(Convert.ToBase64String(inArray)), (object) GlobalVars.SelectedMultiPlayRoomID);
      DebugUtility.Log("LINE:" + str);
      GUIUtility.systemCopyBuffer = str;
      GlobalVars.VersusRoomReuse = true;
    }
  }
}
