﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ChargeV2Limit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.API.Response
{
  public class ChargeV2Limit : GenericResponse<ChargeV2Limit>
  {
    public ChargeV2Limit(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
      {
        this.Age = document.Root["age"].ToInt();
        this.HasCreditLimit = this.Age < 20;
        if (!this.HasCreditLimit)
          return;
        float[] array = document.Root["accept_prices"].GetArray().Select<IValue, float>((Func<IValue, float>) (x => x.ToFloat())).ToArray<float>();
        if (array.Length > 0)
          this.CreditLimit = (float) (Math.Ceiling((double) Enumerable.Max((IEnumerable<float>) array) * 100.0) / 100.0);
        else
          this.CreditLimit = 0.0f;
      }
    }

    public int Age { get; private set; }

    public bool HasCreditLimit { get; private set; }

    public float CreditLimit { get; private set; }
  }
}
