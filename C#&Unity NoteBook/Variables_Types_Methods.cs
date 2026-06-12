// This is the Notebook for C# learning in Unity. by Anqi Wu. Started on jun 5, 2026.
📒//章节
📍//知识点
🥲//没懂


📒//第一部分：Chapter 3：Diving into Variables, Types, and Methods


📍1. 代码格式规则
    //规则1: 每行代码必须用分号结尾，就像句子要有句号
    public string Name = "Anqi";
    //规则2:Method需要花括号{}
    public void MethodName()
    {
        //代码写这里面
    }


📍2. 什么是变量？
    //变量就像一个贴了标签的盒子，用来存储数据。
    //格式： 数据类型 变量名 = 值；
    int currentAge = 22; //整数盒子
    float pi = 3.14f; //小数盒子（要加f后缀）
    String FirstName = "Anqi"; //文本盒子
    bool IsStudent = true; //是/否盒子


📍3. 三种声明变量的方式 3 ways to declare variables:
    //方式1:有类型的值（最常用的）
    int CurrentAge = 22;
    //方式2:只有类型，之后再赋值
    //没有赋值的时候，数字默认是0
    int CurrentAge;
    CurrentAge = 22; //之后再给它值
    //方式3:让C#自动判断类型（不推荐常用）
    var CurrentAge = 22; // C#自动知道这个是int


📍4.访问修饰符 — 谁能看到这个变量？
    //访问修饰符就像变量的"保密级别"
    // public = 公开的，Unity的Inspector里能看到和修改
    public int Score = 100;

    // private = 私有的，只有这个脚本自己能用
    private int Health = 50;
    // 没写修饰符，默认也是private
    int Health = 50;    // 这两行效果一样

//记忆方法：
    //public -> inspector里能看到这个变量->方便在unity里调整
    //private -> Inspector 里没有这个变量->保护内部数据


📍5.调试代码————Debug.log()
    //Debug.Log() 就是让Unity在控制台打印消息，帮你看代码在做什么
    // 方式1：打印简单文字
    Debug.Log("Hello World");

    // 方式2：打印变量的值
    Debug.Log(CurrentAge);

    // 方式3：LogFormat - 用{0}{1}作为变量的占位符
    Debug.LogFormat("My name is {0} and I am {1} years old", 
                    FirstName, CurrentAge);
    // 输出：My name is Alex and I am 30 years old

    // 方式4：字符串插值（最推荐&简洁）
    // 在引号前加$，变量直接放在{}里
    Debug.Log($"My name is {FirstName} and I am {CurrentAge} years old");
    // 输出一样，但写法更简洁


📍6.变量命名规则
    // 推荐：Pascal Case（每个单词首字母大写）
    public int MaxPlayerHealth = 100;
    public string FirstName = "Alex";

    // 不推荐：名字没有意义
    public int h = 100;          // h是什么？
    public int mxplrhlth = 100;  // 完全看不懂


📍🥲🥲7.变量作用域 —————— 变量在哪里能被使用？（🥲没懂-需要提问）
//变量在哪个{ }里创建，就只能在那个{ }里使用。
    public class LearningCurve : MonoBehaviour
{
    // 类作用域 - 整个脚本都能用
    public string CharacterClass = "Ranger";
    
    void Start()
    {
        // 局部作用域 - 只在Start()里能用
        int CharacterHealth = 100;
        
        Debug.Log(CharacterClass);    // 可以用
        Debug.Log(CharacterHealth);   // 可以用
    }
    
    void AnotherMethod()
    {
        Debug.Log(CharacterClass);    // 可以用（类作用域）
        Debug.Log(CharacterHealth);   // 报错！只在Start()里有效
    }
}


📍8.运算符operational character
    // 基本数学运算
    int a = 10;
    int b = 3;

    Debug.Log(a + b);   // 13 加
    Debug.Log(a - b);   // 7  减
    Debug.Log(a * b);   // 30 乘
    Debug.Log(a / b);   // 3  除

    // 快捷赋值运算符
    int x = 10;
    x += 5;    // x = x + 5 → x现在是15
    x -= 3;    // x = x - 3 → x现在是12
    x *= 2;    // x = x * 2 → x现在是24
    x /= 4;    // x = x / 4 → x现在是6

    // 字符串可以用+拼接
    string fullName = "Anqi" + " " + "Wu";
    // fullName = "Anqi Wu"


📍🥲🥲 9. 类型转换( type conversion/Type Casting)(🥲没懂，需要提问)
    // 隐式转换(Implicit Conversion )- 自动发生，安全
    int myInt = 3;
    float myFloat = myInt;   // int自动变成float
    // myFloat = 3.0

    // 显式转换(Explicit Conversion / Explicit Cast) - 手动转换，可能丢失数据
    // 格式：(目标类型)值
    int result = (int)3.14;  // 强制把小数变整数
    // result = 3（小数部分没了！）


📍10.方法 (Methods) — 存储和执行指令
    //方法就像一个‘自动贩卖机’，你可以选择要不要投币（参数），它做完事之后可以选择要不要给你东西（返回值）。

    //格式
    访问修饰符  返回类型  方法名  (参数)
        {
            代码
        }


    //三种常见情况

        //1. 无参数、无返回值
            // 你按一个按钮，它播放一段音乐，完事。 不需要投币，也不给你任何东西，就只是做了一件事。

            // void = 不交还任何东西
            public void SayHello()
            {
                Debug.Log("Hello!");
            }

            // 调用：
            SayHello();

        //2. 有参数、无返回值
            //你投币（参数），它根据你投的东西做事，但不给你实物回来。参数就是你"传进去"的信息，方法拿这个信息来用。

            public void GenerateCharacter(string name, int level)
            {
                Debug.Log($"Character: {name} - Level: {level}");
            }

            // 调用：
            GenerateCharacter("Spike", 32);
            // 你告诉它：名字是Spike，等级是32
            // 它用这两个信息打印出来，但没有给你任何东西回来


        //3. 有参数、有返回值
            //你投币（参数），它吐出一罐饮料（返回值）给你。 
            //  `return` = 把结果交还给调用它的人。
            // int = 这个方法执行完会交还一个整数给你
            public int AddFive(int number)
            {
                return number + 5;
            }

            // 调用：
            int result = AddFive(10);   // 传进去10，它交还15
            Debug.Log(result);           // 打印 15


    //在 Start() 里调用方法
        void Start()
        {
            SayHello();                      // 调用无参数方法
            GenerateCharacter("Spike", 32);  // 调用有参数方法

            int result = AddFive(10);        // 用变量接住返回值
            Debug.Log(result);               // 打印 15
        }

    //方法的执行顺序
        void Start()
        {
            Debug.Log("1. Start 开始");   // 第1个打印
            SayHello();                    // 跳到 SayHello 执行
            Debug.Log("3. Start 结束");   // 第3个打印
        }

        public void SayHello()
        {
            Debug.Log("2. 在 SayHello 里");  // 第2个打印
        }

        // 输出顺序：
        // 1. Start 开始
        // 2. 在 SayHello 里
        // 3. Start 结束

    //关键词总结
        | 中文 | 英文 |
        |------|------|
        | 方法 | Method |
        | 访问修饰符 | Access Modifier |
        | 返回类型 | Return Type |
        | 参数 | Parameter |
        | 返回值 | Return Value |
        | 无返回值 | void |
        | 调用方法 | Call a Method |

    //记忆口诀：
        //参数= 你传给方法的信息（投币）
        //返回值= 方法执行完交还给你的结果（吐出饮料）
        //void= 不交还任何东西


📍11.Unity的两个特殊方法
    // Start() - 游戏开始时只执行一次
    // 用来：设置初始值，做准备工作
    void Start()
    {
        Debug.Log("游戏开始！");
    }

    // Update() - 每帧执行一次（大约每秒60次）
    // 用来：检测按键，持续更新的逻辑
    void Update()
    {
        // 这里的代码每帧都运行
    }