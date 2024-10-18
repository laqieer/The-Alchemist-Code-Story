// Decompiled with JetBrains decompiler
// Type: SRPG.RuneStatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneStatusList : StatusList
  {
    [SerializeField]
    private ImageSpriteSheet m_SetEffectIcon;
    [SerializeField]
    private Text m_SetEffectName;

    public void SetRuneSetEffect(int iconIndex, string name)
    {
      this.m_SetEffectIcon.SetSprite(iconIndex.ToString());
      this.m_SetEffectName.text = name;
    }
  }
}
