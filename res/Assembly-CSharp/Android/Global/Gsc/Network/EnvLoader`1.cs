// Decompiled with JetBrains decompiler
// Type: Gsc.Network.EnvLoader`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.DOM.Json;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Network
{
  public class EnvLoader<T> : Request<EnvLoader<T>, EnvLoader<T>.Response> where T : struct, Configuration.IEnvironment
  {
    private string url;

    public EnvLoader(string url)
    {
      this.url = url;
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["ver"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(SRPG.Network.Version)
      };
    }

    public override string GetMethod()
    {
      return "POST";
    }

    public override string GetPath()
    {
      return (string) null;
    }

    public override string GetUrl()
    {
      return this.url;
    }

    public class Response : Gsc.Network.Response<EnvLoader<T>.Response>
    {
      public readonly Dictionary<string, string> VerRoute = new Dictionary<string, string>();
      public readonly Dictionary<string, Configuration.IEnvironment> Envs = new Dictionary<string, Configuration.IEnvironment>();

      public Response(WebInternalResponse response)
      {
        using (Document document = Document.Parse(response.Payload))
        {
          Gsc.DOM.Json.Object @object = document.Root.GetObject();
          T obj1 = default (T);
          foreach (Member member in @object)
          {
            if (!(member.Name == "body"))
            {
              if (member.Value.IsLong())
                obj1.SetValue(member.Name, member.Value.ToLong().ToString());
              else if (member.Value.IsString())
                obj1.SetValue(member.Name, member.Value.ToString());
              else if (member.Value.IsInt())
                obj1.SetValue(member.Name, member.Value.ToInt().ToString());
            }
          }
          Value obj2;
          if (!@object.TryGetValue("body", out obj2))
          {
            this.Envs.Add("error", (Configuration.IEnvironment) obj1);
            this.VerRoute.Add("default", "error");
          }
          else
          {
            foreach (Member member1 in obj2.GetObject())
            {
              if (member1.Name == "environments")
              {
                using (IEnumerator<Member> enumerator = member1.Value.GetObject().GetEnumerator())
                {
                  if (enumerator.MoveNext())
                  {
                    Member current = enumerator.Current;
                    string name = current.Name;
                    foreach (Member member2 in current.Value.GetObject())
                    {
                      if (member2.Name == "env_name")
                        name = member2.Value.ToString();
                      else
                        obj1.SetValue(member2.Name, member2.Value.ToString());
                    }
                    this.Envs.Add(name, (Configuration.IEnvironment) obj1);
                    this.VerRoute.Add("default", name);
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
