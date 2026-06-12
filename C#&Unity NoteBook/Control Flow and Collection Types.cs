// This is the Notebook for C# learning in Unity. by Anqi Wu. Started on jun 5, 2026.
📒//章节
📍//知识点
🥲//没懂
⚠️ //需要特别注意的地方


📒//第二部分：Chapter 4：Control Flow and Collection Types

📍1.If-Else 判断语句 Selection Statements
//if-else就像路口的路标，满足条件走左边，不满足走右边。

    格式1: 只有if
    // 只在乎条件满足时做什么，不满足就什么都不做
    bool hasDungeonKey = true;

    if (hasDungeonKey)   // 如果hasDungeonKey是true
    {
        Debug.Log("You may enter!");
    }
    // 如果hasDungeonKey是false，什么都不发生，直接跳过


    格式2: if + else
    // 满足条件走一条路，不满足走另一条路
    if (hasDungeonKey)
    {
        Debug.Log("You may enter!");      // true时执行这里
    }
    else
    {
        Debug.Log("You shall not pass!"); // false时执行这里
    }


    格式3: if + else if + else
    // 有多个条件需要判断时使用
    int gold = 32;

    if (gold > 50)              // 第一个条件：超过50
    {
        Debug.Log("You're rich!");
    }
    else if (gold < 15)         // 第一个条件不满足，再检查这个
    {
        Debug.Log("You're poor...");
    }
    else                        // 以上条件都不满足时
    {
        Debug.Log("You're okay.");
    }

    // 执行逻辑：
    // gold = 32
    // 32 > 50? 不是 → 跳过
    // 32 < 15? 不是 → 跳过
    // 执行else → 打印 "You're okay."



📍2.比较运算符 Comparison Operators 和 逻辑运算符 Logical Operators
        //比较运算符 Comparison Operators   （用来比较两个值，结果永远是true或false）
        int a = 10;
        int b = 5;

        Debug.Log(a > b);    // true  → a大于b
        Debug.Log(a < b);    // false → a小于b
        Debug.Log(a >= 10);  // true  → a大于等于10
        Debug.Log(a <= 9);   // false → a小于等于9
        Debug.Log(a == 10);  // true  → a等于10
                            //  ⚠️ ：等于是两个等号==
                            //       一个等号=是赋值，意思完全不同！
        Debug.Log(a != b);   // true  → a不等于b


    /NOT运算符 NOT Operator  （把true变成false,把false变成true,用感叹号!表示）
        bool hasKey = false;
        // 这两种写法完全一样：
        if (hasKey == false)  { Debug.Log("No key!"); }
        if (!hasKey)          { Debug.Log("No key!"); }
        // !hasKey 的意思是 "hasKey不是true"
        // 因为hasKey是false，所以!hasKey是true，条件满足

        // 例子+1
        bool isRaining = true;
        if (!isRaining)   // 如果没有下雨
        {
            Debug.Log("Let's go outside!");
        }
        // isRaining是true，所以!isRaining是false
        // 条件不满足，什么都不打印

    
    And运算符 AND Operator  && (两个条件都必须是true,结果才是true)
        bool isAlive = true;
        bool hasWeapon = true;
        bool hasArmor = false;

        if (isAlive && hasWeapon)   // 活着 并且 有武器
        {
            Debug.Log("Ready to fight!");   // 两个都是true，执行这里
        }

        if (isAlive && hasArmor)    // 活着 并且 有护甲
        {
            Debug.Log("Well protected!");   // hasArmor是false，不执行
        }

        // AND记忆表：
        // true  && true  = true   两个都满足
        // true  && false = false  有一个不满足
        // false && true  = false  有一个不满足
        // false && false = false  两个都不满足


    Or运算符 OR Operator ||   (只要有一个条件是true,结果就是true)
        bool hasKey = false;
        bool hasPassword = true;

        if (hasKey || hasPassword)   // 有钥匙 或者 有密码
        {
            Debug.Log("You can enter!");   // hasPassword是true，所以执行
        }

        // OR记忆表：
        // true  || true  = true   两个都满足
        // true  || false = true   只要一个满足就行
        // false || true  = true   只要一个满足就行
        // false || false = false  两个都不满足



📍3.嵌套判断 Nested Statements
//在if里面再套一个if，用来处理更复杂的情况
        bool weaponEquipped = true;
        string weaponType = "Longsword";

        if (weaponEquipped)                      // 第一层：有没有武器？
        {
            if (weaponType == "Longsword")       // 第二层：是什么武器？
            {
                Debug.Log("For the Queen!");     // 两层都满足才执行
            }
            // 如果武器不是Longsword，什么都不打印
        }
        else                                     // 第一层不满足：没有武器
        {
            Debug.Log("Fists won't work against armor!");
        }

        // 执行逻辑：
        // weaponEquipped = true → 进入第一层if
        // weaponType == "Longsword" → true → 打印 "For the Queen!"


📍4.多条件组合 Evaluating Multiple Conditions
    // 可以把AND和OR组合在一起
    // 声明三个变量并赋值
        bool isAlive = true;                // 布尔值：角色是否存活 = 是
        string weaponType = "Longsword";    // 字符串：武器类型 = 长剑
        int gold = 60;                      // 整数：金币数量 = 60

    //同时检查多个条件
    // 用 && 同时检查两个条件，两个都要为true才进入{}
        if (isAlive && weaponType == "Longsword")   // isAlive是true  且 武器是长剑 
        {
            Debug.Log("For the Queen!");    // 两个条件都满足，打印这句话
        }

    //更复杂的组合
    // 用括号控制判断顺序，先算括号里的
        if ((gold > 50 || isAlive) && weaponType == "Longsword")
        //   ↑ 先算这个括号                ↑ 再用&&连接这个条件
        //   gold > 50 → 60 > 50 → true 
        //   isAlive → true 
        //   true || true → true（OR只要一个true就是true）
        //   最终：true && weaponType == "Longsword"
        //         true && true → true  进入{}
        {
            Debug.Log("Conditions met!");   // 条件满足，打印这句话
}



📍5.Switch 语句 Switch Statement
//当分支超过3-4个的时候，switch 比if-else更清晰整洁
    string characterAction = "Attack";  // 字符串：角色动作 = "Attack"

    switch (characterAction)    // 拿characterAction的值，从上到下和每个case逐一比较
    {
        case "Heal":             // 问：characterAction == "Heal"？→ "Attack" == "Heal"？ 不匹配，跳过
            Debug.Log("Potion sent.");
            break;

        case "Attack":           // 问：characterAction == "Attack"？→ "Attack" == "Attack"？匹配！进入这里
            Debug.Log("To arms!");  // 执行这行，打印 "To arms!"
            break;               // 立刻跳出整个switch，下面的case不再看了

        case "Run":              // 已经break了，不会执行到这里
            Debug.Log("Retreating!");
            break;

        default:                 // 所有case都不匹配时才来这里，这次用不到
            Debug.Log("Shields up.");
            break;               // default里的break是好习惯，保持格式统一
    }
    // 最终输出：To arms!



Fall-through Cases — 多个case共用一段代码
    int dice = 7;               // 整数：骰子点数 = 7

    switch (dice)               // 拿dice的值，从上到下和每个case逐一比较
    {
        case 7:                  // 问：dice == 7？→  匹配！
                                // 但这里没有任何代码，也没有break
                                // 所以直接"掉落"到下一个case继续执行
        case 15:                 // 没有自己去问dice == 15，而是直接从case 7掉下来的
            Debug.Log("Mediocre damage, not bad.");  // 执行这行
            break;               // 跳出switch

        case 20:                 // 已经break了，不会执行到这里
            Debug.Log("Critical hit, the creature goes down!");
            break;

        default:                 // 已经break了，不会执行到这里
            Debug.Log("You completely missed!");
            break;
    }
    // 最终输出：Mediocre damage, not bad.

    // ⚠️ 这个技巧叫 fall-through（掉落/穿透）
    // 用途：让多个case共享同一段代码
    // 规则：只有case里完全没有代码才能掉落，有代码但没有break会报错





📍6.集合类型 Collection Types
        集合就是用一个变量存放多个值

    数组Arrays - 固定大小，不能增减
    Arrays就像是一排固定数量的格子,每个格子放一个值,格子数量创建后不能改变
        // 创建数组（简写方式）
        int[] scores = { 713, 549, 984 };

        // 格子示意图：
        // 索引(Index):  [0]   [1]   [2]
        // 值(Value):    713   549   984
        // ⚠️ 索引从0开始，不是从1开始！

        // 访问元素 - 用方括号[]和索引
        Debug.Log(scores[0]);   // 713（第一个）
        Debug.Log(scores[1]);   // 549（第二个）
        Debug.Log(scores[2]);   // 984（第三个）

        // 修改元素
        scores[1] = 1001;       // 把索引1的格子改成1001

        // 获取数组长度 Length
        Debug.Log(scores.Length);   // 3（一共3个格子）

        //  超出范围 IndexOutOfRangeException 会报错！
        Debug.Log(scores[3]);   //  没有索引3！只有0,1,2


    多维数组 Multidimensional Arrays 
        二维数组, 就像表格（行和列）
        int[,] coordinates = new int[3, 2]  // int[,] 表示这是二维数组
                                            // new int[3, 2] = 创建一个3行2列的空表格
        {
            { 5, 4 },    // 第0行：第0列=5，第1列=4
            { 1, 7 },    // 第1行：第0列=1，第1列=7
            { 9, 3 }     // 第2行：第0列=9，第1列=3
        };

        int value = coordinates[0, 1];  // 去找第0行、第1列的格子
                                        // 第0行 → { 5, 4 }
                                        // 第1列 → 4
                                        // 所以value = 4

        coordinates[0, 1] = 10;         // 找到第0行、第1列的格子
                                        // 把4覆盖成10
                                        // 表格第0行变成 { 5, 10 }
    
    

    列表List ———— 可变大小，可以随时增减
        //列表就像一个可以随意增减的清单
        List<string> 队伍 = new List<string>()  // 创建一个可以随时加人减人的名单
        {
            "小明",    // 索引0
            "小红",    // 索引1
            "小刚"     // 索引2
        };
        // 现在名单：[小明, 小红, 小刚]

        队伍.Add("小李");
        // Add() 永远加到最末尾
        // 现在名单：[小明, 小红, 小刚, 小李]
        //索引：       0     1     2     3

        队伍.Insert(1, "小王");
        // 在索引1的位置塞进小王，后面的人全部往后挤一位
        // 现在名单：[小明, 小王, 小红, 小刚, 小李]
        //索引：       0     1     2     3     4

        队伍.RemoveAt(0);
        // 按索引删除，删掉索引0的人，也就是小明
        // 现在名单：[小王, 小红, 小刚, 小李]
        //索引：       0     1     2     3

        队伍.Remove("小明");
        // 按名字删除，在名单里找"小明"然后删掉
        // ⚠️ 找不到的话什么都不发生，不报错

        Debug.Log(队伍.Count);   // 数名单里现在有几个人 → 4



    字典 Dictionary — 键值对集合 用key查找Value
        //字典是一个两列的表格，左边是名字，右边是对应的数字
        “portion" ->5
        "Antidote"->7
        "Aspirin" ->1
    
        //列表只能存一列，字典可以把两个东西绑在一起，这就是为什么需要它。
        Dictionary<string, int> itemInventory = new Dictionary<string, int>()
        // <string, int> = 左边是string，右边是int
        {
            { "Potion", 5 },      // Potion 绑着 5
            { "Antidote", 7 },    // Antidote 绑着 7
            { "Aspirin", 1 }      // Aspirin 绑着 1
        };

        itemInventory["Potion"];        // 用名字查数字 → 5

        itemInventory["Potion"] = 10;   // 把Potion的数字改成10

        itemInventory.Add("Knife", 3);  // 加一行新的：Knife 绑着 3

        if (itemInventory.ContainsKey("Aspirin"))  // 先确认Aspirin存不存在
        {
            itemInventory["Aspirin"] = 3;          // 存在才改，不然会报错
        }

        itemInventory.Remove("Antidote");  // 把Antidote那整行删掉

        Debug.Log(itemInventory.Count);    // 现在表格里有几行 → 3

    




📍7.循环语句 Iteration Statements
    //循环结束让一段代码重复执行，不用手动写很多遍




FOR Loop
    //在你知道要循环几次的时候用
        // for loop 格式：
        // for (初始值; 条件; 每次循环后做什么)

        for (int i = 0; i < 5; i++)
        //   ↑ 第1格    ↑ 第2格  ↑ 第3格
        //   int i = 0     → 创建一个变量i，从0开始
        //   i < 5         → 每次循环前检查：i还小于5吗？是就继续，否就停
        //   i++           → 每次执行完{}里的代码之后，i加1
        {
            Debug.Log($"这是第{i}次");   // 每次循环执行这行
        }

        // 完整执行过程：
        // i=0 → 0<5? ✅ → 打印"这是第0次" → i变成1
        // i=1 → 1<5? ✅ → 打印"这是第1次" → i变成2
        // i=2 → 2<5? ✅ → 打印"这是第2次" → i变成3
        // i=3 → 3<5? ✅ → 打印"这是第3次" → i变成4
        // i=4 → 4<5? ✅ → 打印"这是第4次" → i变成5
        // i=5 → 5<5? ❌ → 停止

        // 输出：
        // 这是第0次
        // 这是第1次
        // 这是第2次
        // 这是第3次
        // 这是第4次

        // i++ = 每次加1（最常用）
        // i-- = 每次减1（倒着数时用）






遍历 Iterate / Loop through
    //遍历 Iterate = 把数组或列表里每一个元素都看一遍，一个都不跳过。


            int[] scores = { 100, 85, 92 };
            //  索引(Index): [0]  [1]  [2]

            for (int i = 0; i < scores.Length; i++)
            //                   ↑
            //   scores.Length = 3，所以条件是 i < 3
            //   i 会经过 0, 1, 2 → 刚好是数组全部的索引
            {
                Debug.Log($"Score {i}: {scores[i]}");
                //                 ↑        ↑
                //                 i        用 i 当索引去取值
                //
                // i=0 → scores[0] → "Score 0: 100"
                // i=1 → scores[1] → "Score 1: 85"
                // i=2 → scores[2] → "Score 2: 92"
            }
                    // 为什么用 i 当索引？
                    // 你已经知道取值要写 scores[0]、scores[1]、scores[2]
                    // 但如果有100个，不可能手写100行。
                    // i 在循环里会自动变化：0 → 1 → 2
                    // 所以 scores[i] 就是帮你自动换数字的。
                    
                    //为什么是 i < scores.Length？
                    //scores.Length = 3，所以条件变成 i < 3
                    //i 会经过 0、1、2，停在 3
                    //数组的索引刚好也是 0、1、2
                    //所以这样写让 i 刚好走过每一个索引，一个不多一个不少.
    


遍历列表，并在循环里加判断 
Iterate through a List with an If statement inside

            List<string> partyMembers = new List<string>()
        {
            "Tanis the Thief",      // 索引0
            "Merlin the Wise",      // 索引1
            "Sterling the Knight"   // 索引2
        };

        int listLength = partyMembers.Count;  // Count(列表数量) = 3，先存进变量
                                            // 这样每次循环不用重新计算，更高效

        for (int i = 0; i < listLength; i++)  // i 经过 0, 1, 2
        {
            Debug.Log($"Index: {i} - {partyMembers[i]}");
            // i=0 → "Index: 0 - Tanis the Thief"
            // i=1 → "Index: 1 - Merlin the Wise"
            // i=2 → "Index: 2 - Sterling the Knight"

            if (partyMembers[i] == "Merlin the Wise")
            // 循环每转一圈，if 就检查一次，就是每次看一个人，顺便问"这个人是Merlin吗"
            // i=0 → "Tanis"    == "Merlin"? ❌ 跳过
            // i=1 → "Merlin"   == "Merlin"? ✅ 进入{}
            // i=2 → "Sterling" == "Merlin"? ❌ 跳过
            {
                Debug.Log("Glad you're here Merlin!");  // 只有 i=1 时打印这行
            }
        }







Foreach Loop 🥲//没懂

        //遍历集合时用，更简洁
        // 格式：
        // foreach (元素类型 局部变量名 in 集合)
        // {
        //     代码
        // }

        List<string> names = new List<string>()
        {
            "Alice", "Bob", "Charlie"
        };

        foreach (string name in names)   // 每次循环，name自动变成下一个元素
        {
            Debug.Log($"Hello, {name}!");
        }
        // 输出：
        // Hello, Alice!
        // Hello, Bob!
        // Hello, Charlie!


        // foreach遍历字典 - 需要用KeyValuePair
        Dictionary<string, int> items = new Dictionary<string, int>()
        {
            { "Potion", 5 },
            { "Antidote", 7 }
        };

        foreach (KeyValuePair<string, int> kvp in items)
        {
            // kvp.Key   = 键（"Potion"）
            // kvp.Value = 值（5）
            Debug.Log($"Item: {kvp.Key} - Amount: {kvp.Value}");
        }
        // 输出：
        // Item: Potion - Amount: 5
        // Item: Antidote - Amount: 7




While Loop
    //不确定要循环几次的时候使用
        // 格式：
        // while (条件Condition)
        // {
        //     代码
        //     必须有改变条件的语句！
        // }

        int playerLives = 3;

        while (playerLives > 0)      // 只要playerLives大于0就继续
        {
            Debug.Log("Still alive!");
            playerLives--;            // ⚠️ 必须改变条件！否则无限循环！
        }

        Debug.Log("Game Over!");

        // 执行过程：
        // playerLives=3 → 3>0? 是 → 打印 → lives变成2
        // playerLives=2 → 2>0? 是 → 打印 → lives变成1
        // playerLives=1 → 1>0? 是 → 打印 → lives变成0
        // playerLives=0 → 0>0? 否 → 停止循环
        // 打印 "Game Over!"

        // 输出：
        // Still alive!
        // Still alive!
        // Still alive!
        // Game Over!



