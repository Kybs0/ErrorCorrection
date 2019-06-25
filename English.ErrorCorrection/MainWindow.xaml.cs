using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using English.Business;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace English.ErrorCorrection
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await GetResultsFromServerAsync("en-US", InputTextBox.Text);
            ErrorCorrectionTextBox.Text = result;
        }
        private static async Task<string> GetResultsFromServerAsync(string language, string textToCheck)
        {
            if (string.IsNullOrWhiteSpace(textToCheck))
            {
                return "";
            }
            //脚注引用所用的字符
            textToCheck = textToCheck.Replace("\u0002", "*");
            //uriString = uriString.Replace("%C2%A0", "+"); // replace non-breaking space. Why?
            string result = "";
            try
            {
                var checkResult = await EnglishCheckApiService.CheckEnglishSentenceAsync(textToCheck);
                result = checkResult.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        #region 遗弃

        //调用python核心代码
        public static void RunPythonScript(string pythonPath, string args = "", params string[] teps)
        {
            Process p = new Process();
            var pythonExeFileName = @"C:\Users\10167\source\repos\English.ErrorCorrection\English.ErrorCorrection\bin\Debug\Python37-32\python.exe";
            p.StartInfo.FileName = pythonExeFileName;//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            string sArguments = pythonPath;
            foreach (string sigstr in teps)
            {
                sArguments += " " + sigstr;//传递参数
            }
            sArguments += " " + args;

            p.StartInfo.Arguments = sArguments;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string resultInfo = p.StandardOutput.ReadToEnd();
            string errorInfo = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }


        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
        //    dynamic py = pyEngine.ExecuteFile(@"F:\Gitlab\gec\GEC_test.py");//读取脚本文件
        //    string reStr = py.error_correction("Provides precise C # call Python script information");//调用脚本文件中对应的函数
        //}

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    string pythonPath = @"F:\Gitlab\gec\GEC_test.py";//这里是python的文件名字 
        //    RunPythonScript(pythonPath, "-u","informatio");//运行脚本文件 
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pythonPath">待处理python文件的路径</param>
        /// <param name="args"></param>
        /// <param name="teps"></param>
        //public static bool RunPythonScript(string pythonPath, string sentence)
        //{
        //    Process p = new Process();
        //    // 待处理python文件的路径 & 添加参数
        //    string sArguments = $"\"{pythonPath}\"   \"{sentence}\"";
        //    //下面为启动一个进程来执行脚本文件设置参数 
        //    var pythonExeFileName = @"C:\Users\10167\source\repos\English.ErrorCorrection\English.ErrorCorrection\bin\Debug\Python37-32\python.exe"; //注意路径 
        //    try
        //    {
        //        var formattableString = $" /c \" \"{pythonExeFileName}\" \"{sArguments}\" \" ";
        //        var processStartInfo = new ProcessStartInfo("cmd.exe")
        //        {
        //            Verb = "runas",
        //            UseShellExecute = false,
        //            CreateNoWindow = true,
        //            RedirectStandardInput = false, //不重定向输入
        //            RedirectStandardOutput = true, //重定向输出
        //            RedirectStandardError = true,
        //            Arguments = formattableString
        //        };
        //        var cmdProcess = new Process();
        //        cmdProcess.StartInfo = processStartInfo;

        //        if (cmdProcess.Start())
        //        {
        //            //读取输出流释放缓冲,  不加这一句，进程会一直无限等待
        //            string resultInfo = cmdProcess.StandardOutput.ReadToEnd();
        //            string errorInfo = cmdProcess.StandardError.ReadToEnd();
        //            //等待程序执行完退出进程
        //            cmdProcess.WaitForExit();
        //            cmdProcess.Close();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return false;
        //}

        #endregion
    }
}
