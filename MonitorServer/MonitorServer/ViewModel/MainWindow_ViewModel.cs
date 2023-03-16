using Extension;
using MonitorServer.Command;
using MonitorServer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VS_Presentation;
using VS_Presentation.DialogControls;
using VS_Presentation.WPFControls;

namespace MonitorServer.ViewModel
{
    public class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string version
        {
            get
            {
                return String.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
        }

        #region 设备和协议选择
        /// <summary>
        /// 品牌列表
        /// </summary>
        public BindingList<KeyValuePair<string, string>> brandList
        {
            get
            {
                if (null == App.mainProd) return null;
                equipment = null;
                return new BindingList<KeyValuePair<string, string>>(App.mainProd.children.ToDictionary(x => x.Name, y => y.Name).ToList());
            }
        }
        /// <summary>
        /// 设备列表
        /// </summary>
        public BindingList<KeyValuePair<string, string>> equipmentList
        {
            get
            {
                if (null == brand) return null;
                return new BindingList<KeyValuePair<string, string>>((brand as Prod<Enum>).children.ToDictionary(x => x.Name, y => y.Name).ToList());
            }
        }
        /// <summary>
        /// 部署方式列表
        /// </summary>
        public BindingList<KeyValuePair<string, string>> deploymentList
        {
            get
            {
                if (null == equipment) return null;
                return new BindingList<KeyValuePair<string, string>>((equipment as Prod<Enum>).children.ToDictionary(x => x.Name, y => y.Name).ToList());
            }
        }

        ProdComponent _brand;
        /// <summary>
        /// 品牌
        /// </summary>
        public ProdComponent brand
        {
            get
            {
                if(null == _brand) return App.mainProd.FindChild("--");
                return _brand;
            }
            set
            {
                _brand = value;
                _server.handler = GetHandler();
                _server.handler.ReceiveMsg_Action += receiveData;
                OnPropertyChanged("brand");
            }
        }

        ProdComponent _equipment;
        /// <summary>
        /// 设备类型
        /// </summary>
        public ProdComponent equipment
        {
            get
            {
                return _equipment;
            }
            set
            {
                _equipment = value;
                OnPropertyChanged("equipment");
            }
        }

        ProdComponent _deployment;
        /// <summary>
        /// 部署方式
        /// </summary>
        public ProdComponent deployment
        {
            get
            {
                return _deployment;
            }
            set
            {
                _deployment = value;
                OnPropertyChanged("deployment");
            }
        }

        private BaseHandler GetHandler()
        {
            if (null == brand) return new CommonHandler();
            switch ((brand as Prod<Enum>).data)
            {
                case PROD_BRAND_ENUM.Mindray:
                    return new MindrayHandler();
                case PROD_BRAND_ENUM.LiBang:
                    return new LiBangHandler();
                case PROD_BRAND_ENUM.VIASYS:
                    return new VIASYSHandler();
                default:
                    return new CommonHandler();
            }
        }
        #endregion

        #region 设备连接
        /// <summary>
        /// 客户端列表
        /// </summary>
        public BindingList<Client> clientItems
        {
            get
            {
                return new BindingList<Client>(server.dicSocket.Select(x => new Client(x.Value.socket, x.Value.dateTime)).ToList());
            }
        }
        /// <summary>
        /// 选中的客户端列表
        /// </summary>
        public Client selected { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            get
            {
                if (selected == null) return null;
                string Guid;
                if (_server.handler.GUIDDic.TryGetValue(selected.Handle, out Guid))
                {
                    return Guid;
                }
                return null;
            }
        }
        /// <summary>
        /// 设备信息列表
        /// </summary>
        public List<WaveInfo> waveInfoItems
        {
            get
            {
                if (selected == null) return null;
                Dictionary<string, LeadData> leads = null;
                if(_server.handler.LeadDic.TryGetValue(selected.Handle, out leads))
                {
                    return leads.Select(x => new WaveInfo(x.Key, x.Value.id, x.Value.samp, x.Value.msmt)).ToList();
                }
                return null;
            }
        }
        #endregion

        #region 数据接收
        /// <summary>
        /// 是否停止刷新
        /// </summary>
        public bool isStopRefresh
        {
            set
            {
                server.isStopRefreshMessage = value;
            }
        }
        /// <summary>
        /// 是否分包
        /// </summary>
        public bool isSplitPackage
        {
            get
            {
                if (null == server || null == server.handler) return false;
                return server.isSplitPackage;
            }
            set
            {
                if (null == server || null == server.handler) return;
                server.handler.isSplitPackage = value;
                OnPropertyChanged("isSplitPackage");
            }
        }
        /// <summary>
        /// 是否以HEX形式展示
        /// </summary>
        public bool isHexShow
        {
            get
            {
                if (null == server || null == server.handler) return false;
                return server.isHexShow;
            }
            set
            {
                if (null == server || null == server.handler) return;
                server.handler.isHexShow = value;
                OnPropertyChanged("isHexShow");
            }
        }
        /// <summary>
        /// 当前信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 显示的数据内容
        /// </summary>
        public string data
        {
            get
            {
                if (selected == null) return "";
                return server.dicSocket[selected.Handle].data;
            }
        }
        #endregion

        public void receiveData(IntPtr connId, string msg)
        {
            OnPropertyChanged("data");
            OnPropertyChanged("waveInfoItems");
            OnPropertyChanged("GUID");
        }


        #region socket监听服务
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool isConnected
        {
            get
            {
                return server.isListening;
            }
        }

        public bool isDisconnected
        {
            get
            {
                return !server.isListening;
            }
        }

        Service _server;
        /// <summary>
        /// 监听服务类
        /// </summary>
        public Service server
        {
            get
            {
                if (_server == null)
                {
                    _server = new Service();
                    _server.handler = GetHandler();
                    _server.handler.ReceiveMsg_Action += receiveData;
                    _server.RefreshClients += () => { OnPropertyChanged("clientItems"); };
                }
                return _server;
            }
        }
        
        /// <summary>
        /// ip
        /// </summary>
        public string ip { get; set; } = "0.0.0.0";

        int _port = 0;
        /// <summary>
        /// 端口号
        /// </summary>
        public int port
        {
            get
            {
                if (deployment == null) return _port;
                return (deployment as Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>>).data.Value;
            }
            set
            {
                if (!isConnected)
                {
                    if (null == deployment) _port = value;
                    else (deployment as Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>>).data = new KeyValuePair<DEPLOYMENT_MODE_ENUM, int>((deployment as Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>>).data.Key, value);
                }
                OnPropertyChanged("deployment");
            }
        }
        #endregion

        #region 命令绑定
        /// <summary>
        /// 开始监听
        /// </summary>
        public ICommand StartCommand => new RelayCommand(obj =>
        {
            try
            {
                IPAddress tmpIp;
                if (IPAddress.TryParse(ip, out tmpIp))
                {
                    server.Start(tmpIp, port);
                }
            }
            catch (Exception ex)
            {
                Window_MessageBox window = new Window_MessageBox("错误", ex.ToString());
                window.ShowDialog();
            }
            OnPropertyChanged("isConnected");
            OnPropertyChanged("isDisconnected");
        });

        /// <summary>
        /// 停止监听
        /// </summary>
        public ICommand StopCommand => new RelayCommand(obj =>
        {
            try
            {
                server.Stop();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            OnPropertyChanged("isConnected");
            OnPropertyChanged("isDisconnected");
        });
        /// <summary>
        /// 分包
        /// </summary>
        public ICommand SplitCommand => new RelayCommand(obj =>
        {
            isSplitPackage = true;
        });
        /// <summary>
        /// 不分包
        /// </summary>
        public ICommand UnSplitCommand => new RelayCommand(obj =>
        {
            isSplitPackage = false;
        });
        /// <summary>
        /// 继续刷新
        /// </summary>
        public ICommand refreshShowCommand => new RelayCommand(obj =>
        {
            isStopRefresh = false;
        });
        /// <summary>
        /// 停止刷新
        /// </summary>
        public ICommand stopRefreshShowCommand => new RelayCommand(obj =>
        {
            isStopRefresh = true;
        });
        /// <summary>
        /// HEX格式
        /// </summary>
        public ICommand hexEncodingCommand => new RelayCommand(obj => 
        {
            isHexShow = true;
        });
        /// <summary>
        /// 非HEx格式
        /// </summary>
        public ICommand noHexEncodingCommand => new RelayCommand(obj =>
        {
            isHexShow = false;
        });
        /// <summary>
        /// 选中设备
        /// </summary>
        public ICommand selectionchangedCommand => new RelayCommand(obj =>
        {
            if (null != obj as WPFControl_DataGrid)
            {
                selected = (obj as WPFControl_DataGrid).SelectedItem as Client;
            }
            OnPropertyChanged("data");
            OnPropertyChanged("waveInfoItems");
            OnPropertyChanged("GUID");
        });
        /// <summary>
        /// 拷贝波形信息
        /// </summary>
        public ICommand CopyCommand => new RelayCommand(obj => 
        {
            if (null == waveInfoItems && GUID == null) return;
            JObject jObject = new JObject();
            jObject.Add("GUID", GUID);
            jObject.Add("Waves", waveInfoItems == null ? null :JArray.FromObject(waveInfoItems));
            Clipboard.SetDataObject(jObject.ToString());
            ToastBox toastBox = new ToastBox("已保存到剪切板", 2000);
            toastBox.Show();
        });
        /// <summary>
        /// 导出csv波形信息
        /// </summary>
        public ICommand ExportCommand => new RelayCommand(obj => 
        {
            if (null == waveInfoItems && GUID == null) return;
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                FileStream file = new FileStream(String.Format("{0}\\{1}_{2}.csv", foldPath, selected.EndPoint.Replace(".","_").Replace(":","_"), DateTime.Now.ToString("MMddHHmmss")), FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                if(null == waveInfoItems || waveInfoItems.Count() == 0)
                {
                    sw.WriteLine(String.Format("GUID:{0}", GUID));
                }
                else
                {
                    var items = waveInfoItems.ToList();
                    for (int i = 0; i < items.Count(); i++)
                    {
                        StringBuilder sbuilder = new StringBuilder();
                        if (i == 0)
                        {
                            sw.WriteLine(String.Format("GUID:{0}", GUID));
                            foreach (var property in items[i].GetType().GetProperties())
                            {
                                sbuilder.Append(property.GetDescription() + ",");
                            }
                            sw.WriteLine(sbuilder);
                        }
                        sbuilder = new StringBuilder();
                        foreach (var property in items[i].GetType().GetProperties())
                        {
                            sbuilder.Append(property.GetValue(items[i]) + ",");
                        }
                        sw.WriteLine(sbuilder);
                    }
                }
                sw.Flush();
                sw.Close();
                file.Close();
            }
        });
        /// <summary>
        /// 选择品牌
        /// </summary>
        public ICommand BrandSelectedCommand => new RelayCommand(obj => 
        {
            if (isConnected) return;
            if (null != obj) brand = App.mainProd.FindChild((obj as WPFControl_ContextMenuList).SelectedItem.Value);
            equipment = null;
            deployment = null;
            OnPropertyChanged("equipmentList");
            OnPropertyChanged("deploymentList");
        });
        /// <summary>
        /// 选择设备
        /// </summary>
        public ICommand EquipmentSelectedCommand => new RelayCommand(obj =>
        {
            if (isConnected) return;
            if (null != obj) equipment = (brand as Prod<Enum>).FindChild((obj as WPFControl_ContextMenuList).SelectedItem.Value);
            deployment = null;
            OnPropertyChanged("deploymentList");
        });
        /// <summary>
        /// 选择部署方式
        /// </summary>
        public ICommand DeploymentSelectedCommand => new RelayCommand(obj =>
        {
            if (isConnected) return;
            if (null != obj) deployment = (equipment as Prod<Enum>).FindChild((obj as WPFControl_ContextMenuList).SelectedItem.Value);
            _server.handler.deploymentMode = (deployment as Prod<KeyValuePair<DEPLOYMENT_MODE_ENUM, int>>).data.Key;
            OnPropertyChanged("port");
        });
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand CloseCommand => new RelayCommand(obj =>
        {
            if (isConnected)
            {
                Window_MessageDialog window = new Window_MessageDialog("提示", "是否停止监听并关闭窗口？");
                window.Event_Yes += (o, e) => 
                {
                    server.Stop();
                    window.Close();
                    Application.Current.MainWindow.Close();
                };
                window.Event_No += (o, e) =>
                {
                    window.Close();
                };
                window.ShowDialog();
            }
            else Application.Current.MainWindow.Close();
        });
        #endregion
    }
}
