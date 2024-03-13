using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Additional
{
	public class Tr_Initiate
	{
		public string IdISA { get; set; }
		public int EmployeeKey { get; set; }
		public string UserLogin { get; set; }
		public string RevNo { get; set; }
		public string QuotatioNo { get; set; }
		public string CustomerName { get; set; }
		public string ProjectName { get; set; }
		public string TermsOfPayment { get; set; }
		public int? ProjectType { get; set; }
		public int? PreSalesType { get; set; }
		public int? MaintenanceType { get; set; }
		public int? FlagSMO { get; set; }
		public string EmailPIC { get; set; }
		public string PICTelephone { get; set; }
		public int SuccessStory { get; set; }
		public int PMO { get; set; }
		public string SOIDC { get; set; }
		public int FlagSOIDC { get; set; }
		public string BUSOIDC { get; set; }
		public string SARefNo { get; set; }
		public string OiDate { get; set; }
		public string StartProjectDate { get; set; }
		public string EndProjectDate { get; set; }
		public string MaintenanceStartDate { get; set; }
		public string MaintenanceEndDate { get; set; }
		public string BASTDate { get; set; }
		public string DeliverDate { get; set; }
		public string Currency { get; set; }
		public string Sysdate { get; set; }
		public decimal rate { get; set; }
		public int SO { get; set; }
		public string Status { get; set; }
		public int TypeSA { get; set; }
		public int RunRate { get; set; }
		public string BillingTerm { get; set; }
		public int FlagKontrakPayung { get; set; }
		public string ContractNumber { get; set; }
		public int COCODE { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string EndUserCustomerName { get; set; }
		public string OINo { get; set; }
		public decimal? Mandays { get; set; }
		public int? QtyDuration { get; set; }
		public string TypeDuration { get; set; }
	}
}
