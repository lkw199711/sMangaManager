using System;
using System.Collections.Generic;
using System.Data.SqlClient;                // 数据库连接
using System.Diagnostics;
using System.Drawing;
using System.IO;                            // 读写文件
using System.Runtime.InteropServices;       // 获取时间
using System.Security.Cryptography;         // MD5
using System.Text;                          // 文本操作
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace lkw
{
    // 数据库链接
    public class Connect_sql
    {
        // 初始化连接对象
        SqlConnection connectObj;

        /*数据库连接(构造方法)
         返回数据库连接对象
             */
        public Connect_sql(string domain, string database, string username, string password)
        {
            //构造连接字符串
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = domain;
            scsb.InitialCatalog = database;
            scsb.UserID = username;
            scsb.Password = password;

            //创建连接 参数为连接字符串
            SqlConnection sqlConn = new SqlConnection(scsb.ToString());

            //打开连接
            try
            {
                sqlConn.Open();
            }
            catch
            {
                Console.WriteLine("服务器连接错误，请联系管理员！", "连接失败");
                System.Environment.Exit(0);
            }
            connectObj = sqlConn;
        }
        // 执行构造方法
        Connect_sql() { }

        /*sql语句执行
         参数 sql命令
             */
        public int doSql(string command)
        {
            /*创建用于执行sql语句的对象
            参数1：sql语句字符串,参数2：已打开的数据连接对象
                */
            SqlCommand sqlComm = new SqlCommand(command, connectObj);

            // 受影响的行数
            return sqlComm.ExecuteNonQuery();
        }

        public SqlDataReader select(string command)
        {
            SqlCommand sqlComm = new SqlCommand(command, connectObj);

            //接收查询到的sql数据
            SqlDataReader reader = sqlComm.ExecuteReader();

            //返回结果对象
            //if (reader == null || reader.HasRows == false) return null;
            return reader;

        }
    }
    // 自用方法封装
    public class Lkw
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(int hWnd);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]//指定坐标处窗体句柄
        public static extern int WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(int hWnd, ref RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
            public int Width;
            public int Height;
        }

        //输出
        public void log(String str)
        {
            Console.WriteLine(str);
        }

        public void msbox(string str)
        {
            MessageBox.Show(str);
        }

        /*执行cmd命令
         参数 cmd命令字符串
         返回 cmd输出信息
             */
        public string cmd(string str)
        {

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();


            // 返回cmd输出
            return output;
        }

        /// <summary>
        /// md5编码
        /// </summary>
        /// <param name="str">需要编码的字符串</param>
        /// <returns>返回32位字符串</returns>
        public static string MD5_code(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
                                   // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string base64_decode(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string base64_encode(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

            /*检验文件是否存在
             参数 文件路径
                 */
            public bool isFile(string route)
        {
            return File.Exists(route);
        }

        /*检验目录是否存在
         参数 目录路径
             */
        public bool isPath(string route)
        {
            return Directory.Exists(route);
        }

        //新建目录
        public bool makedir(string route)
        {
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
                return true;
            }
            else
                return false;     
        }
        //输入字符串
        public void EnterStr(string Str)
        {
            foreach (char c in Str)
            {
                SendKeys.SendWait("{" + c.ToString() + "}");
            }
        }
        //根据窗口名获取指定窗口
        public RECT FindWindows(string name, int x = 1920/2,int y = 1080/2)
        {
            int thisre;
            if (name == "") 
                thisre = WindowFromPoint(x, y);
            else
                thisre = FindWindow(null, name);

            StringBuilder title = new StringBuilder(256);
            GetWindowText(thisre, title, title.Capacity);

            RECT rc = new RECT();
            GetWindowRect(thisre, ref rc);
            rc.Width = rc.Right - rc.Left; //窗口的宽度
            rc.Height = rc.Bottom - rc.Top; //窗口的高度

            return rc;
        }

        /*隐藏窗口
         参数 窗口名
             */
        public void HideWindow(string name = "",int i = 0)
        {
            ShowWindow(FindWindow(null, name), i);
        }
        //
        public void totop(string name)
        {
            SetForegroundWindow(FindWindow(null, name));
        }

        // 获取现在时间
        public string getTime(string type="")
        {
            DateTime currentTime = DateTime.Now;
            int 年 = currentTime.Year;
            int 月 = currentTime.Month;
            int 日 = currentTime.Day;
            int 时 = currentTime.Hour;
            int 分 = currentTime.Minute;
            int 秒 = currentTime.Second;
            int 毫秒 = currentTime.Millisecond;

            switch (type)
            {
                case "FileName":
                    return 年 + "年" + 月 + "月" + 日 + "日" + 时 + "时" + 分 + "分" + 秒 + "秒";
                case "FileName-Date":
                    return 年 + "年" + 月 + "月" + 日 + "日";
                case "FileName-Time":
                    return 时 + "时" + 分 + "分" + 秒 + "秒";
                case "Date":
                    return System.DateTime.Now.ToShortDateString();
                case "Time":
                    return System.DateTime.Now.ToLongTimeString();
                default:
                    return (System.DateTime.Now.ToShortDateString() + "-" + System.DateTime.Now.ToLongTimeString());
            }
                

        }
        // 关闭程序
        public void exit() { System.Environment.Exit(0); }
        /*开启进程
         参数 程序路径，带exe
         返回 进程对象，用于关闭
             */
        public Process start(string route,string parameter="")
        {
            Process pr = new Process();//声明一个进程类对象
            pr.StartInfo.FileName = route;//指定运行的程序，我的QQ的物理路径。
            pr.StartInfo.Arguments = parameter;//"我是由控制台程序传过来的参数，如果传多个参数用空格隔开" + " 第二个参数";
            pr.Start();//运行QQ

            return pr;
        }
        /* 关闭进程
         参数 进程名，不带exe
             */
        public void kill(string name)
        {
            Process[] proc = Process.GetProcessesByName(name);//创建一个进程数组，把与此进程相关的资源关联。
            for (int i = 0; i < proc.Length; i++)
            {
                proc[i].Kill();  //逐个结束进程.
            }
        }
        //判断进程是否存在
        public bool IsExist(string name)
        {
            Process[] proc = Process.GetProcessesByName(name);//创建一个进程数组，把与此进程相关的资源关联。
            for (int i = 0; i < proc.Length; i++)
            {
                return true;
            }
            return false;
        }
        // 延时（毫秒）
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                //Application.DoEvents();//可执行某无聊的操作
            }
        }
        //延时，沉睡（毫秒）
        public void sleep(int Second = 10)
        {
            Thread.Sleep(Second);
        }
        // 随机数（最低，最高）
        public int random(int min,int max)
        {
            Random ran = new Random();

            return ran.Next(min,max);
        }
        //截图
        public Bitmap ps(int startX = 0, int startY = 0, int iWidth = 1920, int iHeight = 1080)
        {

            //屏幕高

            //按照屏幕宽高创建位图
            Bitmap img = new Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象
            Graphics gc = Graphics.FromImage(img);
            //抓屏并拷贝到myimage里
            gc.CopyFromScreen(new Point(startX, startY), new Point(0, 0), new Size(iWidth, iHeight));
            //this.BackgroundImage = img;
            //保存位图
            //img.Save(@"C:\" + Guid.NewGuid().ToString() + ".jpg");
            //返回
            return img;
        }
        //新建线程
        public Thread NewWork(ThreadStart fn)
        {
            Thread oGetArgThread = new Thread(fn);
            oGetArgThread.IsBackground = true;
            oGetArgThread.Start();

            return oGetArgThread;
        }
        //写入文本
        public void WriteLine(string route,string str)
        {
            //以流的形式写入，在using 语句块中使用，保证块结束时 StreamWriter 立即被关闭
            //StreamWriter第二个参数可选，为false覆盖文件，为true追加到文件
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(route, true))
            {
                //file.Write(line);   //追加，不换行
                file.WriteLine(str);   // 追加，换行   
            }
        }

        internal static void SetWindowPos(int doaHandle, object hWND_TOPMOST)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 正则检查表单
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="regText"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool check_text(string text, string title, string regText = "", string errorText = "只能由大小写26字母，0-9组成，不能包含其他字符！")
        {
            if (text.Length < 2)
            {
                MessageBox.Show(title + "的长度不得少于2", title + "不合规");
                return false;
            }
            if (text.Length > 20)
            {
                MessageBox.Show(title + "的长度不得多于20", title + "不合规");
                return false;
            }
            // 如果不需要正则匹配，则直接返回
            if (regText == "") { return true; }

            Regex reg = new Regex(regText);
            MatchCollection result = reg.Matches(text);

            foreach (Match m in result)
            {
                //输出结果
                MessageBox.Show(title + errorText, title + "不合规");
                return false;
            }
            return true;
        }
    }

    //热键
    public class SystemHotKey
    {
        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 辅助键名称。
        /// Alt, Ctrl, Shift, WindowsKey
        /// </summary>
        [Flags()]
        public enum KeyModifiers { None = 0, Alt = 1, Ctrl = 2, Shift = 4, WindowsKey = 8 }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public static void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (!RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 1409)
                {
                    MessageBox.Show("热键被占用 ！");
                }
                else
                {
                    MessageBox.Show("注册热键失败！错误代码：" + errorCode);
                }
            }
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public static void UnRegHotKey(IntPtr hwnd, int hotKeyId)
        {
            //注销指定的热键
            UnregisterHotKey(hwnd, hotKeyId);
        }

    }

    // ini文件读写
    public class IniFiles
    {
        public string inipath;

        //声明API函数

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="INIPath">文件路径</param> 
        public IniFiles(string INIPath)
        {
            inipath = INIPath;
        }

        public IniFiles() { }

        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string read(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
    }

    // 脚本制作
    public class KeyScript
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        static extern int mouse_event(int flags, int dx, int dy, uint data, int extraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        //颜色常量
        Color black = Color.FromArgb(255, 0, 0, 0);
        Color white = Color.FromArgb(255, 255, 255, 255);

        // 引入自建类库
        Lkw lkw = new Lkw();
        //
        public class Coord
        {
            public int x, y, num;
            public bool exist;
            public Color color;
            public List<Coord> points = new List<Coord>();
            public Coord(int X = -1, int Y = -1, Color Color = new Color(), int Num = 0, bool Exist = false)
            {
                x = X;
                y = Y;
                color = Color;
                num = Num;
                exist = Exist;
            }

        }
        //光标移动（x,y）
        public void MoveTo(int X, int Y, int miss = 5, int step = 4, int interval = 1)
        {
            //声明差异量，动量
            int changeX, changeY, specific, i = 0;
            //设置误差
            double differenceX, differenceY;
            //设置速度
            //判断小值
            bool xORy = true;
            //随机化处理xy坐标
            X = X + lkw.random(-miss, miss);
            Y = Y + lkw.random(-miss, miss);
            //获取位置总差值
            differenceX = Math.Abs(Cursor.Position.X - X);
            differenceY = Math.Abs(Cursor.Position.Y - Y);
            //获取xy位移比值
            if (differenceX > differenceY)
                specific = (int)Math.Round(differenceX / differenceY);
            else
            {
                specific = (int)Math.Round(differenceY / differenceX);
                xORy = false;
            }

            while (Cursor.Position.X != X || Cursor.Position.Y != Y)
            {
                //次数递增
                i++;
                //最后小距离移动，将步长改为1
                if (Math.Abs(Cursor.Position.X - X) < step || Math.Abs(Cursor.Position.Y - Y) < step) step = 1;
                //判断差值正负
                changeX = calculatePlusMinus(X - Cursor.Position.X, step);
                changeY = calculatePlusMinus(Y - Cursor.Position.Y, step);
                //判断是否为指定次数，小值移动
                if (i % specific != 0)
                {
                    if (xORy)
                        changeY = 0;
                    else
                        changeX = 0;
                }
                //执行光标移动
                SetCursorPos(Cursor.Position.X + changeX, Cursor.Position.Y + changeY);
                //停止一毫秒
                Thread.Sleep(interval);
            }
        }
        //点击
        public void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        //移动并且点击
        public void ToClick(int x, int y, int miss = 5, int step = 4)
        {
            MoveTo(x, y, miss, step);
            Thread.Sleep(10);
            Click();
        }
        //鼠标按下
        public void mouse_click(int Second = 20)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(Second);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);     
        }
        //判断正负
        public int calculatePlusMinus(int standard, int num)
        {
            if (standard > 0)
                return num;
            else if (standard < 0)
                return -num;
            else
                return 0;
        }
        //全屏截图
        public Image ScreenPs()
        {
            int iWidth = Screen.PrimaryScreen.Bounds.Width;
            //屏幕高
            int iHeight = Screen.PrimaryScreen.Bounds.Height;
            //按照屏幕宽高创建位图
            Image img = new Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象
            Graphics gc = Graphics.FromImage(img);
            //抓屏并拷贝到myimage里
            gc.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            //保存位图
            return img;
        }
        //窗口截图
        public Bitmap WindowPs(Lkw.RECT window)
        {
            return ps(window.Left, window.Top, window.Width, window.Height);
        }
        //截图
        public Bitmap ps(int startX = 0, int startY = 0, int iWidth = 1920, int iHeight = 1080)
        {

            //屏幕高

            //按照屏幕宽高创建位图
            Bitmap img = new Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象
            Graphics gc = Graphics.FromImage(img);
            //抓屏并拷贝到myimage里
            gc.CopyFromScreen(new Point(startX, startY), new Point(0, 0), new Size(iWidth, iHeight));
            //this.BackgroundImage = img;
            //保存位图
            //img.Save(@"C:\" + Guid.NewGuid().ToString() + ".jpg");
            //返回
            return img;
        }
        //局部找图
        public bool PartFind(Bitmap bigPic, Bitmap samllPic, int startX, int startY, int samllWidth, int samllHeight, int ErrorColor = 0, int errPoint = 0)
        {
            int ErrorPoint = 0;
            for (int y = 0; y < samllHeight; y++)
            {
                for (int x = 0; x < samllWidth; x++)
                {
                    if (!CompareColor(samllPic.GetPixel(x, y), bigPic.GetPixel(startX + x, startY + y), ErrorColor))
                    {
                        ErrorPoint++;
                        if (ErrorPoint > errPoint)
                            return false;
                    }
                }
            }
            return true;

        }
        //局域找字
        public bool PartGraph(Bitmap bigPic, List<Coord> Points, Coord befor, int ErrorColor, int ErrorPoint)
        {
            int width = bigPic.Width, height = bigPic.Height;
            int length = Points.Count;
            int changeX, changeY;
            int ErrPoints = 0;
            for (int i = 1; i < length; i++)
            {
                changeX = Points[i].x - Points[i - 1].x;
                changeY = Points[i].y - Points[i - 1].y;
                //检验坐标点是否符合图片大小范围
                if (befor.x + changeX < width && befor.x + changeX > 0 && befor.y + changeY < height && befor.y + changeY > 0)
                {
                    if (!CompareColor(bigPic.GetPixel(befor.x + changeX, befor.y + changeY), Points[i].color, ErrorColor))
                    {
                        ErrPoints++;
                        if (ErrPoints > ErrorPoint)
                            return false;
                    }
                }
                else { return false; }
                    
                befor = new Coord(befor.x + changeX, befor.y + changeY);
            }
            return true;
        }
        //比较颜色
        public bool CompareColor(Color A, Color B, int err = 0)
        {
            if (Math.Abs(A.R - B.R) > err || Math.Abs(A.G - B.G) > err || Math.Abs(A.B - B.B) > err)
                return false;
            else
                return true;
        }
        //找到第所有正常点（找字子方法）
        public List<Coord> FindNormalPoint(Bitmap Pic)
        {
            List<Coord> points = new List<Coord>();
            Color first = new Color();
            Color BackGround = Pic.GetPixel(0, 0);
            int width = Pic.Width;
            int height = Pic.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    first = Pic.GetPixel(x, y);
                    if (first != BackGround)
                    {
                        points.Add(new Coord(x, y, first));
                    }
                }
            }
            return points;
        }
        //找图
        /*
         从一张大图中找到小图的位置，返回包含小图坐标的对象
         bigPic 位图文件
         smallPic 位图文件
         ErrorColor 颜色容差（0 - 255）
         ErrorPoint 点数容差（0 - 小图长*宽）
             */
        public Coord FindPic(Bitmap bigPic, Bitmap smallPic, int ErrorColor = 0, int ErrorPoint = 0, bool more = false)
        {
            Coord Coord = new Coord();
            int bigPicWidth = bigPic.Width;
            int bigPicHeight = bigPic.Height;
            int smallPicWidth = smallPic.Width;
            int smallPicHeight = smallPic.Height;
            int FindWidth = bigPicWidth - smallPicWidth;
            int FindHeight = bigPicHeight - smallPicHeight;

            int m = 0, n = 0;
            Color BigColor, SamllColor = smallPic.GetPixel(m, n);


            for (int i = 0; i < FindHeight; i++)
            {
                for (int j = 0; j < FindWidth; j++)
                {
                    BigColor = bigPic.GetPixel(j, i);
                    //找到第一个点，进行局部查找
                    if (CompareColor(BigColor, SamllColor, ErrorColor) && PartFind(bigPic, smallPic, j, i, smallPicWidth, smallPicHeight, ErrorColor, ErrorPoint))
                    {
                        Coord.x = j;
                        Coord.y = i;
                        Coord.num++;
                        Coord.exist = true;
                        if (!more) return Coord;
                    }
                }
            }
            return Coord;
        }
        //找色
        public Coord FindColor(Bitmap bigPic, Color color, int ErrorColor = 0, bool more = false)
        {
            Coord Coord = new Coord();
            int bigPicWidth = bigPic.Width;
            int bigPicHeight = bigPic.Height;
            for (int y = 0; y < bigPicHeight; y++)
            {
                for (int x = 0; y < bigPicWidth; x++)
                {
                    if (CompareColor(bigPic.GetPixel(x, y), color, ErrorColor))
                    {
                        Coord.x = x;
                        Coord.y = y;
                        Coord.num++;
                        Coord.exist = true;

                        if (more)
                        {
                            Coord.points.Add(new Coord(x, y, color));
                        }
                        else
                        {
                            return Coord;
                        }
                    }
                }
            }
            return Coord;
        }
        //找字
        public Coord FindGraph(Bitmap bigPic, Bitmap smallPic, int ErrorColor = 0, int ErrorPoint = 0, bool more = false)
        {
            List<Coord> Points = FindNormalPoint(smallPic);
            Color color;
            int bigPicWidth = bigPic.Width;
            int bigPicHeight = bigPic.Height;
            int smallPicWidth = smallPic.Width;
            int smallPicHeight = smallPic.Height;
            int FindWidth = bigPicWidth - smallPicWidth;
            int FindHeight = bigPicHeight - smallPicHeight;

            for (int n = 0; n < bigPicHeight; n++)
            {
                for (int m = 0; m < bigPicWidth; m++)
                {
                    color = bigPic.GetPixel(m, n);
                    if (CompareColor(color, Points[0].color, ErrorColor) && PartGraph(bigPic, Points, new Coord(m, n), ErrorColor, ErrorPoint))
                    {
                        return new Coord(m, n, color, 1, true);
                    }
                }
            }

            return new Coord();
        }
        //局部找图
        public Coord PartFindPic(Bitmap Pic, int startX, int startY, int width, int height, int ErrorColor = 10, int ErrorPoint = 10, bool more = false)
        {
            Coord Coord = FindPic(ps(startX, startY, width, height), Pic, ErrorColor, ErrorPoint, more);
            if (Coord.exist)
            {
                Coord.x += startX;
                Coord.y += startY;
            }
            return Coord;
        }
        //局部找色
        public Coord PartFindColor(Color color, int startX, int startY, int width, int height, int ErrorColor = 0, bool more = false)
        {
            Coord Coord = FindColor(ps(startX, startY, width, height), color, ErrorColor, more);
            if (Coord.exist)
            {
                Coord.x += startX;
                Coord.y += startY;
            }
            return Coord;
        }
        //局部找字
        public Coord PartFindGraph(Bitmap Pic, int startX, int startY, int width, int height, int ErrorColor = 5, int ErrorPoint = 5, bool more = false)
        {
            Coord Coord = FindGraph(ps(startX, startY, width, height), Pic, ErrorColor, ErrorPoint, more);
            if (Coord.exist)
            {
                Coord.x += startX;
                Coord.y += startY;
            }
            return Coord;
        }
        //找字图，颜色处理
        public Bitmap makePic(Bitmap img, Color NormColor)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (img.GetPixel(x, y) == NormColor)
                        img.SetPixel(x, y, black);
                }
            }
            return img;
        }
        //局域找数
        public int PartNum(Bitmap bigPic, List<Coord> Points, Coord befor, int ErrorPoint = 3000)
        {
            int width = bigPic.Width, height = bigPic.Height;
            int length = Points.Count;
            int changeX, changeY;
            int ErrPoints = 0;
            for (int i = 1; i < length; i++)
            {
                changeX = Points[i].x - Points[i - 1].x;
                changeY = Points[i].y - Points[i - 1].y;
                //检验坐标点是否符合图片大小范围
                if (befor.x + changeX < width && befor.x + changeX > 0 && befor.y + changeY < height && befor.y + changeY > 0)
                {
                    if (bigPic.GetPixel(befor.x + changeX, befor.y + changeY) != black)
                    {
                        ErrPoints++;
                        if (ErrPoints > ErrorPoint)
                            return 3000;
                    }
                }
                else { ErrPoints++; }

                befor = new Coord(befor.x + changeX, befor.y + changeY);
            }
            return ErrPoints;
        }

        /*以下为找数方法，传入图片进行识别*/
        //多线程找数
        public string Distinguish(Bitmap pic)
        {
            int[] str = new int[] { 10,10,10,10,10,10};
            //处理验证码原图
            List<Bitmap> imgs = Division(pic);

            //如果分割处理数量不对，则返回错误值，再次查找
            if (imgs.Count != 6) return "000000";

            /*
            for (int i = 0, length = imgs.Count; i < length; i++)
            {
                str[i] = FindNum(new Bitmap(imgs[i]), sample);          
            }
            */
            lkw.NewWork(new ThreadStart(() =>
            {
                str[0] = FindNum(new Bitmap(imgs[0]), GetSample());
            }));
            lkw.NewWork(new ThreadStart(() =>
            {
                str[1] = FindNum(new Bitmap(imgs[1]), GetSample());
            }));
            lkw.NewWork(new ThreadStart(() =>
            {
                str[2] = FindNum(new Bitmap(imgs[2]), GetSample());
            }));
            lkw.NewWork(new ThreadStart(() =>
            {
                str[3] = FindNum(new Bitmap(imgs[3]), GetSample());
            }));
            lkw.NewWork(new ThreadStart(() =>
            {
                str[4] = FindNum(new Bitmap(imgs[4]), GetSample());
            }));
            lkw.NewWork(new ThreadStart(() =>
            {
                str[5] = FindNum(new Bitmap(imgs[5]), GetSample());
            }));
            
            while (!IsZero(str)) { lkw.sleep(1000); }

            string a="";
            for (int i = 0; i < str.Length; i++)
                a += str[i];
            Console.WriteLine(a);
            return a;
        }
        //分割数字
        public List<Bitmap> Division(Bitmap pic)
        {
            List<Bitmap> a = new List<Bitmap>();
            int width = pic.Width;
            int height = pic.Height;
            int start = -1, end = 1, num = 0, t = 0;

            while (end != width - 1)
            {
                start = FindColorCol(pic, t, black);

                if (start == width - 1) return a;

                end = FindNotColorCol(pic, start, black);

                Bitmap img = cut(pic, start, 0, end - start, height);

                //img.Save(@"E:\" + num++.ToString() + ".png");

                a.Add(img);

                t = end;
            }

            return a;

        }
        //找数
        public int FindNum(Bitmap bigPic, List<Bitmap> smallPic)
        {
            List<Bitmap> bigPics = new List<Bitmap>();
            int Result = 0, err = 0, ResultErr = 3000,r=0;
            Bitmap vertical = bigPic;
            //建立90个旋转样本
            for (int i = -45; i < 45; i++)
            {
                bigPics.Add(ReSize((Rotate(bigPic, i))));
            }

            for (int j = 0,l = bigPics.Count; j < l; j+=5)
            {
                for (int i = 0, length = smallPic.Count; i < length; i++)
                {
                    //List<Coord> Points1 = FindBlackPoint(smallPic[i]);
                    //获取坐标系数组
                    List<Coord> Points = FindBlackPoint(bigPics[j]);
                    //获取待识别图像宽度，高度*/
                    int smallPicWidth = smallPic[i].Width;
                    int smallPicHeight = smallPic[i].Height;

                    for (int n = 0; n < smallPicHeight; n++)
                    {
                        for (int m = 0; m < smallPicWidth; m++)
                        {
                            if (smallPic[i].GetPixel(m, n) == Points[0].color)
                            {
                                err = PartNum(smallPic[i], Points, new Coord(m, n), ResultErr);
                                //找到最小值
                                if (err < ResultErr) {Result = i;ResultErr = err; r = j; vertical = bigPics[j]; }                                   
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("原图与" + (Result%10).ToString() + "的误差为" + ResultErr.ToString()+"，角度为"+(r-45).ToString()+"度°");
            return Features(TwoPoles(vertical), Result % 10);
        }
        //找到图形开始位置
        public int FindColorCol(Bitmap pic, int start, Color color)
        {
            int width = pic.Width;
            int height = pic.Height;

            for (int x = start; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pic.GetPixel(x, y) == color)
                        return x;
                }
            }

            return width-1;
        }
        //找到图形开始位置-列
        public int FindColorRow(Bitmap pic, int start, Color color)
        {
            int width = pic.Width;
            int height = pic.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = start; x < width; x++)
                {
                    if (pic.GetPixel(x, y) == color)
                        return y;
                }
            }

            return 0;
        }
        //找到图形开始位置-倒序
        public int FindColorColDesc(Bitmap pic, Color color)
        {
            int width = pic.Width;
            int height = pic.Height;

            for (int x = width - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pic.GetPixel(x, y) == black)
                        return x;
                }
            }

            return 0;
        }
        //找到图形开始位置-列-倒叙
        public int FindColorRowDesc(Bitmap pic, Color color)
        {
            int width = pic.Width;
            int height = pic.Height;

            for (int y = height-1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    if (pic.GetPixel(x, y) == color)
                        return y;
                }
            }

            return 0;
        }
        //找到图形结束为止
        public int FindNotColorCol(Bitmap pic, int start, Color color)
        {
            int width = pic.Width;
            int height = pic.Height;

            for (int x = start; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pic.GetPixel(x, y) == color) break;
                    if (y == height - 1) return x;
                }
            }

            return width-1;
        }
        //找数（单字）
        public int FindNum1(Bitmap bigPic, List<Bitmap> smallPic)
        {
            int Result = 0, err = 0, ResultErr = 3000;

                //获取待识别图像宽度，高度
                int bigPicWidth = bigPic.Width;
                int bigPicHeight = bigPic.Height;

                for (int i = 0, length = smallPic.Count; i < length; i++)
                {
                    //获取坐标系数组
                    List<Coord> Points = FindBlackPoint(bigPic);

                    for (int n = 0; n < smallPic[i].Height; n++)
                    {
                        for (int m = 0; m < smallPic[i].Width; m++)
                        {
                            if (smallPic[i].GetPixel(m, n) == Points[0].color)
                            {
                                err = PartNum(smallPic[i], Points, new Coord(m, n), ResultErr);
                                //找到最小值
                                if (err < ResultErr) { Result = i; ResultErr = err; }
                            }
                        }
                    }
                }

            Console.WriteLine("原图与" + Result.ToString() + "的误差为" + ResultErr.ToString());
            return Result;
        }
        //找到第所有黑点（找数数方法）
        public List<Coord> FindBlackPoint(Bitmap Pic)
        {
            List<Coord> points = new List<Coord>();
            Color first = new Color();
            int width = Pic.Width;
            int height = Pic.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    first = Pic.GetPixel(x, y);
                    if (first == black)
                    {
                        points.Add(new Coord(x, y, first));
                    }
                }
            }
            return points;
        }
        //图片颜色两极化（黑白）
        public Bitmap TwoPoles(Bitmap pic)
        {
            Color color;
            int width = pic.Width;
            int height = pic.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    color = pic.GetPixel(x, y);
                    if (color.A != 255 || color.G > 125 || color.B > 125 || color.R > 125)
                        pic.SetPixel(x, y, white);
                    else
                        pic.SetPixel(x, y, black);
                }
            }
            return pic;
        }
        //旋转图像
        public Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360; //弧度转换 
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高 
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图 
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量 
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致 
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移 
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换 
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save(“yuancd.jpg“, System.Drawing.Imaging.ImageFormat.Jpeg); 
            return dsImage;
        }
        //缩放图像
        public Bitmap ScaleImage(Bitmap pBmp, int pWidth, int pHeight)
        {
            try
            {
                Bitmap tmpBmp = new Bitmap(pWidth, pHeight);
                Graphics tmpG = Graphics.FromImage(tmpBmp);

                //tmpG.InterpolationMode = InterpolationMode.HighQualityBicubic;

                tmpG.DrawImage(pBmp,
                                           new Rectangle(0, 0, pWidth, pHeight),
                                           new Rectangle(0, 0, pBmp.Width, pBmp.Height),
                                           GraphicsUnit.Pixel);
                tmpG.Dispose();
                return tmpBmp;
            }
            catch
            {
                return null;
            }
        }
        //更改图像大小为标准尺寸
        public Bitmap ReSize(Bitmap img)
        {
            var points = FindBlackPoint(img);
            int change = points[points.Count - 1].y - points[0].y;

            var tow = (int)Math.Round(((float)img.Width / change * 44));
            var toh = (int)Math.Round((float)img.Height / change * 44);

            return ScaleImage(img, tow, toh);
        }
        //根据数字特征，识别数字
        public int Features(Bitmap img,int Result)
        {
            int width = img.Width;
            int height = img.Height;
            int Left=-1, Right=-1, Top=-1, Bottom=-1;
            //寻找左侧值
            Left = FindColorCol(img, 0, black);
            //寻找右侧值
            Right = FindColorColDesc(img, black);
            //寻找上侧值
            Top = FindColorRow(img, 0, black);
            //寻找下侧值
            Bottom = FindColorRowDesc(img, black);

            int Width = Right - Left;
            int Height = Bottom - Top;

            Color Center = img.GetPixel(Left + Width / 2, Top + Height / 2);
            Color CenterLeft = img.GetPixel(Left + Width / 2/2, Top + Height / 2);
            Color CenterLeftb = img.GetPixel(Left + Width / 2/2 -2, Bottom - Height / 2/2-2);
            Color CenterRight = img.GetPixel(Right - Width / 2/2, Top + Height / 2);
            /*
            Console.WriteLine("中心"+Center);
            Console.WriteLine("中心偏左"+CenterLeft);
            Console.WriteLine((Left + Width / 2 / 2 -3)+","+(Bottom - Height / 2 / 2 -3) +"中心偏左下"+CenterLeftb);
            Console.WriteLine("中心偏右"+CenterRight);
            Console.WriteLine(Result);
            */
            switch (Result)
            {
                case 8:
                    if (Center == white)
                        return 0;
                    else if (Center == black && CenterLeft == white && CenterRight == black)
                        return 3;
                    break;
                case 6:
                    if (CenterLeft == white && CenterRight == white)
                        return 0;
                    else if (CenterLeftb == white)
                        return 5;                  
                    break;
                case 1:
                    if (Width > 25)
                        return 7;
                    break;
                case 0:
                    if (Center == black && CenterLeft == white && CenterRight == black)
                        return 3;
                    break;
                default:
                    return Result;
            }

            return Result;
        }
        //裁切图片
        public Bitmap cut(Bitmap pic, int x, int y, int width, int height)
        {
            RectangleF cloneRect = new RectangleF(x, y, width, height);
            System.Drawing.Imaging.PixelFormat format = pic.PixelFormat;
            Bitmap cloneBitmap = pic.Clone(cloneRect, format);
            return cloneBitmap;
        }
        //查看数组是否完成
        public bool IsZero(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == 10) return false;

            return true;
        }
        //查看验证码两边是否切字
        public bool BothSides(Bitmap img)
        {
            //寻找最左侧是否有未完全图形
            for(int y = 0,height=img.Height; y < height; y++)
            {
                if (img.GetPixel(5, y) == black)
                    return false;
            }
            //寻找最右侧是否有未完全图形
            for (int y = 0,width=img.Width; y < img.Height; y++)
            {
                if (img.GetPixel(width-1, y) == black)
                    return false;
            }
            //没有未完全显示图形，返回真
            return true;
        }
        //查找图片最大有多少列连续的白色
        public int FindWhiteCol(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int start = -1, end = 1, row = 0, t = 0;

            while (start != width - 1)
            {
                start = FindNotColorCol(img, t, black);
                if (start == width - 1) return row;

                end = FindColorCol(img, start, black);

                //存储白色最大行数
                if (end - start > row) row = end - start;

                if (end == width - 1) return row;
                t = end;
            }

            return row;
        }
        //获取数字样本
        public List<Bitmap> GetSample()
        {
            List<Bitmap> arr = new List<Bitmap>();
            //获取样本
            for (int k = 0; k < 60; k++)
            {
                arr.Add(new Bitmap(@".\1图片\验证码\样本\" + k.ToString() + ".png"));

            }
            return arr;
        }
    }
}
