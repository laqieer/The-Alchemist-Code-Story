import os
import json
import re

def EnumToJson():
    # path to files
    path=os.path.dirname(os.path.realpath(__file__))
    mypath = os.path.join(path,'Japan','SRPG')
    files = os.listdir(mypath)

    # enum
    enum = {}

    for f in files:
        print(f)
        try:
            with open(os.path.join(mypath,f), "rt", encoding='utf8') as f:
                file = f.read()

            pre='public enum '

            while pre in file:
                file=file[file.index(pre)+len(pre):]
                text=file.split('\n')

                cEnum=[[],{}]
                use=0
                name=text[0]
                if ' ' in name:
                    name=name[:name.index(' ')]
                bracket=0

                for line in text[1:]:
                    line=line.lstrip(' ').rstrip(' ')
                    if line == '{':
                        bracket+=1
                    elif line == '}':
                        bracket-=1
                        if bracket==0:
                            break
                    else: #normal entry
                        if bracket!=1:
                            continue
                        if '[' in line:
                            line=line[:line.index('[')].rstrip(' ')+line[line.rindex(']')+1:].lstrip(' ')
                        if '=' in line: # = 16, // 0x00000010"
                            try:
                                line=line.split(' = ',1)
                                line[1]=line[1].split(',')[0].replace('L','')
                                cEnum[1][int(line[1],0)]=line[0]
                            except IndexError:
                                continue
                        else:
                            cEnum[0].append(line.rstrip(','))

                if len(cEnum[0])==0:
                    use=1
                print(str(len(cEnum[use])))    
                enum[name]=cEnum[use]
        except PermissionError:
            print('PermissionError:')#[QuestMissionType(QuestMissionValueType.ValueIsNone)] 
    
    #fix stringified indexes
    enum={
        key:{
            i:val
            for i,val in enumerate(dir)
        } if type(dir) == list else dir
        for key,dir in enum.items()
    }

    #dump = re.sub( r'"(\d+)"(:)',r'\1\2',dump)

    os.makedirs(path, exist_ok=True)
    name=os.path.join(path, 'Enums.json')
    with open(name, "wb") as f:
        f.write(json.dumps(enum, indent=4, ensure_ascii=False).encode('utf8'))

EnumToJson()