using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using WebApplication1.Models;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Web.Services;
using System.Configuration;

namespace WebApplication1.Models
{
    public class ItemData : My_DAO
    {

        public int Search_count { get; set; }
        public string Item_name { get; set; }
        public string Item_price { get; set; }

        

        #region 초기_아이템 정보 넣기
        public int insert_Item_DB(List<ItemData> items)
        {
            MySqlConnection con; con = Get_connection();
            List<string> Rows = new List<string>(); //빠른 삽입을 위한 쿼리문 작성
            StringBuilder Scmd = new StringBuilder("Insert into auction (item_name,item_price) values ");
            //MySqlCommand cmd = new MySqlCommand("Insert into auction item_name, values ({1},{2})", con);
            using (con)
            {
                con.Open();
                for (int i = 0; i < items.Count; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString(items[i].Item_name), MySqlHelper.EscapeString(items[i].Item_price)));
                    
                }
                Scmd.Append(string.Join(",", Rows));
                var a = Rows[1];
                
                

                using (MySqlCommand cmd = new MySqlCommand(Scmd.ToString(), con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }


                con.Close();

                return 1;
            }
        }
        #endregion

        #region 아이템 새로운 열 추가
        public int insert_new_row()
        {
            DateTime time = DateTime.Now; //현재 시간 객체
            string m_time = time.ToString("yy_MM_dd"); //현재 시간 포맷

            int result;
            MySqlConnection con = Get_connection();

            using (con)
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("alter table auction add " + m_time + " varchar(30)" , con); //현재 포맷 형식 row추가

                result = cmd.ExecuteNonQuery();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return result;
        }
        #endregion
        //update auction_tmp set 21_10_18 = 'test' where item_name = 'd';
        #region 새로운 아이템 가격정보 추가
        public int update_item_DB(List<ItemData> items)
        {
            DateTime time = DateTime.Now; //현재 시간 객체
            string m_time = time.ToString("yy_MM_dd"); //현재 시간 포맷

            MySqlConnection con = Get_connection();
            using (con)
            {
                con.Open();

                for (int i = 0; i < items.Count; i++)
                {
                    using (MySqlCommand cmd = new MySqlCommand("update auction set " + m_time + " = '" + items[i].Item_price + "' where item_name = '" + items[i].Item_name + "'" ,con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
                return 1;
        }
        #endregion

        #region Json 비직렬화
        public List<ItemData> get_Json()
        {

            DateTime time = DateTime.Now; //현재 시간 객체
            string m_time = time.ToString("MMdd"); //현재 시간 포맷

            List<ItemData> Items = new List<ItemData>();

            var webClient = new WebClient();
            var json = webClient.DownloadString("C:/Users/HJ/source/repos/WebApplication1/WebApplication1/Scripts/"+m_time+".json"); //JSON 파일 읽어오기
            IDictionary<string, dynamic> model = JsonConvert.DeserializeObject<IDictionary<string, dynamic>>(json); //JSON deserialize

            /*
            var a = model["리넨 옷감"]["mr"];
            var b = model.First().Key;
            var c = model.ElementAt(0).Key;
            var d = model.First()..net mysql sqlclient webconfigValue["mr"]; //key value값
            */

            //Model 객체 Key - Value
            for (int i = 0; i < model.Count; i++)
            {
                ItemData item_data = new ItemData();

                var tmp = model.ElementAt(i);

                item_data.Item_name = tmp.Key;
                item_data.Item_price = tmp.Value["mr"];
                //System.Diagnostics.Trace.WriteLine(i);
                //System.Diagnostics.Trace.WriteLine(tmp.Key);
                Items.Add(item_data);
            }

            return Items;
        }
        #endregion


        #region 모든 아이템 JSON 직렬화
        public List<ItemData> Get_All_items_DB()
        {
           // List<string> item_names = new List<string>();

            

            List<ItemData> items = new List<ItemData>();

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["AHDB_DBConnection"].ToString());

            con.Open();

            using (con)
            {
                

                using (MySqlCommand cmd = new MySqlCommand("select * from auction", con))
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ItemData item = new ItemData
                        {
                            Item_name = rdr["item_name"].ToString()
                        };
                        items.Add(item);
                    }

                    rdr.Close();

                }

                con.Close();
            }

            return items;


        }
        #endregion

        #region Top5 검색어 찾기
        public List<ItemData> Get_Top_List()
        {
            List<ItemData> items = new List<ItemData>();

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["AHDB_DBConnection"].ToString());


            con.Open();

            using (con)
            {
                

                using (MySqlCommand cmd = new MySqlCommand("select * from auction order by count desc limit 5", con))
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ItemData item = new ItemData
                        {
                            Search_count = Convert.ToInt32(rdr["count"]),
                            Item_name = rdr["item_name"].ToString()
                        };
                        items.Add(item);
                    }

                    rdr.Close();
                }

                con.Close();
            }

            return items;
        }
        #endregion

        #region 검색한 아이템 가격 가져오기
        public ItemData Get_Item_Price(string item_name)
        {
            ItemData item = new ItemData();

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["AHDB_DBConnection"].ToString());

            con.Open();

            using (con)
            {
                


                using (MySqlCommand cmd = new MySqlCommand("select * from auction where item_name = '" + item_name +"'", con))
                {
                    
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        item.Item_name = rdr["item_name"].ToString();
                        item.Item_price = rdr["item_price"].ToString();
                    }
                    
                    rdr.Close();

                }
                //조회수 증가
                using (MySqlCommand cmd = new MySqlCommand("update auction set count = count+1 where item_name= '" + item_name + "'" , con))
                {
                    cmd.ExecuteNonQuery();

                }


                con.Close();
            }

            return item;

        }

        #endregion
        #region AJAX -> CS 함수 호출 테스트
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void DBTest()
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["AHDB_DBConnection"].ToString());

            con.Open();

            using (con)
            {
                
                using (MySqlCommand cmd = new MySqlCommand("insert into auction_tmp values ('c' , '123', '234')",con))
                {
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
        #endregion
    }
}