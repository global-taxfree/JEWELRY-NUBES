using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GTF_Comm;
using JEWELRY_NUBES.Util;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace JEWELRY_NUBES.Tran
{
    //거래실행. 필요시 로컬 DB 정보 업데이트
    class Transaction
    {
        public string ServerUrl     { get; set; }
        public string ServerIp      { get; set; }
        public string ServerPort    { get; set; }

        public Transaction()
        {
            IgnoreBadCertificates();
        }

        public string GetMacAdress()
        {
            string mac_adress = "";

            NetworkInterface[] ads = NetworkInterface.GetAllNetworkInterfaces();
            foreach(NetworkInterface ad in ads)
            {
                if(ad.NetworkInterfaceType == NetworkInterfaceType.Ethernet && ad.OperationalStatus == OperationalStatus.Up )
                {
                    PhysicalAddress pa = ad.GetPhysicalAddress();
                    if (pa != null && !pa.ToString().Equals(""))
                    {
                        mac_adress = pa.ToString();
                        break;
                    }
                }
            }
            return mac_adress;
        }

        public string Login(String strUserId , String strPassword)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);
                // mac adress 가지고 오기
                //string mac_adress = GetMacAdress();
                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/login";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;
                JObject jsonmerchant = new JObject();

                jsonReq.Add("userid", strUserId);
                jsonReq.Add("password", strPassword);
                jsonReq.Add("outlet", Constants.OUTLET_TYPE);
                //jsonReq.Add("mac_adress", mac_adress);

                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                /*
                jsonRes = JObject.Parse(strRes);
                Constants.merchant_list = JArray.Parse(jsonRes["list"].ToString());
                strRes = jsonRes["code"].ToString();
                jsonmerchant = Constants.merchant_list[0].ToObject<JObject>(); 

                if (jsonRes["group_id"] != null)
                {
                    Constants.GROUP_ID = jsonRes["group_id"].ToString();
                    Constants.GROUP_NAME = jsonRes["group_name"].ToString();
                }

                if (jsonmerchant["mid"] != null)
                {
                    Constants.MID = jsonmerchant["mid"].ToString();
                }
                if (jsonmerchant["tid"] != null)
                {
                    Constants.TID = jsonmerchant["tid"].ToString();
                }
                if (jsonmerchant["gst_no"] != null)
                {
                    Constants.GST_NO = jsonmerchant["gst_no"].ToString();
                }
                if (jsonmerchant["rec_prefix"] != null)
                {
                    Constants.REC_PREFIX = jsonmerchant["rec_prefix"].ToString();
                }
                if (jsonmerchant["dup_rc"] != null)
                {
                    Constants.DUP_RC = jsonmerchant["dup_rc"].ToString();
                }
                if (jsonmerchant["rec_digits"] != null)
                {
                    Constants.REC_DIGITS = jsonmerchant["rec_digits"].ToString();
                }
                if (jsonmerchant["item_code1"] != null)
                {
                    Constants.ITEM_CODE1 = jsonmerchant["item_code1"].ToString();
                }
                if (jsonmerchant["item_code2"] != null)
                {
                    Constants.ITEM_CODE2 = jsonmerchant["item_code2"].ToString();
                }
                if (jsonmerchant["item_code3"] != null)
                {
                    Constants.ITEM_CODE3 = jsonmerchant["item_code3"].ToString();
                }
                if (jsonmerchant["item_code4"] != null)
                {
                    Constants.ITEM_CODE4 = jsonmerchant["item_code4"].ToString();
                }
                if (jsonmerchant["item_code5"] != null)
                {
                    Constants.ITEM_CODE5 = jsonmerchant["item_code5"].ToString();
                }
                */
                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["code"].ToString();

                if (jsonRes["group_id"] != null)
                {
                    Constants.GROUP_ID = jsonRes["group_id"].ToString();
                    Constants.GROUP_NAME = jsonRes["group_name"].ToString();
                }
                if (jsonRes["mid"] != null)
                {
                    Constants.MID = jsonRes["mid"].ToString();
                }
                if (jsonRes["tid"] != null)
                {
                    Constants.TID = jsonRes["tid"].ToString();
                }
                if (jsonRes["gst_no"] != null)
                {
                    Constants.GST_NO = jsonRes["gst_no"].ToString();
                }
                if (jsonRes["rec_prefix"] != null)
                {
                    Constants.REC_PREFIX = jsonRes["rec_prefix"].ToString();
                }
                if (jsonRes["dup_rc"] != null)
                {
                    Constants.DUP_RC = jsonRes["dup_rc"].ToString();
                }
                if (jsonRes["item_code1"] != null)
                {
                    Constants.ITEM_CODE1 = jsonRes["item_code1"].ToString();
                }
                if (jsonRes["item_code2"] != null)
                {
                    Constants.ITEM_CODE2 = jsonRes["item_code2"].ToString();
                }
                if (jsonRes["item_code3"] != null)
                {
                    Constants.ITEM_CODE3 = jsonRes["item_code3"].ToString();
                }
                if (jsonRes["item_code4"] != null)
                {
                    Constants.ITEM_CODE4 = jsonRes["item_code4"].ToString();
                }
                if (jsonRes["item_code5"] != null)
                {
                    Constants.ITEM_CODE5 = jsonRes["item_code5"].ToString();
                }
                string dtn = DateTime.Now.ToString("yyyyMMdd"); 

                if(dtn.Equals(jsonRes["to_day"].ToString()))
                {
                    Constants.CHECK_SERVERDATE = "Y";
                }
            }
            catch(Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        //가맹점 검색
        public string searchStoreName(String strKeyword)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchStoreName";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("keyword", strKeyword);
                jsonReq.Add("group_id", Constants.GROUP_ID);

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        //자동완성 가맹점 검색
        public string searchAutoCompanyName(String strKeyword)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchAutoCompanyName";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("keyword", strKeyword);
                jsonReq.Add("group_id", Constants.GROUP_ID);

               // Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        //자동완성 국가코드
        public string searchCountryCode(String strKeyword)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchCountryCode";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

               // Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        //Purchase Item List
        public string getItemCodeList()
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/getItemCodeList";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

               // Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            return strRes;
        }

        public string issueTicket(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/json/etrs/issueTicket";

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            return strRes;
        }

        public string voidTicket(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/json/etrs/voidTicket";

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            return strRes;
        }

        // 2017년 9월 5일 전표번호 중복 체크 조회 
        public int checkRctCount(string jsonReq)
        {
            int nRctCnt = 0;           

            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/checkRctCount";

                JObject jsonRes = null;

               // Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                if (jsonRes["code"].ToString().Equals("1"))
                {
                    nRctCnt = int.Parse(jsonRes["total_cnt"].ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return nRctCnt;
        }

        public int searchDocIdCount(string jsonReq)
        {
            int total_cnt = 0;

            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchDocIdCount";

                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
               // Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                if (jsonRes["code"].ToString().Equals("1"))
                {
                    total_cnt = int.Parse(jsonRes["total_cnt"].ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return total_cnt;
        }

        public string searchDocIdList(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchDocIdList";

                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        public string searchBuyJewelleryAmt(string jsonReq)
        {
            string strRes = string.Empty;
            
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchBuyJewelleryAmt";

                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                //jsonRes = JObject.Parse(strRes);
                //strRes = jsonRes["check_yn"].ToString();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        public string checkRefundAmt(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/checkRefundAmt";

                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }


        public string updateSealBagInfo(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/json/etrs/updateSealBagInfo";
                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);
                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["code"].ToString();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            return strRes;
        }


        public int searchReceiptCount(string jsonReq)
        {
            int total_cnt = 0;

            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchReceiptCount";

                JObject jsonRes = null;

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                if (jsonRes["code"].ToString().Equals("1"))
                {
                    total_cnt = int.Parse(jsonRes["total_cnt"].ToString());
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return total_cnt;
        }

        public string searchReceiptList(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchReceiptList";

                JObject jsonRes = null;

               // Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        public string getDocIdDetails(String strDocId)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/getDocIdDetails";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("doc_id", strDocId);

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        public string getReceiptDetails(String strDocId)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/getReceiptDetails";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("doc_id", strDocId);

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }

        public bool checkNetworkStatus()
        {
            string strRes = "";

            bool retValue = false;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/checkNetworkStatus";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("userid", Constants.USER_ID);

                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);

                jsonRes = JObject.Parse(strRes);

                if (jsonRes["code"] != null && jsonRes["code"].ToString().Equals("1"))
                {
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return retValue;
        }

        public string UserInfo(String strUserId)
        {
            string strRes = string.Empty;
            return strRes;
        }

        //서버 전표 등록
        public string Submit()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //서버 전표 등록 최종 확인
        public string Confirm()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //조회
        public string Search()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //세부항목조회
        public string SearchDetail()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //로컬DB 설정값 조회
        public string SearchConfig()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //가장 마지막에 접속한 사용자 정보 업데이트
        public string getLastUserInfo()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //가장 마지막에 접속한 사용자 정보 업데이트
        public string setLastUserInfo()
        {
            string strRes = string.Empty;
            return strRes;
        }

        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            System.Net.ServicePointManager.ServerCertificateValidationCallback
             = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        private static bool AcceptAllCertifications
        (
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        }

        #region TR - LOG BOOK
        public int searchLogBookCount(string jsonReq)
        {
            int total_cnt = 0;

            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchLogBookCount";

                JObject jsonRes = null;

                Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                if (jsonRes["code"].ToString().Equals("1"))
                {
                    total_cnt = int.Parse(jsonRes["total_cnt"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return total_cnt;
        }

        public string searchLogBook(string jsonReq)
        {
            string strRes = string.Empty;

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                ServerUrl = Constants.SERVER_URL + "/service/merchant/tfm/searchLogBook";

                JObject jsonRes = null;

                Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(ServerUrl, jsonReq.ToString(), true, Constants.SERVER_TIMEOUT);
                Constants.LOGGER_DOC.Info("<--" + strRes);

                jsonRes = JObject.Parse(strRes);
                strRes = jsonRes["list"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.Message);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return strRes;
        }
        #endregion

    }
}
