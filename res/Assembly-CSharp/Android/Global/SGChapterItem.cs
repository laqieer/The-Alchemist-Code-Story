// Decompiled with JetBrains decompiler
// Type: SGChapterItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGChapterItem : MonoBehaviour
{
  [SerializeField]
  private ImageArray[] stars;
  [SerializeField]
  private Text progressText;

  private void Start()
  {
  }

  private void Update()
  {
  }

  public void SetProgress(int total, int completed)
  {
    this.progressText.text = completed.ToString() + "/" + (object) total;
    int num = (int) ((double) ((float) completed / (float) total) / 0.330000013113022);
    for (int index = 0; index < num; ++index)
      this.stars[index].ImageIndex = 1;
  }
}
