// Decompiled with JetBrains decompiler
// Type: UniWebViewAndroidStaticListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Reflection;
using UnityEngine;

#nullable disable
public class UniWebViewAndroidStaticListener : MonoBehaviour
{
  private void Awake() => Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);

  private void OnJavaMessage(string message)
  {
    string[] strArray1 = message.Split("@"[0]);
    if (strArray1.Length < 3)
    {
      Debug.Log((object) "Not enough parts for receiving a message.");
    }
    else
    {
      UniWebViewNativeListener listener = UniWebViewNativeListener.GetListener(strArray1[0]);
      if (Object.op_Equality((Object) listener, (Object) null))
      {
        Debug.Log((object) "Unable to find listener");
      }
      else
      {
        MethodInfo method = typeof (UniWebViewNativeListener).GetMethod(strArray1[1]);
        if ((object) method == null)
          Debug.Log((object) ("Cannot find correct method to invoke: " + strArray1[1]));
        int length = strArray1.Length - 2;
        string[] strArray2 = new string[length];
        for (int index = 0; index < length; ++index)
          strArray2[index] = strArray1[index + 2];
        method.Invoke((object) listener, new object[1]
        {
          (object) string.Join("@", strArray2)
        });
      }
    }
  }
}
