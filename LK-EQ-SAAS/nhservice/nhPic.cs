using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nhservice.nhPhotoService;
using System.Net;
using System.ServiceModel;
using System.Xml;

namespace nhservice
{
    public class nhPic
    {

        private static string[] ArrayToStrings(ArrayOfString ret)
        {
            if (ret == null) return null;
            string[] RetArr = new string[ret.Count];
            for (int i = 0; i < ret.Count; i++)
            {
                RetArr[i] = ret[i];
            }
            return RetArr;
        }


        /// <summary>
        /// 上传或更新照片
        /// </summary> 
        /// <returns></returns>
        public static string[] nhPicOnload(string grbm, string ylzh, string cardno,string sickname,
                                            string picStr,string hospcode,string voperator ) 
        { 
            PhotoServiceInterfacePortTypeClient client = null;
            try
            {
                string v_year=DateTime.Now.Year.ToString();
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.ReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 883647 };

                client = new PhotoServiceInterfacePortTypeClient(binding, new EndpointAddress("http://jk.gzxnh.gov.cn/xfirephoto/services/PhotoServiceInterface"));

                string[] RetArr = ArrayToStrings(client.save(NHClient.getAreaId(ylzh), grbm, ylzh, cardno, sickname, picStr, hospcode, voperator));
                if (RetArr == null)
                {
                    throw new Exception("上传照片信息失败");
                }
                if (RetArr[0].Equals("1")) //失败情况   
                {
                    throw new Exception("上传照片信息失败，原因【" + RetArr[1] + "】-nh");
                }
                return RetArr;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        /// <summary>
        /// 下载获取农合照片
        /// </summary> 
        /// <returns></returns>
        public static string[] nhGetPic(string grbm)
        {
            // string picurl = "http://10.0.2.132/blazeds/bin-debug/common/image.jsp?id=" + grbm;
            // WebClient webClient = new WebClient();
            // webClient.DownloadFile("http://10.0.2.132/blazeds/bin-debug/common/image.jsp?id=" + grbm, "test.jpg");
            // Image.FromFile("dingdang.png)//picshow是picturebox");  

            PhotoServiceInterfacePortTypeClient client = null;
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.ReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 883647 };

                client = new PhotoServiceInterfacePortTypeClient(binding, 
                         new EndpointAddress("http://jk.gzxnh.gov.cn/xfirephoto/services/PhotoServiceInterface"));
 
                string[] RetArr = ArrayToStrings(client.get(grbm));
                if (RetArr == null)
                 {
                   throw new Exception("下载照片信息失败");
                 }
                if (RetArr[0].Equals("1")) //失败情况   
                 {
                    throw new Exception("下载照片信息失败，原因【" + RetArr[1] + "】-nh");
                 }
                return RetArr;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }

    }
}
