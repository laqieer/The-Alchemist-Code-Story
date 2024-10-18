// Decompiled with JetBrains decompiler
// Type: UniWebDemo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UniWebDemo : MonoBehaviour
{
  public GameObject cubePrefab;
  public TextMesh tipTextMesh;

  private void Start()
  {
    Debug.LogWarning((object) "UniWebView only works on iOS/Android/WP8. Please switch to these platforms in Build Settings.");
  }
}
