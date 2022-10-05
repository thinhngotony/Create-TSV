using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSVProject
{
    public partial class TSV_Export : Form
    {
        public TSV_Export()
        {
            InitializeComponent();
        }

        public static List<string> headerNames = new List<string> {
            "店舗コード"
            ,"POSレジ端末NO"
            , "加盟店情報1"
            , "加盟店情報2"
            , "店舗端末ID"
            , "端末番号"
            , "ブランドコード"
            , "伝票番号"
            , "ご利用日時"
            , "カード番号"
            , "シーケンス番号"
            , "業務"
            , "金額"
            , "取引状態"
            , "最終決済状態"
        };

        public string storeCode = "";
        public string setting_pos_no = "";


        public class itemsList
        {
            public string storeCode { get; set; }
            public string setting_pos_no { get; set; }
            public string merchInfo1 { get; set; }
            public string merchInfo2 { get; set; }
            public string storeTerminalID { get; set; }
            public string terminalNumber { get; set; }
            public string brandCode { get; set; }
            public string slipNo { get; set; }
            public string useDateTime { get; set; } //Regardless of brand, standardize to "YYYY-MM-DD hh:mm:ss" format
            public string cardNumber { get; set; }
            public string seqNo { get; set; }
            public string biz { get; set; }
            public string tradeAmount { get; set; }
            public string tradeStatus { get; set; } // 01: established, 02: unfinished
            public string settlementStatus { get; set; } //Final settlement status at the time of sales or sales cancellation*1
        }

        //string file_path = @"C:\Users\Tony\source\repos\TSVProject\bin\Debug\";

        public static string setName(string storeCode, string setting_pos_no)
        {
            DateTime dateTime = DateTime.UtcNow;
            string formatDateTime = dateTime.ToString("yyyyMMdd");
            string formatTime = dateTime.ToString("HHmmss");
            string name = string.Format("EMoney_tran_list_{0}_{1}_{2}_{3}.tsv", storeCode, setting_pos_no, formatDateTime, formatTime);
            return name;
        }

        public static void exportTSV(string file_path)
        {
            itemsList dataFake = new itemsList();
            dataFake.storeCode = "0236";
            dataFake.setting_pos_no = "09";
            dataFake.merchInfo1 = "Urakawa Bookstore";
            dataFake.merchInfo2 = "044-921-2532";
            dataFake.storeTerminalID = "JE10710768089";
            dataFake.terminalNumber = "49712-560-63157";
            dataFake.brandCode = "Suica";
            dataFake.slipNo = "00471";
            dataFake.useDateTime = "2022-12-13 04:22:13";
            dataFake.cardNumber = "123456789012345";
            dataFake.seqNo = "4012";
            dataFake.biz = "1";
            dataFake.tradeAmount = "880";
            dataFake.tradeStatus = "02";
            dataFake.settlementStatus = "01";

            itemsList dataFake2 = new itemsList();
            dataFake2.storeCode = "0236";
            dataFake2.setting_pos_no = "09";
            dataFake2.merchInfo1 = "Urakawa Bookstore";
            dataFake2.merchInfo2 = "044-921-2532";
            dataFake2.storeTerminalID = "JE10710768089";
            dataFake2.terminalNumber = "49712-560-63157";
            dataFake2.brandCode = "Suica";
            dataFake2.slipNo = "00471";
            dataFake2.useDateTime = "2022-12-13 04:22:13";
            dataFake2.cardNumber = "123456789012345";
            dataFake2.seqNo = "4012";
            dataFake2.biz = "1";
            dataFake2.tradeAmount = "880";
            dataFake2.tradeStatus = "02";
            dataFake2.settlementStatus = "01";

            DataTable tempData = parseDataList(new List<itemsList> { dataFake, dataFake2 });     
            UpdateLocalTSV(file_path, dataFake.storeCode, dataFake.setting_pos_no, tempData);
        }

        public static DataTable parseDataList(List<itemsList> itemsList)
        {
            DataTable dt = new DataTable();
            foreach (string name in headerNames)
            {
                dt.Columns.Add(name);
            }
            
            foreach (var item in itemsList)
            {
                dt.Rows.Add(
                  item.storeCode
                , item.setting_pos_no
                , item.merchInfo1
                , item.merchInfo2
                , item.storeTerminalID
                , item.terminalNumber
                , item.brandCode
                , item.slipNo
                , item.useDateTime
                , item.cardNumber
                , item.seqNo
                , item.biz
                , item.tradeAmount
                , item.tradeStatus
                , item.settlementStatus);
            }
            return dt;
        }
        public static string checkExistTSV(string path, string storeCode, string setting_pos_no)
        {
        
         DirectoryInfo di = new DirectoryInfo(path);
            DateTime dateTime = DateTime.UtcNow;
            string name = setName(storeCode, setting_pos_no);
            string formatDateTime = dateTime.ToString("yyyyMMdd");

            FileInfo[] itemFile = di.GetFiles(string.Format("EMoney_tran_list_{0}_{1}_{2}_*.tsv", storeCode, setting_pos_no, formatDateTime));

            if (itemFile.Length == 0)
            {
                return "";
            } 

            foreach (var fi in itemFile)
            {

                if (name.Substring(0, name.LastIndexOf("_")) == fi.Name.Substring(0, fi.Name.LastIndexOf("_")))
                {
                    return fi.Name;               
                }
            }
            return "";
                      
        }

        public static void UpdateLocalTSV(string file_path, string storeCode, string setting_pos_no, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            //Handle write header only first TSV
            string name = checkExistTSV(file_path, storeCode, setting_pos_no);

            if (name == "")
            {
                name = setName(storeCode, setting_pos_no);
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join("\t", columnNames));
            }


            //write data body
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"");
                sb.AppendLine(string.Join("\t", fields));
            }

            File.AppendAllText(name.ToString(), sb.ToString().Replace("\r",""), Encoding.UTF8);

        }


        private void exportFunc(object sender, EventArgs e)
        {
            exportTSV(@"C:\Users\Tony\source\repos\TSVProject\bin\Debug\");
        }
    }
}
