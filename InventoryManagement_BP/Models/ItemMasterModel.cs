using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement_BP.Models
{
    public class ItemMasterModel
    {
        public ItemMasterModel()
        {

        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Unit { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}