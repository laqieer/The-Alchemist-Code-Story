﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GachaRateParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class GachaRateParam
  {
    private string m_name = string.Empty;
    private float m_rate;
    private GachaDropData.Type m_type;
    private float m_rate_coef;
    private int m_rarity;
    private EElement m_elem;
    private int m_num;
    private int m_hash;
    public long sortPriority;

    public float Rate
    {
      get
      {
        return this.m_rate;
      }
    }

    public GachaDropData.Type Type
    {
      get
      {
        return this.m_type;
      }
    }

    public float CalcRate
    {
      get
      {
        return (float) ((double) this.m_rate / (double) this.m_rate_coef * 100.0);
      }
    }

    public string name
    {
      get
      {
        return this.m_name;
      }
    }

    public int rarity
    {
      get
      {
        return this.m_rarity;
      }
    }

    public EElement elem
    {
      get
      {
        return this.m_elem;
      }
    }

    public int hash
    {
      get
      {
        return this.m_hash;
      }
    }

    public int num
    {
      get
      {
        return this.m_num;
      }
    }

    public bool Deserialize(JSON_GachaRateParam json)
    {
      if (json == null)
      {
        DebugUtility.LogError("*** FlowNode_ReqGachaRate : Deserialize's json is null");
        return false;
      }
      this.m_rate = json.rate;
      if (json.itype == "item")
      {
        this.m_type = GachaDropData.Type.Item;
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(json.iname);
        if (itemParam == null)
        {
          DebugUtility.LogError("GachaInfoRate=>iname:" + json.iname + "はItemParamに存在しません.");
          return false;
        }
        this.m_name = itemParam.name;
        this.m_rarity = itemParam.rare;
        this.m_num = json.num;
        this.m_hash = itemParam.iname.GetHashCode();
      }
      else if (json.itype == "unit")
      {
        this.m_type = GachaDropData.Type.Unit;
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.iname);
        if (unitParam == null)
        {
          DebugUtility.LogError("GachaInfoRate=>iname:" + json.iname + "はUnitParamに存在しません.");
          return false;
        }
        this.m_name = unitParam.name;
        this.m_rarity = (int) unitParam.rare;
        this.m_elem = unitParam.element;
      }
      else if (json.itype == "artifact")
      {
        this.m_type = GachaDropData.Type.Artifact;
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
        if (artifactParam == null)
        {
          DebugUtility.LogError("GachaInfoRate=>iname:" + json.iname + "はArtifactParamに存在しません.");
          return false;
        }
        this.m_name = artifactParam.name;
        this.m_rarity = json.rare == -1 ? artifactParam.rareini : json.rare;
      }
      else if (json.itype == "concept_card")
      {
        this.m_type = GachaDropData.Type.ConceptCard;
        ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
        if (conceptCardParam == null)
        {
          DebugUtility.LogError("GachaInfoRate=>iname:" + json.iname + "はArtifactParamに存在しません.");
          return false;
        }
        this.m_name = conceptCardParam.name;
        this.m_rarity = conceptCardParam.rare;
      }
      return true;
    }

    public void SetCoef(float value)
    {
      this.m_rate_coef = value;
    }
  }
}
