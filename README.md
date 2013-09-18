FollowTyperAndSender
===================
this software can measure your typing speed through QQ. And everyone can join and have fun.

How To Use
----------
>    1. Press `[F9]` to change your communication Group in QQ.
>    2. Press `[F3]` to fetch the messages from the QQ.
>    3. Type what you get right now.
>    4. Finally, the software can send your typing Grade to the QQ group.

Addition
---------
the hot key can change by yourself.

开发背景
--------
> * 打字是一项技能，更是一门艺术，打字速度在很多时候决定的不只是效率，还有你的心情。  
> * **金山打字通**可能是大家所熟知的提升打字速度的工具，但由于其单机的局限性，自己熟习显示乏味、无聊。   
> * 借助比较流行的聊天工具QQ，我们可以把大家打字的热情聚集起来，大家通过本**跟打器**，与群友互动，相互比较，在各项数据中，提升自己。

软件概述
--------
> * 本跟打器的主要**思路**是，利用WINDOWS API捕获QQ窗口的句柄，并获取QQ聊天内容，然后提取指定的文字，最后用户将指定的文字用自己的输入法打完。  
> * 在此期间，软件记录相应的数据，并在用户打完后，将打字的各项测试数据，发送群信息，方便所有群友，对比查看。

主要技术
--------
> * 利用`WINDOWS API` 获取QQ窗口的句柄、以及键盘事件
> * 利用 `C#`，写出各种算法，精确地测试相关数据
> * 利用 `ACCESS` 数据库，存储跟打的所有成绩

测试数据
--------
> * **跟打器**的初衷是，提供全面而精准的数据，让群友找出自己的不足，通过专项提高自己的打字技能。  
> **其测试数据项如下**  
> * 速度｜回改｜击键｜码长｜错字｜字数｜键数｜用时｜日期｜

文件结构([树形图](rsc\\文件树形图.txt))
--------


    │  .gitattributes
    │  .gitignore
	│  README.md
	│  tree.txt
	│  跟打相关－新.sln
	│  
	├─bin  //最终应用生成文件夹
	│      
	├─rsc  资源文件夹
	│      Grades.mdb
	│      mainicon.ico
	│      typerhis.ico
	│      跟打历史截图.jpg
	│      跟打历史截图.png
	│      跟打器主界面截图.jpg
	│      
	├─Tools  //删除测试成绩小工具
	│          
	├─TypeHistory //跟打成绩历史
	│          
	├─Typer //跟打器主文件
	│  │
	│  └─Resources // txt资源文件
	│          
	├─UpGrade //检查软件更新代码
	│          
	├─Win32 //操作WINDOWS API
	│      
	└─数据库操作
        
软件界面
-------
跟打器主文件**FollowTyper.exe**  
![FollowTyper.exe](\rsc/跟打器主界面截图.jpg)  
跟打历史文件**TypeHistory.exe**  
![TypeHistory.exe](\rsc/跟打历史截图.jpg)


---------------------------
WEIBO
------
For some advices and suggestions, please contact me on [SinaWeibo](
http://weibo.com/yangsonglove)

