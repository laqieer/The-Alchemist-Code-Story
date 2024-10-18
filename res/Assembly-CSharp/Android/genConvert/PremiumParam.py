def PremiumParam(json):
    this={}#PremiumParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['m_Iname'] = json['iname']
    if 'image' in json:
        this['m_Image'] = json['image']
    if 'begin_at' in json:
        this['m_BeginAt'] = json['begin_at']==null?0L:TimeManager.GetUnixSec)
    if 'begin_at' in json:
        this['m_EndAt'] = json['begin_at']==null?0L:TimeManager.GetUnixSec)
    if 'span' in json:
        this['m_Span'] = json['span']
    #returntrue
return this
