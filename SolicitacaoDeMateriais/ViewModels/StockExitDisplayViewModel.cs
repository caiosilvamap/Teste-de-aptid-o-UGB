namespace SolicitacaoDeMateriais.ViewModels
{
    public class StockExitDisplayViewModel
    {
        public int Id { get; set; }
        public string ProductEANcode { get; set; }
        public string ProductName { get; set; }
        public string ProductSupplierName { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public int RequisitionQuantity { get; set; }
        public DateTime? CreationDate { get; set; } 
    }
}
