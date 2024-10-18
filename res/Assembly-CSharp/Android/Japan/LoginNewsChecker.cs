// Decompiled with JetBrains decompiler
// Type: LoginNewsChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class LoginNewsChecker : MonoBehaviour
{
  private void Update()
  {
    if (!LoginNewsInfo.IsChangePubInfo())
      return;
    HomeWindow objectOfType = UnityEngine.Object.FindObjectOfType<HomeWindow>();
    if (!((UnityEngine.Object) objectOfType != (UnityEngine.Object) null))
      return;
    objectOfType.ChangeNewsState();
  }
}
