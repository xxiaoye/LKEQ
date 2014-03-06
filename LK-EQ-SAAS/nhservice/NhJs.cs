using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using nhservice.orm;

namespace nhservice
{
   public    class NhJs
    {
       /// <summary>
       /// 预计算项目
       /// </summary>
       /// <param name="mxd"></param>
       /// <returns></returns>
       //public static RegJsInfo YJs(string idc10,MenZhenYiZhu brInfo,List<MenZhenYiZhuFzjcMx> mxd,bool iszsjs)
       //{
       //    RegJsInfo yjs = new RegJsInfo();
       //    List<RegisterMx> mxs = new List<RegisterMx>();
       //    foreach (MenZhenYiZhuFzjcMx mx1 in mxd)
       //    {

       //        if (mx1.Nhbm != null && mx1.Nhbm.Trim().Length > 0)
       //        {
       //            RegisterMx ms = new RegisterMx();
       //            ms.Id = mx1.Rkey;
       //            ms.Lb = "01";//类别 
       //            ms.Name = mx1.Nhmc;
       //            ms.Code = mx1.Nhbm;
       //            ms.Dw = mx1.Dw;
       //            ms.Sysj = mx1.Zxsj;
       //            ms.Dj = decimal.Parse(mx1.Dj);
       //            ms.Je = mx1.Hsje;
       //            ms.Sl = mx1.Sl;
       //            mxs.Add(ms);
       //        }
       //    }
       //    if (mxs.Count == 0)
       //        return null;
       //    RegisterInfo info = new RegisterInfo();
       //    info.Jzsj = brInfo.Jzsj;
       //    info.Jbdm = idc10;
       //    info.Grbm = brInfo.Grbm;
       //    info.Nhylzh = brInfo.Nhylzh;
       //    info = NHClient.SubmitMzh(info);
       //    if (info != null)
       //    {
       //        yjs.Reg = info;
       //        string w = NHClient.SubmitMzhMx(info, mxs);
       //        JsXx js = NHClient.Js(info, iszsjs);
       //        yjs.Js = js;
       //    }
       //    return yjs;
       //}
       ///// <summary>
       ///// 预结算药品
       ///// </summary>
       ///// <param name="mxd"></param>
       ///// <param name="iszy"></param>
       ///// <returns></returns>
       //public static RegJsInfo YJs(string idc10, MenZhenYiZhu brInfo, List<MenZhenYiZhuZXCfMx> mxd, bool iszy,bool iszsjs)
       //{
       //    RegJsInfo yjs = new RegJsInfo();
       //    List<RegisterMx> mxs = new List<RegisterMx>();
       //    foreach (MenZhenYiZhuZXCfMx mx1 in mxd)
       //    {
       //        if (mx1.Nhbm != null && mx1.Nhbm.Trim().Length > 0)
       //        {
       //            RegisterMx ms = new RegisterMx();
       //            ms.Id = mx1.Rkey;
       //            ms.Lb = iszy ? "09" : "02";

       //            ms.Name = mx1.Nhmc;
       //            ms.Code = mx1.Nhbm;
       //            ms.Dw = mx1.Dw;
       //            ms.Jx = mx1.Jx;
       //            ms.Gg = mx1.Gg;
       //            ms.Sysj = DateTime.Now;
       //            ms.Dj = decimal.Parse(mx1.Dj);
       //            ms.Je = mx1.Hsje;
       //            ms.Sl = mx1.Sl;
       //            mxs.Add(ms);
       //        }
       //    }
       //    if (mxs.Count == 0)
       //        return null;
       //    RegisterInfo info = new RegisterInfo();
       //    info.Jzsj = brInfo.Jzsj;
       //    info.Jbdm = idc10;
       //    info.Grbm = brInfo.Grbm;
       //    info.Nhylzh = brInfo.Nhylzh;
       //    info = NHClient.SubmitMzh(info);
       //    if (info != null)
       //    {
       //        yjs.Reg = info;
       //        string w = NHClient.SubmitMzhMx(info, mxs);
       //        JsXx js = NHClient.Js(info, iszsjs);
       //        yjs.Js = js;
       //    }
       //    return yjs;
       //}
       
       
      
       
    }
}
