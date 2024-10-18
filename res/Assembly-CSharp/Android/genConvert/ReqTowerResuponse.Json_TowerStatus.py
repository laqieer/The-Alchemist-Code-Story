def ReqTowerResuponse.Json_TowerStatus(json):
    this={}#ReqTowerResuponse.Json_TowerStatusjson)
    #this.status=newTowerResuponse.Status()
    this['']
    this['status']
    if 'fname' in json:
        this['status']['fname'] = json['fname']
    this['status']
    if 'questStates' in json:
        this['status']['state'] = json['questStates']
    #TowerFloorParamtowerFloor=MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname)
    #if(towerFloor==null)
    #return
    #List<TowerFloorParam>towerFloors=MonoSingleton<GameManager>.Instance.FindTowerFloors(towerFloor.tower_id)
    #List<QuestParam>referenceQuestList=newList<QuestParam>()
    #for(shortindex=0(int)index<towerFloors.Count++index)
        #towerFloors[(int)index].FloorIndex=index
        #referenceQuestList.Add(towerFloors[(int)index].GetQuestParam())
    #QuestParamquestParam1=towerFloor.GetQuestParam()
    #foreach(QuestParamquestParam2inreferenceQuestList)
    #questParam2.state=QuestStates.New
    #this.SetQuestState(referenceQuestList,questParam1,QuestStates.Cleared,true)
    #questParam1.state=this.status.state
return this
