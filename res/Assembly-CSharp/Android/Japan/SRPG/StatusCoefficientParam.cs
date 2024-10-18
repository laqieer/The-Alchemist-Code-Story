﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StatusCoefficientParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class StatusCoefficientParam
  {
    private float mHP;
    private float mAttack;
    private float mDefense;
    private float mMagAttack;
    private float mMagDefense;
    private float mDex;
    private float mSpeed;
    private float mCritical;
    private float mLuck;
    private float mCombo;
    private float mMove;
    private float mJump;

    public float HP
    {
      get
      {
        return this.mHP;
      }
    }

    public float Attack
    {
      get
      {
        return this.mAttack;
      }
    }

    public float Defense
    {
      get
      {
        return this.mDefense;
      }
    }

    public float MagAttack
    {
      get
      {
        return this.mMagAttack;
      }
    }

    public float MagDefense
    {
      get
      {
        return this.mMagDefense;
      }
    }

    public float Dex
    {
      get
      {
        return this.mDex;
      }
    }

    public float Speed
    {
      get
      {
        return this.mSpeed;
      }
    }

    public float Critical
    {
      get
      {
        return this.mCritical;
      }
    }

    public float Luck
    {
      get
      {
        return this.mLuck;
      }
    }

    public float Combo
    {
      get
      {
        return this.mCombo;
      }
    }

    public float Move
    {
      get
      {
        return this.mMove;
      }
    }

    public float Jump
    {
      get
      {
        return this.mJump;
      }
    }

    public void Deserialize(JSON_StatusCoefficientParam json)
    {
      if (json == null)
        return;
      this.mHP = json.hp;
      this.mAttack = json.atk;
      this.mDefense = json.def;
      this.mMagAttack = json.matk;
      this.mMagDefense = json.mdef;
      this.mDex = json.dex;
      this.mSpeed = json.spd;
      this.mCritical = json.cri;
      this.mLuck = json.luck;
      this.mCombo = json.cmb;
      this.mMove = json.move;
      this.mJump = json.jmp;
    }
  }
}
