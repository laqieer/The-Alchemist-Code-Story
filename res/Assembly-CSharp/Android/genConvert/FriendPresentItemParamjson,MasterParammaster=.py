def FriendPresentItemParamjson,MasterParammaster=(json):
    this={}#FriendPresentItemParamjson,MasterParammaster=null)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['m_Id'] = json['iname']
    if 'name' in json:
        this['m_Name'] = json['name']
    if 'expr' in json:
        this['m_Expr'] = json['expr']
    #if(!string.IsNullOrEmpty(json.item))
    if 'item' in json:
        this['m_Item'] = MonoSingleton<GameManager>.Instance.GetItemParam
    if 'num' in json:
        this['m_Num'] = json['num']
    if 'zeny' in json:
        this['m_Zeny'] = json['zeny']
    #try
        #if(!string.IsNullOrEmpty(json.begin_at))
        if 'begin_at' in json:
            this['m_BeginAt'] = TimeManager.GetUnixSec)
        #if(!string.IsNullOrEmpty(json.end_at))
        if 'end_at' in json:
            this['m_EndAt'] = TimeManager.GetUnixSec)
        #if(!(this.m_Id=="FP_DEFAULT"))
        #return
        #FriendPresentItemParam.DefaultParam=this
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.ToString())
return this
