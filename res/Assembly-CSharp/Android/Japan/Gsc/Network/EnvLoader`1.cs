// Decompiled with JetBrains decompiler
// Type: Gsc.Network.EnvLoader`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index = "ver";
      Serializer instance = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0;
      object obj = instance.Add<string>(fMgCache0).Serialize<string>(SRPG.Network.Version);
      dictionary2[index] = obj;
      return dictionary1;
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
          T instance = Activator.CreateInstance<T>();
          foreach (Member member in @object)
          {
            if (!(member.Name == "body"))
            {
              if (member.Value.IsLong())
                instance.SetValue(member.Name, member.Value.ToLong().ToString());
              else
                instance.SetValue(member.Name, member.Value.ToString());
            }
          }
          Value obj;
          if (!@object.TryGetValue("body", out obj))
          {
            this.Envs.Add("error", (Configuration.IEnvironment) instance);
            this.VerRoute.Add("default", "error");
          }
          else
          {
            foreach (Member member1 in obj.GetObject())
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
                      else if (member2.Value.IsInt())
                        instance.SetValue(member2.Name, member2.Value.ToInt().ToString());
                      else
                        instance.SetValue(member2.Name, member2.Value.ToString());
                    }
                    this.Envs.Add(name, (Configuration.IEnvironment) instance);
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
