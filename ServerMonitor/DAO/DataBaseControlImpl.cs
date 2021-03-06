﻿using Newtonsoft.Json.Linq;
using ServerMonitor.Controls;
using ServerMonitor.DAOImpl;
using ServerMonitor.Models;
using ServerMonitor.SiteDb;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace ServerMonitor.SiteDb
{
    public class DataBaseControlImpl : DBInit
    {
        /// <summary>
        /// 持有SiteDAO对象实现对Site的操作
        /// </summary>
        private ISiteDAO siteDao;
        /// <summary>
        /// 数据库名称
        /// </summary>
        private static string dbFileName;
        /// <summary>
        /// 数据库路径，通过私有方法生成
        /// </summary>
        private static string dBPath;
        /// <summary>
        /// 延迟加载对象
        /// </summary>
        public static DataBaseControlImpl Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        public static string DbFileName { get => dbFileName; set => dbFileName = value; }
        /// <summary>
        /// 数据库路径由私有方法生成，所以设为只读变量
        /// </summary>
        public static string DBPath { get => dBPath; }

        static DataBaseControlImpl() { }

        private DataBaseControlImpl()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="DBFilename">数据库名称</param>
        public void InitDB(string DBFilename)
        {
            SetDBFilename(DBFilename);
            siteDao = new SiteDaoImpl();
            // ApplicationData.Current.LocalFolder.Path balabala的指的是这个位置 ->C:\Users\xiao22805378\AppData\Local\Packages\92211ab1-5481-4a1a-9111-a3dd87b81b72_8zmgqd0netmce\LocalState\
            if (!File.Exists(dBPath))//判断数据库文件是否存在
            {
                // ApplicationData.Current.LocalFolder.Path balabala的指的是这个位置 ->C:\Users\xiao22805378\AppData\Local\Packages\92211ab1-5481-4a1a-9111-a3dd87b81b72_8zmgqd0netmce\LocalState\
                using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), dBPath))
                {
                    conn.CreateTable<SiteModel>();
                    conn.CreateTable<LogModel>();
                    conn.CreateTable<ErrorLogModel>();
                    conn.CreateTable<ContactModel>();
                    conn.CreateTable<SiteContactModel>();
                    List<SiteModel> l_site = new List<SiteModel>
                    {
                        // 插入默认的五条数据
                        new SiteModel()
                        {
                            Site_name = "Baidu",
                            Site_address = "https://www.baidu.com",
                            Is_server = false,
                            Protocol_type = "HTTPS",
                            Server_port = 1,
                            Monitor_interval = 5,
                            Is_Monitor = false,
                            Status_code = "200",
                            Request_TimeCost = 25383,
                            Create_time = DateTime.Now,
                            Update_time = DateTime.Now,
                            Is_pre_check = false,
                            Request_succeed_code = "200",
                            Is_success = 2
                        },
                        new SiteModel()
                        {
                            Site_name = "Yahoo",
                            Site_address = "http://www.yahoo.com",
                            Is_server = false,
                            Protocol_type = "HTTP",
                            Server_port = 1,
                            Monitor_interval = 5,
                            Is_Monitor = true,
                            Status_code = "200",
                            Request_TimeCost = 11851,
                            Create_time = DateTime.Now,
                            Update_time = DateTime.Now,
                            Is_pre_check = false,
                            Request_succeed_code = "200",
                            Is_success = 1
                        },
                        new SiteModel()
                        {
                            Site_name = "Bing",
                            Site_address = "http://www.bing.com",
                            Is_server = false,
                            Protocol_type = "HTTP",
                            Server_port = 1,
                            Monitor_interval = 5,
                            Is_Monitor = false,
                            Status_code = "200",
                            Request_TimeCost = 287,
                            Create_time = DateTime.Now,
                            Update_time = DateTime.Now,
                            Is_pre_check = false,
                            Request_succeed_code = "200",
                            Is_success = 2
                        },
                        new SiteModel()
                        {
                            Site_name = "Bing",
                            Site_address = "https://cn.bing.com",
                            Is_server = true,
                            Protocol_type = "SOCKET",
                            Server_port = 80,
                            Monitor_interval = 5,
                            Is_Monitor = true,
                            Status_code = "1000/0",
                            Request_TimeCost = 11,
                            Create_time = DateTime.Now,
                            Update_time = DateTime.Now,
                            Is_pre_check = true,
                            Request_succeed_code = "1000",
                            Is_success = 2,
                            Protocol_content =""
                        }
                    };
                    siteDao.InsertListSite(l_site);
                }
            }
            else
            {
                /**
                 * 放处理数据库已经存在的处理代码
                 */
            }
        }
        /// <summary>
        /// 设置数据库名
        /// </summary>
        /// <param name="Filename">数据库文件名</param>
        public void SetDBFilename(string Filename)
        {
            if (string.IsNullOrEmpty(Filename) && string.IsNullOrWhiteSpace(Filename))
            {
                throw new ArgumentNullException("操作数据库名称不合法!");
            }
            else
            {
                DbFileName = Filename;
                dBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, Filename);
            }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly DataBaseControlImpl instance = new DataBaseControlImpl();
        }
    }
}
