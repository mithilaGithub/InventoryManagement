using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using InventoryManagement_BP.Models;

namespace InventoryManagement_BP.Controllers
{
    public class ItemMasterController : Controller
    {

        SqlConnection con = new SqlConnection(@"Server=localhost;Database=BPInventoryManagement;Integrated Security = True;");

        //
        // GET: /ItemMaster/

        public ActionResult ItemMasterList()
        {
            return View();
        }
        public ActionResult ItemMasterAdd()
        {
            return View();
        }

        public ActionResult StockTransaction()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ItemMaster_GetAll()
        {
            List<ItemMasterModel> ItemMasterList = new List<ItemMasterModel>();
            ItemMasterModel ItemMaster = new ItemMasterModel();
          
            con.Open();
            String query = "Select * from ItemMaster";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                ItemMaster = new ItemMasterModel();
                ItemMaster.Id = Convert.ToInt32(data.GetValue(0));
                ItemMaster.Name = data.GetValue(1).ToString();
                ItemMaster.Unit = data.GetValue(2).ToString();
                ItemMaster.CurrentStock = Convert.ToInt32(data.GetValue(3));
                ItemMaster.ReorderLevel = Convert.ToInt32(data.GetValue(4));
                ItemMasterList.Add(ItemMaster);
            }
            cmd.Dispose();
            con.Close();
            return Json(ItemMasterList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemMaster_Add(ItemMasterModel ItemMaster)
        {
            con.Open();
            String query = "insert into ItemMaster(Name,Unit,ReorderLevel,CreatedDate) values('" + ItemMaster.Name + "','" + ItemMaster.Unit + "'," + ItemMaster.ReorderLevel + ",'" + ItemMaster.CreatedDate + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            int rows = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemMaster_Update(ItemMasterModel ItemMaster)
        {
            con.Open();
            String query = "update ItemMaster set Name='" + ItemMaster.Name + "',Unit='" + ItemMaster.Unit + "',ReorderLevel=" + ItemMaster.ReorderLevel + ",UpdatedDate='" + DateTime.Now + "' where Id = " + ItemMaster.Id + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            int rows = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ItemMaster_Delete(ItemMasterModel ItemMaster)
        {
            con.Open();
            String query = "delete from ItemMaster where Id =" + ItemMaster.Id;
            SqlCommand cmd = new SqlCommand(query, con);
            int rows = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public int stock(int id, String type)
        {
            List<StockTransactionModel> stockList = new List<StockTransactionModel>();
            StockTransactionModel stock = new StockTransactionModel();

            String queryStock = "Select * from StockTransaction";
            SqlCommand cmdStock = new SqlCommand(queryStock, con);
            SqlDataReader dataStock = cmdStock.ExecuteReader();
            while (dataStock.Read())
            {
                stock = new StockTransactionModel();
                stock.ItemId = Convert.ToInt32(dataStock.GetValue(1));
                stock.Quantity = Convert.ToInt32(dataStock.GetValue(2));
                stock.Type = dataStock.GetValue(3).ToString();
                stockList.Add(stock);
            }
            int total = stockList.Where(x => x.ItemId == id && x.Type == type).Sum(x => x.Quantity);
            cmdStock.Dispose();
            dataStock.Dispose();
            return total;
        }


        [HttpPost]
        public JsonResult StockTransaction_Add(StockTransactionModel Stock)
        {
            con.Open();
            String query = "insert into StockTransaction(ItemId,Quantity,Type,Date) values(" + Stock.ItemId + "," + Stock.Quantity + ",'" + Stock.Type + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            int rows = cmd.ExecuteNonQuery();
            int TotalAdded = stock(Stock.ItemId, "Added");
            int TotalConsumpted = stock(Stock.ItemId, "Consumpted");
            cmd.Dispose();

            String queryTemp = "update ItemMaster set TotalAdded=" + TotalAdded + ",TotalConsumpted=" + TotalConsumpted + ",CurrentStock=" + (TotalAdded - TotalConsumpted) + " where Id = " + Stock.ItemId + ";";
            SqlCommand cmdTemp = new SqlCommand(queryTemp, con);
            int rowsTemp = cmdTemp.ExecuteNonQuery();
            cmdTemp.Dispose();
            con.Close();

            return Json(new { stock = rows, item = rowsTemp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StockReport_GetAll()
        {
            List<ItemMasterModel> ItemMasterList = new List<ItemMasterModel>();
            ItemMasterModel ItemMaster = new ItemMasterModel();
         
            con.Open();
            String query = "Select CurrentStock,ReorderLevel,Name from ItemMaster";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                ItemMaster = new ItemMasterModel();
                ItemMaster.CurrentStock = Convert.ToInt32(data.GetValue(0));
                ItemMaster.ReorderLevel = Convert.ToInt32(data.GetValue(1));
                ItemMaster.Name = data.GetValue(2).ToString();
                ItemMasterList.Add(ItemMaster);
            }
            data.Dispose();
            cmd.Dispose();

            List<StockTransactionModel> stockList = new List<StockTransactionModel>();
            List<StockTransactionModel> stockListMonths = new List<StockTransactionModel>();
            StockTransactionModel stockMonths = new StockTransactionModel();
            StockTransactionModel stock = new StockTransactionModel();
            String queryTemp = "Select Date,Quantity,Type,Name,ItemId from StockTransaction,ItemMaster where ItemMaster.Id = StockTransaction.ItemId ";
            SqlCommand cmdTemp = new SqlCommand(queryTemp, con);
            SqlDataReader dataTemp = cmdTemp.ExecuteReader();
            while (dataTemp.Read())
            {
                stock = new StockTransactionModel();
                stock.Date = Convert.ToDateTime(dataTemp.GetValue(0));
                stock.Quantity = Convert.ToInt32(dataTemp.GetValue(1));
                stock.Type = dataTemp.GetValue(2).ToString();
                stock.ItemName = dataTemp.GetValue(3).ToString();
                stock.ItemId = Convert.ToInt32(dataTemp.GetValue(4));
                stockList.Add(stock);
            }
            var today = DateTime.Now;
            var beforeMonth = DateTime.Now.AddMonths(-1);
         
            stockListMonths = stockList.Where(x => x.Date >= beforeMonth && x.Date <= today).ToList();
            cmdTemp.Dispose();
            con.Close();

            var reorderLevelDataAvlPoints = ItemMasterList.Select(x => new
            {
                y = (x.CurrentStock - x.ReorderLevel > 0 ? x.CurrentStock - x.ReorderLevel : x.CurrentStock),
                label = x.Name,
                color = "Blue"
            }
              );
            var reorderLevelDataminPoints = ItemMasterList.Select(x => new
            {
                y = x.ReorderLevel,
                label = x.Name,
                color = "Red"
            }
             );

            var stockDataPointsTemp = stockListMonths.GroupBy(y => y.ItemId);

            var stockDataPoints = stockDataPointsTemp.Select(x => new
            {
                y = Convert.ToInt32((((double)(x.Where(y => y.Type == "Consumpted").Sum(z => z.Quantity))) / ((double)(x.Where(y => y.Type == "Added").Sum(z => z.Quantity))) * 100)),
                label = x.FirstOrDefault().ItemName,
                color = "Blue"
            }
            );

            return Json(new { ReorderLevelDataAvlPoints = reorderLevelDataAvlPoints, ReorderLevelDataminPoints = reorderLevelDataminPoints, StockDataPoints = stockDataPoints }, JsonRequestBehavior.AllowGet);
        }
    }
}
