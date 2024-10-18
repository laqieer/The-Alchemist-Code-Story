// Decompiled with JetBrains decompiler
// Type: SGConfigWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGConfigWindow : MonoBehaviour
{
  [SerializeField]
  private List<Toggle> languageToggles;

  private void Start()
  {
    this.updateLanguageToggles(GameUtility.Config_Language);
  }

  protected void updateLanguageToggles(string language)
  {
    for (int index = 0; index < this.languageToggles.Count; ++index)
    {
      Toggle languageToggle = this.languageToggles[index];
      languageToggle.isOn = languageToggle.name == language;
      languageToggle.interactable = !languageToggle.isOn;
    }
  }

  public void resetToggle()
  {
    for (int index = 0; index < this.languageToggles.Count; ++index)
    {
      Toggle languageToggle = this.languageToggles[index];
      if (languageToggle.name == GameUtility.Config_Language)
      {
        languageToggle.isOn = true;
        languageToggle.interactable = !languageToggle.isOn;
      }
    }
  }
}
