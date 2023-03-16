using Extension;
using Newtonsoft.Json.Linq;
using Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using VS_Presentation.DialogControls;

namespace MonitorServer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Prod<string> mainProd;

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += Application_ThreadException;
            GetProdConfig();
            MainWindow window = new MainWindow();
            window.Show();
        }

        /// <summary>
        /// 获取部署配置
        /// </summary>
        protected void GetProdConfig()
        {
            mainProd = new Prod<string>("svm");
            JArray array = JArray.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonitorSetting.json"), System.Text.Encoding.Default));
            foreach (var token in array)
            {
                PROD_BRAND_ENUM @enum;
                if(Enum.TryParse(token["PROD_BRAND"].ToString(), out @enum))
                {
                    Prod<Enum> prod = new Prod<Enum>(@enum.GetDescription());
                    prod.data = @enum;
                    foreach (var subtoken in (token as JObject))
                    {
                        if (subtoken.Key != "ECG" && subtoken.Key != "MV") continue;
                        EQUIP @enum2;
                        if(Enum.TryParse(subtoken.Key, out @enum2))
                        {
                            Prod<Enum> type = new Prod<Enum>(@enum2.GetDescription());
                            type.data = @enum2;
                            foreach (var endtoken in subtoken.Value)
                            {
                                DEPLOYMENT_MODE_ENUM @enum3;
                                if( Enum.TryParse(endtoken["MODE"].ToString(), out @enum3))
                                {
                                    Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>> mode = new Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>>(@enum3.GetDescription());
                                    mode.data = new KeyValuePair<DEPLOYMENT_MODE_ENUM, int>(@enum3, (int)endtoken["PORT"]);
                                    type.Add(mode);
                                }
                            }
                            prod.Add(type);
                        }
                    }
                    mainProd.Add(prod);
                }
            }
        }

        /// <summary>
        /// 通用异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            Window_MessageBox window_MessageBox = new Window_MessageBox("错误", e.ToString());
            window_MessageBox.ShowDialog();
        }
    }
}
