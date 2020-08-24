using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using log4net;
using log4net.Config;
using GTF_Config;
using GTF_LocalDB;
using Newtonsoft.Json.Linq;

namespace JEWELRY_NUBES.Util
{
    public class Constants
    {
        //경로
        public static string PATH_ROOT = "./";
        public static string PATH_CONFIG = "../Config/";
        public static string PATH_DB = "../DB/";
        public static string PATH_CERT = "../Cert/";

        public static string PATH_CONFIG_FILE = "Config.xml";   //CONFIG 파일명
        public static string PATH_DB_FILE = "STFM.accdb";       //LOCAL DB 파일명
        public static string ENCRYPT_CERT_FILE = "GTF_CAT_AUTH.cer";

        public static string SYSTEM_LANGUAGE = "EN";            //기본 설정 언어
        public static int PASSPORT_TYPE = -1;                   //여권스캐너
        public static string PRINTER_TYPE = string.Empty;       //프린터 명
        public static string PRINTER_OPOS_TYPE = string.Empty;  //OPOS 연결 프린터 명
        public static int SLIP_TYPE = -1;                       //출력전표 종류
        public static string PRINTER_SELECT = string.Empty;       //프린터 선택

        public static string GROUP_ID = string.Empty;
        public static string GROUP_NAME = string.Empty;
        public static string USER_ID = string.Empty;         //사용자 ID
        public static string USER_AUTH = string.Empty;          //사용자권한
        //리틀인디아
        public static string MID = string.Empty;          //사용자권한
        public static string TID = string.Empty;          //사용자권한
        public static string GST_NO = string.Empty;          //사용자권한
        public static string REC_PREFIX = string.Empty;          //사용자권한
        public static string DUP_RC = string.Empty;          //사용자권한
        public static string REC_DIGITS = string.Empty;
        public static string ITEM_CODE1 = string.Empty;          //사용자권한
        public static string ITEM_CODE2 = string.Empty;          //사용자권한
        public static string ITEM_CODE3 = string.Empty;          //사용자권한
        public static string ITEM_CODE4 = string.Empty;          //사용자권한
        public static string ITEM_CODE5 = string.Empty;          //사용자권한

        public static Boolean IS_DEV { get; set; }              //개발여부

        public static int SCAN_TIMEOUT { get; set; }            //여권스캐너 TIMEOUT
        public static string SERVER_URL { get; set; }           //서버 URL
        public static string OUTLET_TYPE { get; set; }
        public static int SERVER_TIMEOUT { get; set; }
        public static string APPEND_KEY { get; set; }

        //logger 생성
        public static ILog LOGGER_DOC { get; set; }             //전문 로그
        public static ILog LOGGER_SCREEN { get; set; }          //화면 액션 로그
        public static ILog LOGGER_MAIN { get; set; }            //전체 로그
        public static GTF_ConfigManager CONF_MANAGER;           //Config Manager
        public static GTF_LocalDBManager LDB_MANAGER;           //Local DB Manager
        public static string CHECK_SERVERDATE = "N";

        public static JArray merchant_list = null;

        public Constants()
        {
            PATH_CONFIG = PATH_ROOT + PATH_CONFIG;
            PATH_DB = PATH_ROOT + PATH_DB;
            PATH_CERT = PATH_ROOT + PATH_CERT;

            ENCRYPT_CERT_FILE = PATH_CERT + ENCRYPT_CERT_FILE;

            PATH_CONFIG_FILE = PATH_CONFIG + PATH_CONFIG_FILE;
            PATH_DB_FILE =  PATH_DB + PATH_DB_FILE;
            CONF_MANAGER = GTF_ConfigManager.Instance(null);
            CONF_MANAGER.loadAppConfig(PATH_CONFIG_FILE);
            LDB_MANAGER = GTF_LocalDBManager.Instance();
            LDB_MANAGER.dbOpen(PATH_DB_FILE);

            PASSPORT_TYPE       = "".Equals( LDB_MANAGER.selectConfigData("PASSPORT_TYPE")) ? -1 : Int32.Parse(LDB_MANAGER.selectConfigData("PASSPORT_TYPE"));
            PRINTER_TYPE        = LDB_MANAGER.selectConfigData("PRINTER_TYPE");
            PRINTER_OPOS_TYPE   = LDB_MANAGER.selectConfigData("PRINTER_OPOS_TYPE");
            PRINTER_SELECT      = LDB_MANAGER.selectConfigData("PRINTER_SELECT");
            SLIP_TYPE           = "".Equals(LDB_MANAGER.selectConfigData("SLIP_TYPE")) ? -1 : Int32.Parse(LDB_MANAGER.selectConfigData("SLIP_TYPE")); 

            SYSTEM_LANGUAGE = CONF_MANAGER.getAppValue("DEFAULT_LANG"); //터미널 기본언어 세팅
            IS_DEV = "Y".Equals(CONF_MANAGER.getAppValue("DEV"));       //개발여부
            SERVER_URL = CONF_MANAGER.getAppValue("SERVER_URL");
            APPEND_KEY = CONF_MANAGER.getAppValue("APPEND_KEY");
            SCAN_TIMEOUT = int.Parse(CONF_MANAGER.getAppValue("SCAN_TIMEOUT"));
            SERVER_TIMEOUT = int.Parse(CONF_MANAGER.getAppValue("SERVER_TIMEOUT"));

            OUTLET_TYPE = CONF_MANAGER.getAppValue("OUTLET_TYPE");

            //국가별 화면 디스크립트 load
            CONF_MANAGER.loadCustomConfig("ScreenText", PATH_CONFIG + "ScreenText.xml");
            //국가별Message load
            CONF_MANAGER.loadCustomConfig("Message", PATH_CONFIG + "Message.xml");

            //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //LOGGER 는 단말기 프로잭트 내에선 Constants 클래스 호출해서 사용
            //타 패키지에서는 생성자에서 Logger 를 세팅.
            log4net.Config.BasicConfigurator.Configure();

            XmlConfigurator.Configure(new System.IO.FileInfo(PATH_CONFIG + "\\" + CONF_MANAGER.getAppValue("LOG4NET_CONFIG"))); // <- Application base 디렉토리의 log4net.xml을 읽어들일 때. 

            LOGGER_DOC = log4net.LogManager.GetLogger("DOC");
            LOGGER_SCREEN = log4net.LogManager.GetLogger("SCREEN");
            LOGGER_MAIN = log4net.LogManager.GetLogger("MAIN");
            ILog dd = log4net.LogManager.GetLogger("ConsoleAppender");
            

            log4net.Config.BasicConfigurator.Configure();            
        }
    }
}
