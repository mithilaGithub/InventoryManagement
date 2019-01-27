using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement_BP.Models
{
    public class StockTransactionModel
    {
        public StockTransactionModel()
        {

        }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public String ItemName { get; set; }
        public int Quantity { get; set; }
        public String Type { get; set; }
        public DateTime Date { get; set; }

    }
}