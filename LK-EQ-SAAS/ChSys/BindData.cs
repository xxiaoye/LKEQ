using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;

using System.Data;


namespace ChSys
{
    /**==public class BindData
    {
        
        public static void BindByDict(ComboBox bo, int gid,string sel,bool isPy) {
            ObjDao<DictData> dao = new ObjDao<DictData>();
            int i = -1;
            int s = -1;
            IList<DictData> li = dao.Find("dicgrpid=" + gid);
            if (li != null && li.Count > 0) {
                foreach (DictData d in li) {
                    i++;
                    if (d.Dicid.Equals(sel)) {
                        s = i;
                    }
                    if (isPy)
                    d.Dicdesc = d.Pycode + "-" + d.Dicdesc;
                    bo.Items.Add(d);
                
                }
            }
           
            bo.DisplayMember = "dicdesc";
            bo.ValueMember = "dicid";
            bo.SelectedIndex = s;
           
           
        }
        public static void BindByDict(ComboBox bo, int gid, string sel) {
                BindByDict(bo, gid, sel, true);
        }
        public static void BindByDict(ToolStripComboBox bo, int gid,bool isPy)
        {
            ObjDao<DictData> dao = new ObjDao<DictData>();
        
            IList<DictData> li = dao.Find("dicgrpid=" + gid);
            if (li != null && li.Count > 0) {
                foreach (DictData d in li) {
                    if (isPy)
                        d.Dicdesc = d.Pycode + "-" + d.Dicdesc;
                    bo.Items.Add(d.Dicdesc);
                
                }
            }
        }
        
        public static void BindByYaoFang(ComboBox bo, string Choscode, string sel)
        {
            ObjDao<YaoFang> dao = new ObjDao<YaoFang>();
            IList<YaoFang> li = dao.Find("Choscode='" + Choscode+"'");
            int i = -1;
            int s =-1;
            if (li != null && li.Count > 0)
            {
                foreach (YaoFang d in li)
                {
                    i++;
                    if ((d.Id+"").Equals(sel))
                    {
                        s = i;
                    }
                    
                    bo.Items.Add(d);

                }
            }
            bo.SelectedIndex = s;
            bo.DisplayMember = "yfmc";

        }
        /// <summary>
        /// 通过sql绑定下拉列表 绑定对象 SelValue sql的查询语句至少包含2个字段的信息 第一个是 值 第二个是文本
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="sql"></param>
        /// <param name="sel"></param>
        public static void BindBySql(ComboBox bo, string sql, string sel)
        {
            if (sql.IndexOf("'{choscode}'")>0)
                sql = sql.Replace("{choscode}",  His.his.Choscode );
            else
                sql = sql.Replace("{choscode}", "'" + His.his.Choscode + "'");
            DataTable dt = new Dao().find(sql);
            int s = -1;
            int count = dt.Rows.Count;
            if (dt != null && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    SelValue d = new SelValue(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                    if (d.Value.Equals(sel))
                    {
                        s = i;
                    }

                    bo.Items.Add(d);

                }
            }
            bo.SelectedIndex = s;
            bo.DisplayMember = "text";
            bo.ValueMember = "value";

        }
        
    }
    public class SelValue {
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public SelValue(string t, string v) {
            this.text = t;
            this.value = v;
        }
    }**/
}
