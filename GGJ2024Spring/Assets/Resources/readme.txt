往Resources文件夹内添加新种类的资源时，新建一个文件夹并命名为你想要的名字（并且需要在名字末尾加一个s），
然后在Scripts/Managers/LoadManager.cs文件中添加一个对应的switch case的分支（格式仿照原有分支，注意case接的名字末尾是不带s的，可以看下面的示例）
用以上方法命名之后，你可以使用LoadResource类型的事件（北海的框架里面的事件系统）来加载这个文件夹里面的资源而不是使用Unity自带的LoadResource方法。

例如我希望添加一个Plants文件夹用来存放各种花朵的预制体，在把预制体都放到这个文件夹里面之后，在LoadManager.cs里面添加一行case语句：
case ResourceType.Plant:                  //这个ResourceType是一个枚举，需要ctrl+左键自行跳转过去添加一个叫Plant的类型
                    o = Resources.Load<Material>("Plants/" + objectName);                //这里的Plants就是你新建的文件夹名字，所以实际上还是用文件路径搜索资源
                    if (o != null)
                        o = Instantiate((GameObject)o);        //这些照搬就行
                    break;