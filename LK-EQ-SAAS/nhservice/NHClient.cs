using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nhservice.NHService;
using nhservice.orm;
using System.Data;
using System.ServiceModel.Description;
using YtUtil.tool;

namespace nhservice
{
    public class NHClient
    {
        /// <summary>
        /// 设置农合访问超时时间
        /// </summary>
        private static void setNHOutTime(HisInterfacePortTypeClient client, int nh_SendTime)
        {
            client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, nh_SendTime / 60, nh_SendTime % 60);
        }

        private static ArrayOfString getStr(List<RegisterMx> mxs, string filed)
        {
            ArrayOfString ar = new ArrayOfString();
            for (int i = 0; i < mxs.Count; i++)
            {
                RegisterMx c = mxs[i];
                if (filed.Equals("Id"))
                {
                    ar.Add(c.Id);
                }
                else if (filed.Equals("Code"))
                {
                    ar.Add(c.Code);
                }
                else if (filed.Equals("Name"))
                {
                    ar.Add(c.Name);
                }
                else if (filed.Equals("Lb"))
                {
                    ar.Add(c.Lb);
                }
                else if (filed.Equals("Gg"))
                {
                    ar.Add(c.Gg);
                }
                else if (filed.Equals("Jx"))
                {
                    ar.Add(c.Jx);
                }
                else if (filed.Equals("Dw"))
                {
                    ar.Add(c.Dw);
                }
                else if (filed.Equals("Sysj"))
                {
                    ar.Add(c.Sysj.ToString("yyyy-MM-dd"));
                }
                else if (filed.Equals("Area"))
                {
                    ar.Add(c.Area);
                }

            }
            return ar;

        }
        private static ArrayOfFloat getFloat(List<RegisterMx> mxs, string filed)
        {
            ArrayOfFloat ar = new ArrayOfFloat();
            for (int i = 0; i < mxs.Count; i++)
            {
                RegisterMx c = mxs[i];
                if (filed.Equals("Dj"))
                {
                    ar.Add(float.Parse(c.Dj + ""));
                }
                else if (filed.Equals("Sl"))
                {
                    ar.Add(float.Parse(c.Sl + ""));
                }
                else if (filed.Equals("Je"))
                {
                    ar.Add(float.Parse(c.Je + ""));
                }

            }
            return ar;

        }
        public static string getErr(string msg)
        {
            if (msg.IndexOf("SYSTEM_") > -1)
                return msg.Replace("SYSTEM_", "");
            return null;
        }
        /// <summary>
        /// 获取地区ID
        /// </summary>
        /// <param name="nh"></param>
        /// <returns></returns>
        public static string getAreaId(string nh)
        {
            if (nh.Length >= 12)
            {
                return nh.Substring(0, 6);
            }
            if (NHHOSCODE == null || NHHOSCODE.Length == 0)
            {
                throw new Exception("医疗机构的农合编码不存在！");
            }
            return NHHOSCODE;
        }
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
        /// 获取地区编码
        /// </summary>
        /// <param name="YLCode"></param>
        /// <returns></returns>
        public static string getAreaCode(string YLCode)
        {  //获得地区编码
            /*
              如果【医疗证号】为12位以上的，就取前6位作为地区编码，否则需要自己输入。为了方便性，在此取本地注册的编码。
           */
            //if(areacode == null){ 
            string areacode = "";
            int codelen = YLCode.Length;
            if (codelen >= 12)
            {
                areacode = YLCode.Substring(0, 6);
            }
            else
            {
                areacode = NHHOSCODE;
            }

            return areacode;
        } 
        //====================================================================================
        //                      获取病人信息相关
        //====================================================================================
        /// <summary>
        /// 单位
        /// </summary>
        public static string NHHOSCODE;
        /// <summary>
        /// 获取参合病人信息
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <returns></returns>
        public static List<NHPeoInfo> GetPeoList(string nhyear,string nhylzh)
        {
            HisInterfacePortTypeClient client =null;
            try
            {
                client  = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 
                if (nhyear.Equals(""))
                    nhyear = DateTime.Now.Year + "" ;

                List<NHPeoInfo> lis = new List<NHPeoInfo>();
                ArrayOfString[] ls = client.traceQueryTargetY(nhyear, getAreaId(nhylzh), nhylzh);
                if (ls == null)
                {
                    throw new Exception("提取病人农合信息失败，请检查农合医疗证号是否正确！");
                }
                foreach (ArrayOfString p in ls) {
                    NHPeoInfo br = new NHPeoInfo();
                    br.Grbm = p[0];
                    br.Xm   = p[1];
                    br.Xb   = p[2];
                    br.Csrq = p[3];
                    br.Ide  = p[4];
                    br.Ylzh = p[5];
                    br.Sfmb = p[6];
                    br.mztotalbc = p[7];
                    br.zytotalbc = p[8];
                    br.topflag   = p[9];

                    lis.Add(br);
                }
                if (lis.Count == 0) {
                    throw new Exception("提取参合家庭成员信息失败，请检查农合医疗证号是否正确！");
                }
                return lis;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
            
        }
        /// <summary>
        /// 获取农合账户信息
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <returns></returns>
        public static NHHomeInfo GetHomeInfo(string nhyear,string nhylzh)
        {
            HisInterfacePortTypeClient client =null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8);
                if (nhyear.Equals(""))
                    nhyear = DateTime.Now.Year + "";
                NHHomeInfo br = new NHHomeInfo();
                ArrayOfString p = null;
                try
                {
                    p = client.traceTargetB(nhyear, getAreaId(nhylzh), nhylzh);
                }
                catch {
                    throw new Exception("连接农合服务器失败，请稍候在试，或者使用其他方式进行登记！");
                }
                if (p == null)
                {
                    throw new Exception("获取参和家庭信息失败，请检查农合医疗证号是否正确！");
                }
                br.Jtbh = p[0];
                br.Ylzh = p[1];
                br.Hzxm = p[2];
                br.Zz = p[3];
                br.Zhye = p[4];
                br.Hsx = p[5];
                br.Chzt = p[6];
                br.Zhzt = p[7];
                return br;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
            
        }
       
        //====================================================================================
        //                      门诊计费相关
        //====================================================================================

        /// <summary>
        /// 上传门诊信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="mxs"></param>
        /// <returns></returns>
        public static string SubmitMZInfo(string nhyear,RegisterInfo info, List<RegisterMx> mxs){
            string nhmzh = RegMz(nhyear,info);
            info.Nhmzh = nhmzh;
            try
            {
                SubmitMzhMx(info, mxs);
            }
            catch (Exception e) {
                DelMzXX(info.Ylzh, nhmzh);
                throw e;
            }
            return nhmzh;
        
        }
       
        /// <summary>
        /// 删除门诊信息
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="nhmzh"></param>
        /// <returns></returns>
        public static string DelMzXX(string nhylzh,string nhmzh)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 20); 

                string msg = client.traceQueryTargetN(DateTime.Now.Year + "", getAreaId(nhylzh), nhmzh, true);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return d;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
        /// <summary>
        /// 冲红
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// 

        //-----------测试所用--
        public static void testop(string nhylzh, string nhmzh)
        {   HisInterfacePortTypeClient client = null; 
                client = new HisInterfacePortTypeClient();
                ArrayOfString res;
                for (int i = 14351820; i < 14352459; i++)
                {
                    res = client.traceQueryTargetQ(DateTime.Now.Year + "", "522423", i+"");
                    if ("0".Equals(res[0]) && "5224230313030014".Equals(res[4]))
                    {
                        //MessageBox.Show("" + i, "提示");
                        break;
                    }
                } 
 
        }  //--------xxx-

        public static string[] MzCh(string nhylzh,string nhmzh)
        {
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 15); 

                ArrayOfString res  ; 
                res = client.traceQueryTargetR(DateTime.Now.Year + "", getAreaId(nhylzh), nhmzh); 
                string[] RetArr = ArrayToStrings(res);
                if (RetArr == null)
                {
                    throw new Exception("冲红失败");
                }
                if (!RetArr[0].Equals("0")) //失败情况   
                { 
                    //遇到这种情况需要重复冲红,(即：可能由于超时导致农合端已经被冲红，HIS端还未冲红的情况，重新结算再冲红)
                    try
                    {
                        if (RetArr[1].IndexOf(nhmzh) >= 0)
                        {
                            try
                            {
                                NHClient.MzJs("",nhylzh, nhmzh, true);
                            }catch{
                                return RetArr;
                            }

                            res = client.traceQueryTargetR(DateTime.Now.Year + "", getAreaId(nhylzh), nhmzh);
                            RetArr = ArrayToStrings(res);
                            if (RetArr == null)
                            {
                                throw new Exception("冲红失败");
                            }
                            else if (!RetArr[0].Equals("0")) //失败情况   
                            {
                                throw new Exception("冲红失败," + RetArr[1]+"-nh");
                            }
                        }
                        else
                        {
                            throw new Exception("冲红失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                                        
                }
                return RetArr;               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }


        /// <summary>
        /// 门诊结算
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="nhmzh"></param>
        /// <param name="IfCal">true 真实结算 false 预结算</param>
        /// <returns></returns>
        public static string[] MzJs(string vYear, string nhylzh, string nhmzh, bool IfCal)  // 门诊结算
        {
            HisInterfacePortTypeClient client = null;
            string vArea = getAreaCode(nhylzh);
            if (vYear.Equals(""))
                vYear = DateTime.Now.Year.ToString();
           
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 20); 

                ArrayOfString ret=null;
                // 费用结算  : 真实结算和模拟结算
                if (IfCal)
                {        //---真实结算
                    ret = client.traceQueryTargetP(vYear, vArea, nhmzh);
                    string[] RetArr = ArrayToStrings(ret);
                    if (RetArr == null) {
                        throw new Exception("新农合报销结算失败");
                    }
                    if (RetArr[0]==null || !RetArr[0].Equals("0")) //失败情况   
                    {
                        string ss = RetArr[1] == null ? " " : RetArr[1];
                        throw new Exception("新农合报销结算失败，原因【" + ss + "】");
                        
                    }
                    return RetArr;
                }
                else
                {       //---模拟结算
                    ret = client.traceQueryTargetO(vYear, vArea, nhmzh);
                    string[] RetArr = ArrayToStrings(ret);
                    if (RetArr == null)
                    {
                        throw new Exception("预结算失败null");
                    }
                    if (!RetArr[0].Equals("0")) //失败情况   
                    {
                        throw new Exception("预结算失败 RetArr[0]=" + RetArr[0]);                       
                    }
                    return RetArr;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
          
        }

        /// <summary>
        /// 上传农合门诊信息
        /// </summary>
        /// <param name="pa"></param>
        /// <returns></returns>
        private static string RegMz(string nhyear,RegisterInfo RegData)
        {

            HisInterfacePortTypeClient client =null;
            try
            {
                if (nhyear.Equals(""))
                    nhyear = DateTime.Now.Year.ToString() ;
                RegisterInfo info = new RegisterInfo();

                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 20);

                string ret = client.traceQueryTargetL(nhyear, RegData.Areacode, RegData.Grbm,
                      NHHOSCODE, RegData.Ryrq, RegData.Ksbm, RegData.Doctors, RegData.Status, RegData.Jbbm, RegData.Type,
                       "", "", "", "", RegData.Istg, RegData.IsSS, RegData.Isjtzh, "", "", "");
                
                string d = NHClientUtil.PDError(ret);
                if (d != null) {
                    throw new Exception(d);
                }
                return ret;
                
            }
            catch (Exception e) {
                throw new Exception(e.Message); //+ e.StackTrace
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
           
        }
        
        /// <summary>
        /// 上传门诊明细
        /// </summary>
        /// <param name="info"></param>
        /// <param name="mxs"></param>
        /// <returns></returns>
        private static string SubmitMzhMx(RegisterInfo info, List<RegisterMx> mxs)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 30); 

                string ys = info.Ryrq.Split('-')[0] + "";
                ArrayOfString year = new ArrayOfString();
                
                ArrayOfString nhh = new ArrayOfString();
               
                for (int i = 0; i < mxs.Count; i++)
                {
                    year.Add(ys);
                   
                    nhh.Add(info.Nhmzh);
                }

                string msg = client.traceQueryTargetM(year, getStr(mxs, "Area"), getStr(mxs, "Id"), nhh,
                    getStr(mxs, "Code"), getStr(mxs, "Name"), getStr(mxs, "Lb"), getStr(mxs, "Gg"), getStr(mxs, "Jx"), getStr(mxs, "Dw")
                    , getFloat(mxs, "Dj"), getFloat(mxs, "Sl"), getFloat(mxs, "Je")
                    , getStr(mxs, "Sysj"), null, null, null, null, null, null, null, null, null, null);

                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
           
        }
        public static string[] MzJsMoni(string ylzh, string nhbm)  //门诊预结算（多信息返回）2011新增
        {/**arr[0]	是否成功0成功1失败    arr[1] 患者姓名         arr[2]    患者性别
			arr[3]	年龄                                          arr[4]    医疗证号      arr[5]	家庭地址
			arr[6]	联系方式              arr[7]	就医机构名称  arr[8]    就诊时间
			arr[9]	就诊号                arr[10]	诊断          arr[11]	账户余额
			arr[12]	本年度门诊次数        arr[13]	门诊总费用    arr[14]	保内费用
			arr[15]	自费费用              arr[16]	起伏线        arr[17]	家庭账户补助
			arr[18]	门诊统筹补助          arr[19]	大额统筹补助  arr[20]	备注
			arr[21]	计算过程  **/  			

            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 

                string[] RetArr = ArrayToStrings(client.traceQueryTargetQ(DateTime.Now.Year.ToString(), getAreaId(ylzh), nhbm));
                if (RetArr == null)
                {
                    throw new Exception("获取农合补偿结算信息失败");
                }
                if (RetArr[0]==null ||RetArr[0].Equals("1")) //失败情况   
                {
                    string ss = RetArr[1] == null ? "" : RetArr[1];
                    throw new Exception("获取农合补偿结算信息失败，原因【" + ss + "】");

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
        
        //====================================================================================
        //                      住院计费相关
        //====================================================================================
       
        /// <summary>
        /// 上传住院记录
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>

        public static string ZYRegister(ZyRegisterInfo reg)  // 入院登记
        {
            string ret;
            string v_year;

            string[] RegData = new string[19];
            RegData[0] = reg.Area;
            RegData[1] = reg.Grbm;
            RegData[2] = reg.Hoscode;
            RegData[3] = reg.Zyh;
            RegData[4] = reg.Rytime;
            RegData[5] = reg.Cytime;
            RegData[6] = reg.Ryks;
            RegData[7] = reg.Cyks;
            RegData[8] = reg.Doctor;
            RegData[9] = reg.Ryzt;
            RegData[10] = reg.Cyzt;
            RegData[11] = reg.Jbbm;
            RegData[12] = reg.Bq;
            RegData[13] = reg.Bfh;
            RegData[14] = reg.Cwh;
            RegData[15] = reg.Sfzjqt;
            RegData[16] = reg.Sscode;
            RegData[17] = reg.ZyLx;
            HisInterfacePortTypeClient client = null;            

            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 15); 
                
                v_year = RegData[4].Substring(0, 4); //根据入院日期来获取年份
                ret = client.traceQueryTargetE1(v_year, RegData[0], RegData[1], RegData[2], RegData[3],
                                              RegData[4], RegData[5], RegData[6], RegData[7], RegData[8], RegData[9],
                                              RegData[10], RegData[11], RegData[12], RegData[13], RegData[14], RegData[15],
                                              "", RegData[16], "", RegData[17], "", "", "", "", "001");
                string d = NHClientUtil.PDError(ret);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return ret;
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
        /// 上传住院明细
        /// </summary>
        /// <param name="mxs"></param>
        /// <param name="zynhh"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static string SubmitZYMx(List<RegisterMx> mxs, string zynhh, string years)
        {

            HisInterfacePortTypeClient client = null;            

            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 30); 

                ArrayOfString year = new ArrayOfString();
                ArrayOfString nhh = new ArrayOfString();
                for (int i = 0; i < mxs.Count; i++)
                {
                    year.Add(years);
                    nhh.Add(zynhh);
                }
                string msg = client.traceQueryTargetF(year, getStr(mxs, "Area"), getStr(mxs, "Id"), nhh, 
                    getStr(mxs, "Code"), getStr(mxs, "Name") , getStr(mxs, "Lb"), getStr(mxs, "Gg"), 
                    getStr(mxs, "Jx"), getStr(mxs, "Dw") , getFloat(mxs, "Dj"), getFloat(mxs, "Sl"), 
                    getFloat(mxs, "Je") , getStr(mxs, "Sysj"));
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

        }
        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="YLCode"></param>
        /// <param name="RecNo"></param>
        /// <returns></returns>
        public static string[] ZyJsMoni(string v_year ,string ylzh, string nhzybm)  //住院预结算（多信息返回）2011新增
        {
            //0--	状态0成功，1失败                   
            //1--失败说明    
            //2--住院总费用            
            //3--保内总费用
            //4--起伏线           
            //5--实际补偿          
            //6--计算公式     
            //7--年度累计补偿次数  
            //8--年度累计补偿费用
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 10); 

                string[] RetArr = ArrayToStrings(client.traceQueryTargetH1(v_year, getAreaId(ylzh), nhzybm, ""));
                if (RetArr == null)
                {
                    throw new Exception("获取农合补偿结算信息失败");
                }
                if (!RetArr[0].Equals("0")) //失败情况   
                {
                    //可能农合端已经结算的处理方法
                    DataTable dt = GetSickBCInfo(ylzh);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (nhzybm.Equals(r["f0"]) && r["f14"].ToString().IndexOf("结算") >= 0)
                            {
                                RetArr[0] = "ret";  //需要冲红重新上传农合的标志
                                return RetArr;
                            }
                        }
                     }
                    throw new Exception("获取农合补偿结算信息失败，原因【" + RetArr[1] + "】");
                }

                if (!RetArr[2].Equals("") && double.Parse(RetArr[2]) < double.Parse(RetArr[3]))
                {
                    throw new Exception("总费用：" + RetArr[2] + "、保内可报销费用：" + RetArr[3] +
                                        " \r\n        保内费用大于总费用，请检查一下农合费用编码的正确性，\r\n然后再进行费用重传操作。");
                }
                return RetArr;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally {
                if (client != null)  
                    client.Close(); 
            }
        }
        /// <summary>
        /// 住院预结算 多信息返回
        ///  arr[0]	是否成功0成功1失败    arr[1] 患者姓名      arr[2]	患者性别
        ///     arr[3]	年龄                                      arr[4]  医疗证号      arr[5]	家庭地址
        ///      arr[6]	联系方式                          arr[7]	就医机构名称  arr[8]	入院时间
        ///     arr[9]	出院时间                        arr[10]	出院诊断           arr[11]	住院天数
        ///     arr[12]	本年度住院次数         arr[13]	住院总费用       arr[14]	保内费用
        ///     arr[15]	自费费用                       arr[16]	起伏线                 arr[17]	实际补偿
        ///     arr[18]	自付金额                      arr[19]	年度累计补偿  arr[20]	备注
        ///     arr[21]	计算过程
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="nhzybm"></param>
        /// <returns></returns>
        public static string[] ZyJsMoni1(string ylzh, string nhzybm)  //住院预结算（多信息返回）2011新增
        {
           
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 

                string[] RetArr = ArrayToStrings(client.traceQueryTargetK(DateTime.Now.Year.ToString(), getAreaId(ylzh), nhzybm));
                if (RetArr == null)
                {
                    throw new Exception("获取农合补偿结算信息失败");
                }
                if (RetArr[0].Equals("1")) //失败情况   
                {
                    throw new Exception("获取农合补偿结算信息失败，原因【" + RetArr[1] + "】");

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
        /// 住院正式结算
        /// </summary>
        /// <param name="YLCode"></param>
        /// <param name="RecNo"></param>
        /// <returns></returns>
        public static string ZyJs(string ylzh, string nhzybm)  
        {
            
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 15); 

                string ret = client.traceQueryTargetI(DateTime.Now.Year.ToString(), getAreaId(ylzh), nhzybm);
                string d = NHClientUtil.PDError(ret);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return ret;

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
        /// 已上传住院明细的总费用
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="nhzybm"></param>
        /// <returns></returns>
        public static string GetZYTotalFare(string ylzh, string nhzybm)
        {
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 

                string msg = client.traceQueryTargetS(DateTime.Now.Year.ToString(), getAreaId(ylzh), nhzybm);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }

                return msg;
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
        /// 删除住院登记信息
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="nhzybm"></param>
        /// <returns></returns>
        public static string DelZyInfo(string ylzh,string nhzybm) {
            string year = DateTime.Now.Year.ToString();
            string area= getAreaId(ylzh);
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 20); 

                string msg = client.traceQueryTargetG(year, area, nhzybm, true);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;
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
        /// 清除住院登记信息
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="nhzybm"></param>
        /// <returns></returns>
        public static string ClearZyInfo(string ylzh, string nhzybm)
        {
            string year = DateTime.Now.Year.ToString();
            string area = getAreaId(ylzh);
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 28); 

                string msg = client.traceQueryTargetG(year, area, nhzybm, false);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;
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
        /// 进行住院单独模拟
        /// </summary> 
        /// <param name="zynhh">医疗证号</param>
        /// <param name="nhzybm">就诊补偿序号</param>
        /// <param name="iflag">是否需要显示错误消息标志</param>
        /// <returns></returns>
        public static string ZYFareMoni(string nhylzh, string nhzybm, bool iflag)
        { 
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 

                string msg = client.traceQueryTargetH(DateTime.Now.Year.ToString(), getAreaId(nhylzh), nhzybm);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    if (iflag)
                      throw new Exception(d);
                    else
                      return "";
                }
                return msg;
            }
            catch (Exception e)
            {
                if (iflag) throw e; else return "";
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
        /// 重新上传住院明细
        /// </summary>
        /// <param name="mxs"></param>
        /// <param name="zynhh"></param>
        /// <param name="nhzybm"></param>
        /// <returns></returns>
        public  static string ReUpZyMx(List<RegisterMx> mxs, string nhylzh, string nhzybm)
        {
            ClearZyInfo(nhylzh, nhzybm);
            if (mxs != null) {
                SubmitZYMx(mxs, nhzybm, DateTime.Now.Year.ToString());
            }
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 28); 

                string msg =client.traceQueryTargetH(DateTime.Now.Year.ToString(), getAreaId(nhylzh), nhzybm);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;
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
        ///获取住院补偿序号
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="grbm">个人编码</param>
        /// <returns></returns>
        public  static string getZyId( string nhylzh, string grbm)
        {
          
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 

                string msg = client.traceQueryTargetC(DateTime.Now.Year.ToString(), getAreaId(nhylzh), NHHOSCODE, grbm);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                    throw new Exception(d);
                }
                return msg;
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
        /// 住院退票: 对住院结算信息进行回退
        /// </summary>
        /// <param name="YLCode"></param>
        /// <param name="RecNo"></param>
        /// <returns></returns>
        public static string ZyJsHuiTui(string ylzh, string nhzybm)  // 住院退票: 对住院结算信息进行回退
        {
            string year = DateTime.Now.Year.ToString();
            string area = getAreaId(ylzh);
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 12); 

                string msg = client.traceQueryTargetJ(year, area, nhzybm);
                string d = NHClientUtil.PDError(msg);
                if (d != null)
                {
                        //判断农合端是否已被冲红,如果农合端已冲红，就只执行HIS端冲红即可
                    DataTable dt = GetSickBCInfo(ylzh);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows) 
                        {
                           if (nhzybm.Equals(r["f0"]) && (r["f14"].ToString().IndexOf("冲红")>=0)) return msg;
                        }
                    }

                    throw new Exception(d);
                }
                return msg;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null) 
                    client.Close();
            }
        }
        /// <summary>
        /// 获取农合病人信息
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="SickName"></param>
        /// <param name="indate"></param>
        /// <returns></returns>
        public static string[] GetPersonInfo(string nhyear,string ylzh, string SickName, string indate)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
                if (nhyear.Equals(""))
                    nhyear = DateTime.Now.Year.ToString();
                
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8);

                string[] RetArr = ArrayToStrings(client.traceTargetA(nhyear, getAreaId(ylzh), SickName, ylzh));
                if (RetArr == null)
                {
                    if (!"".Equals(indate))
                    {
                        nhyear = indate.Substring(0, 4);
                        RetArr = ArrayToStrings(client.traceTargetA(nhyear, getAreaId(ylzh), SickName, ylzh));
                        if (RetArr == null)
                        {
                            throw new Exception("获取病人信息失败");
                        }
                    }
                    else throw new Exception("获取病人信息失败");
                }
                if (RetArr[0].Equals("1")) //失败情况   
                {
                    throw new Exception("获取病人信息失败，原因【" + RetArr[1] + "】");

                }
                return RetArr;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
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
        /// 获取农合病人信息
        /// </summary>
        /// <param name="ylzh"></param>
        /// <param name="SickName"></param>
        /// <param name="indate"></param>
        /// <returns></returns>
        public static DataTable GetZYFareDetail(string years,string zybm)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
               
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 12); 

                ArrayOfString[] zymx;
                zymx = client.traceQueryC(years, getAreaId(""), zybm);
                DataTable dt = new DataTable();
                dt.Columns.Add("lsh");
                dt.Columns.Add("ybbm");
                dt.Columns.Add("mc");
                dt.Columns.Add("sl");
                dt.Columns.Add("dj");
                dt.Columns.Add("je");
                dt.Columns.Add("gg");
                dt.Columns.Add("jx");
                dt.Columns.Add("yblb");
                dt.Columns.Add("rq");
                dt.Columns.Add("bxbl");
                dt.Columns.Add("fylb");
                dt.Columns.Add("dw");
                dt.Columns.Add("xzlx");
                dt.Columns.Add("xzz");
                if (zymx == null)
                {
                    throw new Exception("获取病人住院明细失败！");
                }
                for (int i = 0; i < zymx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["lsh"]= zymx[i][0];
                    r["ybbm"]= zymx[i][2];
                    r["mc"]= zymx[i][3];
                    r["sl"]= zymx[i][9];
                    r["dj"]= zymx[i][8];
                    r["je"]= zymx[i][10];
                    r["gg"]= zymx[i][6];
                    r["jx"]= zymx[i][7];
                    r["yblb"]= zymx[i][12];
                    r["rq"]= zymx[i][14];
                    if (zymx[i][11] != null)
                    {
                        r["bxbl"] = Math.Ceiling(double.Parse(zymx[i][11].ToString())*100) +"%";
                    }
                    r["fylb"]= zymx[i][4];
                    r["dw"]= zymx[i][5];
                    r["xzlx"]= zymx[i][15];
                    r["xzz"]= zymx[i][16];
                    dt.Rows.Add(r);
                }
                return dt;

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


        public static DataTable GetMZFareDetail(string zybm)
        {
            HisInterfacePortTypeClient client = null;
            try
            {

                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 18); 

                ArrayOfString[] zymx;
                zymx = client.traceQueryD(DateTime.Now.Year + "", "", zybm);
                DataTable dt = new DataTable();
                dt.Columns.Add("lsh");
                dt.Columns.Add("ybbm");
                dt.Columns.Add("mc");
                dt.Columns.Add("sl");
                dt.Columns.Add("dj");
                dt.Columns.Add("je");
                dt.Columns.Add("gg");
                dt.Columns.Add("jx");
                dt.Columns.Add("yblb");
                dt.Columns.Add("rq");
                dt.Columns.Add("bxbl");
                dt.Columns.Add("fylb");
                dt.Columns.Add("dw");
                dt.Columns.Add("xzlx");
                dt.Columns.Add("xzz");
                if (zymx == null)
                {
                    throw new Exception("获取病人门诊明细失败！");
                }
                for (int i = 0; i < zymx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["lsh"] = zymx[i][0];
                    r["ybbm"] = zymx[i][1];
                    r["mc"] = zymx[i][3];
                    r["sl"] = zymx[i][9];
                    r["dj"] = zymx[i][8];
                    r["je"] = zymx[i][10];
                    r["gg"] = zymx[i][6];
                    r["jx"] = zymx[i][7];
                    r["yblb"] = zymx[i][12];
                    r["rq"] = zymx[i][13];
                    if (zymx[i][11] != null)
                    {
                        r["bxbl"] = Math.Ceiling(double.Parse(zymx[i][11].ToString()) * 100) + "%";
                    }
                    r["fylb"] = zymx[i][4];
                    r["dw"] = zymx[i][5];
                    //r["xzlx"] = zymx[i][15];
                    //r["xzz"] = zymx[i][16];
                    dt.Rows.Add(r);
                }
                return dt;

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

        public static DataTable GetMZBcDetail(string nhjgcode, string Startdate, string Enddate)  //门诊明细查询
        {

            HisInterfacePortTypeClient client = null;
            try
            {

                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 30); 

                ArrayOfString[] zymx;
                zymx = client.traceQueryE1(DateTime.Now.Year + "", "", nhjgcode, Startdate, Enddate);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");
                dt.Columns.Add("f3");
                dt.Columns.Add("f4");
                dt.Columns.Add("f5");
                dt.Columns.Add("f6");
                dt.Columns.Add("f7");
                dt.Columns.Add("f8");
                dt.Columns.Add("f9");
                dt.Columns.Add("f10");
                dt.Columns.Add("f11");
                dt.Columns.Add("f12");
                dt.Columns.Add("f13");
                dt.Columns.Add("f14");
                dt.Columns.Add("f15");
                dt.Columns.Add("f16");
                if (zymx == null || zymx.Length == 0)
                {
                    throw new Exception("该时间区间没有数据！");
                }
                for (int i = 0; i < zymx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"]= zymx[i][0];
                    r["f1"]= zymx[i][1];
                    r["f2"]= zymx[i][2];
                    r["f3"]= zymx[i][3];
                    r["f4"]= zymx[i][4];
                    r["f5"]= zymx[i][5];
                    r["f6"]= zymx[i][7];
                    r["f7"]= zymx[i][8];
                    r["f8"]= zymx[i][9];
                    r["f9"]= zymx[i][10];
                    r["f10"]= zymx[i][16];
                    r["f11"]= zymx[i][17];
                    r["f12"]= zymx[i][18];
                    r["f13"]= zymx[i][19];
                    r["f14"] = zymx[i][20];
                    r["f15"] = zymx[i][22];
                    r["f16"] = zymx[i][21];
                    dt.Rows.Add(r);
                }
                return dt;

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
        public static DataTable GetZYBcDetail(string nhjgcode,string Startdate, string Enddate)  //住院明细查询
        {
            HisInterfacePortTypeClient client = null;
            try
            {

                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 18); 

                ArrayOfString[] zymx;
                zymx = client.traceQueryI(nhjgcode, Startdate, Enddate);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");
                dt.Columns.Add("f3");
                dt.Columns.Add("f4");
                dt.Columns.Add("f5");
                dt.Columns.Add("f6");
                dt.Columns.Add("f7");
                dt.Columns.Add("f8");
                dt.Columns.Add("f9");
                dt.Columns.Add("f10");
                dt.Columns.Add("f11");
                dt.Columns.Add("f12");
                dt.Columns.Add("f13");
                dt.Columns.Add("f14");
                if (zymx == null)
                {
                    throw new Exception("获取病人住院明细失败！");
                }
                for (int i = 0; i < zymx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"]= zymx[i][0];
                    r["f1"]= zymx[i][1];
                    r["f2"]= zymx[i][2];
                    r["f3"]= zymx[i][3];
                    r["f4"]= zymx[i][4];
                    r["f5"]= zymx[i][5];
                    r["f6"]= zymx[i][6];
                    r["f7"]= zymx[i][9];
                    r["f8"]= zymx[i][10];
                    r["f9"]= zymx[i][11];
                    r["f10"]= zymx[i][12];
                    r["f11"]= zymx[i][13];
                    r["f12"]= zymx[i][14];
                    r["f13"]= zymx[i][15];
                    r["f14"]= zymx[i][8]; 
                    dt.Rows.Add(r);
                }
                return dt;

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



        public static DataTable GetSickBCInfo(string nhylzh)  //查一个家庭的患者补偿信息查询
        {
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 12); 

                ArrayOfString[] zymx;
                zymx = client.traceQueryH(nhylzh);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");
                dt.Columns.Add("f3");
                dt.Columns.Add("f4");
                dt.Columns.Add("f5");
                dt.Columns.Add("f6");
                dt.Columns.Add("f7");
                dt.Columns.Add("f8");
                dt.Columns.Add("f9");
                dt.Columns.Add("f10");
                dt.Columns.Add("f11");
                dt.Columns.Add("f12");
                dt.Columns.Add("f13");
                dt.Columns.Add("f14");
                dt.Columns.Add("f15");
                if (zymx == null || zymx.Length == 0)
                {
                    throw new Exception("没有该农合证号的住院登记信息！");
                }
                for (int i = 0; i < zymx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"] = zymx[i][0];
                    r["f1"] = zymx[i][1];
                    r["f2"] = zymx[i][2];
                    r["f3"] = zymx[i][3];
                    r["f4"] = zymx[i][4];
                    r["f5"] = zymx[i][5];
                    r["f6"] = zymx[i][6];
                    r["f7"] = zymx[i][7];
                    r["f8"] = zymx[i][8];
                    r["f9"] = zymx[i][9];
                    r["f10"] = zymx[i][10];
                    r["f11"] = zymx[i][11];
                    r["f12"] = zymx[i][12];
                    r["f13"] = zymx[i][13];
                    r["f14"] = zymx[i][14];
                    r["f15"] = zymx[i][15];
                    dt.Rows.Add(r);
                }
                return dt;
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
        ///床位包干在院状态登记
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="regno">补偿序号</param>
        /// <param name="sdt">开始时间</param>
        /// <param name="edt">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="operatorcode">操作员编号 </param>
        /// <returns></returns>
        public static string BedBgReg(string nhylzh, string regno, string sdt, string edt, string status,string operatorcode)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
                ArrayOfString ret = null;
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 18); 

                ret = client.traceA(DateTime.Now.Year.ToString(), getAreaId(nhylzh), regno, sdt, edt, status, operatorcode);

                string[] RetArr = ArrayToStrings(ret);
                if (RetArr == null)
                {
                    throw new Exception("床位包干登记失败");
                }
                if (!RetArr[0].Equals("0")) //失败情况   
                {
                    throw new Exception("床位包干登记失败，原因【" + RetArr[1] + "】");

                }
                return ""; 
                
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
        ///床位包干入院状态全删除
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="regno">补偿序号</param>
        /// <returns></returns>
        public static string BedBgRegDel(string nhylzh, string regno)
        {

            HisInterfacePortTypeClient client = null;
            try
            {
                ArrayOfString ret = null;
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 12); 

                ret = client.traceB(DateTime.Now.Year.ToString(), getAreaId(nhylzh), regno);

                string[] RetArr = ArrayToStrings(ret);
                if (RetArr == null)
                {
                    throw new Exception("床位包干入院状态全删除失败");
                }
                if (!RetArr[0].Equals("0")) //失败情况   
                {
                    throw new Exception("床位包干入院状态全删除失败，原因【" + RetArr[1] + "】");
                }
                return "";

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
        ///床位包干在院状态查询
        /// </summary>
        /// <param name="nhylzh"></param>
        /// <param name="regno">补偿序号</param>
        /// <returns></returns>
        public static DataTable GetBedBgData(string nhylzh, string regno)
        {
            HisInterfacePortTypeClient client = null;
            try
            {

                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 8); 
                ArrayOfString[] bgmx;
                bgmx = client.traceQueryE(DateTime.Now.Year.ToString(), getAreaId(nhylzh), regno);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");
                dt.Columns.Add("f3");
                dt.Columns.Add("f4");
                dt.Columns.Add("f5");
                dt.Columns.Add("f6");
                dt.Columns[4].DataType = System.Type.GetType("System.Int32"); 
                if (bgmx == null || bgmx.Length == 0)
                {
                    //throw new Exception("没有床位包干数据！");
                    return null;
                }
                for (int i = 0; i < bgmx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"] = bgmx[i][0];
                    r["f1"] = bgmx[i][1];
                    r["f2"] = bgmx[i][2];
                    r["f3"] = bgmx[i][3];
                    r["f4"] = bgmx[i][4];
                    r["f5"] = bgmx[i][5];
                    r["f6"] = bgmx[i][6];                 
                    dt.Rows.Add(r);
                }
                return dt;
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




        //------------------------------ 各类下载方法 ---------------------------

        /// <summary>
        ///下载诊疗及药品项目
        /// </summary>
        /// <param name="areaid">县区编码</param>
        /// <param name="hospcode">医院编码</param>
        /// <returns></returns>
        public static DataTable DownFareDict(string areaid, string hospcode)
        {
            HisInterfacePortTypeClient client = null;
            try
            { 
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 150); 

                foreach (var op in client.Endpoint.Contract.Operations) 
                { 
                    var dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null) 
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = 65536000 ; 
                    } 
                }

                ArrayOfString[] Dictmx;
                Dictmx = client.traceQueryTargetV(DateTime.Now.Year.ToString(), areaid, hospcode, 1, 100000);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");
                dt.Columns.Add("f3");
                dt.Columns.Add("f4");
                dt.Columns.Add("f5");
                dt.Columns.Add("f6");
                dt.Columns.Add("f7");
                dt.Columns.Add("f8");
                dt.Columns.Add("f9");
                dt.Columns.Add("f10");
                dt.Columns.Add("f11");
                   //dt.Columns[4].DataType = System.Type.GetType("System.Int32"); 

                if (Dictmx == null || Dictmx.Length == 0)
                {
                    throw new Exception("没有需要下载的诊疗药品字典数据！");
                }
                for (int i = 0; i < Dictmx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"] = Dictmx[i][0];  // 项目代码 
                    if (Dictmx[i][1].ToString().Equals("1"))// 保内外（0：保外    1：保内)
                       r["f1"] = "保内";  
                    else
                       r["f1"] = "保外";  

                    r["f2"] = Dictmx[i][2];  // 报补比例
                    r["f3"] = Dictmx[i][3];  // 单价限价
                    r["f4"] = Dictmx[i][4];  // 限制次数

                    if (Dictmx[i][5].IndexOf("<") < 0)
                    {
                        r["f5"]  = Dictmx[i][5];  // 项目名称
                        r["f10"] = Dictmx[i][10].ToLower();  // 拼音码
                    }
                    else
                    {
                        r["f5"] = Dictmx[i][1].Replace("<", "(").Replace(">", ")");  // 项目名称
                        r["f10"] = Dictmx[i][10].Replace("<", "(").Replace(">", ")").ToLower();  // 拼音码
                    }

                    r["f6"] = Dictmx[i][6];  // 规格
                    r["f7"] = Dictmx[i][7];  // 剂型
                    r["f8"] = Dictmx[i][8];  // 单位
                    r["f9"] = Dictmx[i][9];  // 类别
                    
                   /* if (Dictmx[i][10].ToString().Equals(""))
                      r["f10"] = PyWbCode.getPyCode(Dictmx[i][5].ToString());
                    else
                      r["f10"] = Dictmx[i][10].ToLower();  // 拼音简码
                    */

                    if (Dictmx[i][11].ToString().Equals("02"))
                      r["f11"] = "基药";  // 是否基药（02：基药）
                    dt.Rows.Add(r);
                }
                return dt;
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
        /// 下载疾病信息
        /// </summary>
        /// <param name="areaid">县区编码</param>
        /// <returns></returns>
        public static DataTable DownICDdict(string areaid)
        {
            HisInterfacePortTypeClient client = null;
            try
            { 
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 180);
                foreach (var op in client.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = 65536000;
                    }
                }

                ArrayOfString[] Dictmx;
                Dictmx = client.traceQueryTargetZ(DateTime.Now.Year.ToString(), areaid,1, 100000);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");  
                dt.Columns.Add("f1");  
                dt.Columns.Add("f2"); 


                if (Dictmx == null || Dictmx.Length == 0)
                {
                    throw new Exception("没有需要下载的疾病信息数据！");
                }
                for (int i = 0; i < Dictmx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"] = Dictmx[i][0];  // 疾病编码
                    if (Dictmx[i][1].IndexOf("<") < 0)
                    {
                        r["f1"] = Dictmx[i][1];  // 疾病名称
                        r["f2"] = Dictmx[i][2].ToLower();  // 拼音码
                    }
                    else
                    {
                        r["f1"] = Dictmx[i][1].Replace("<","(").Replace(">",")");  // 疾病名称
                        r["f2"] = Dictmx[i][2].Replace("<", "(").Replace(">", ")").ToLower();  // 拼音码
                    }
                    dt.Rows.Add(r);
                }
                return dt;
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
        /// 下载手术字典
        /// </summary>
        /// <param name="areaid">县区编码</param>
        /// <param name="hospcode">机构编码</param>
        /// <returns></returns>
        public static DataTable DownOperationDict(string areaid, string hospcode)
        {
            HisInterfacePortTypeClient client = null;
            try
            {
                client = new HisInterfacePortTypeClient();
                setNHOutTime(client, 150);

                foreach (var op in client.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = 65536000;
                    }
                }

                ArrayOfString[] Dictmx;
                Dictmx = client.traceQueryJ(DateTime.Now.Year.ToString(), areaid, hospcode);
                DataTable dt = new DataTable();
                dt.Columns.Add("f0");
                dt.Columns.Add("f1");
                dt.Columns.Add("f2");


                if (Dictmx == null || Dictmx.Length == 0)
                {
                    throw new Exception("没有需要下载的手术字典数据！");
                }
                for (int i = 0; i < Dictmx.Length; i++)
                {
                    DataRow r = dt.NewRow();
                    r["f0"] = Dictmx[i][0];  // 手术编码

                    if (Dictmx[i][1].IndexOf("<") < 0)
                    {
                        r["f1"] = Dictmx[i][1];  // 手术名称
                        r["f2"] = Dictmx[i][2].ToLower();  // 拼音码
                    }
                    else
                    {
                        r["f1"] = Dictmx[i][1].Replace("<", "(").Replace(">", ")");
                        r["f2"] = Dictmx[i][2].Replace("<", "(").Replace(">", ")").ToLower();  
                    }

                    dt.Rows.Add(r);
                }
                return dt;
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
