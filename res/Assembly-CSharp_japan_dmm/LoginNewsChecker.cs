// Decompiled with JetBrains decompiler
// Type: LoginNewsChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class LoginNewsChecker : MonoBehaviour
{
  private void Update()
  {
    if (!LoginNewsInfo.IsChangePubInfo())
      return;
    HomeWindow objectOfType = Object.FindObjectOfType<HomeWindow>();
    if (!Object.op_Inequality((Object) objectOfType, (Object) null))
      return;
    objectOfType.ChangeNewsState();
  }
}
