using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.ViewModel;
using DQGeneric.BusinessObject;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace DQCustomer.BusinessLogic.Services
{
    public class GenericAPI : IGenericAPI
    {
        private string base_url;
        public GenericAPI(string url)
        {
            base_url = url;
        }

        #region invoke
        
        private string Invoke(string method, string uri, string body)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = delegate { return true; },
            };
            using (var httpClient = new HttpClient(handler))
            {
                //Set Basic Auth
                var user = "abughaizan";
                var password = "4pl!k#sIT3Am";
                var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", user, password)));
                //var tokentest = BuildJWTToken();

                int _timeOutSec = 90;
                string _contentType = "application/json";
                string _uri = base_url + "/api/DQGenericService/" + uri;
                //string _uri = base_url + "/api/" + uri;

                httpClient.BaseAddress = new Uri(_uri);
                httpClient.Timeout = new TimeSpan(0, 0, _timeOutSec);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokentest);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokentest);
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Funnel", "1.0")));
                httpClient.MaxResponseContentBufferSize = 1024 * 1024 * 8;

                HttpResponseMessage response = new HttpResponseMessage();
                HttpMethod _method = new HttpMethod(method);
                switch (_method.ToString().ToUpper())
                {
                    case "GET":
                    case "HEAD":
                        response = httpClient.GetAsync(_uri).Result;
                        break;
                    case "POST":
                        {
                            HttpContent _body = new StringContent(body);
                            _body.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                            response = httpClient.PostAsync(_uri, _body).Result;
                        }
                        break;
                    case "PUT":
                        {
                            HttpContent _body = new StringContent(body);
                            _body.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                            response = httpClient.PutAsync(_uri, _body).Result;
                        }
                        break;
                    case "DELETE":
                        response = httpClient.DeleteAsync(_uri).Result;
                        break;
                    default:
                        break;
                }
                response.EnsureSuccessStatusCode();
                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }

        #endregion

        
        private string DownloadString(string uri)
        {
            return Invoke("GET", uri, string.Empty);
        }

        private string DownloadStringUpdate(string uri, string body)
        {
            return Invoke("PUT", uri, body);
        }
        private string DownloadStringPost(string uri, string body)
        {
            return Invoke("POST", uri, body);
        }
		

        public string GetCustomerName(long customerGenID)
        {
            string result = DownloadString(string.Format("Customer/genid={0}", customerGenID));
            var customer = JsonConvert.DeserializeObject<Customer>(result);
            if (customer == null)
                return string.Empty;
            return customer.CustomerName;
        }

        public string GetEmployeeName(int employeeID)
        {
            string result = DownloadString(string.Format("Employee/{0}", employeeID));
            var customer = JsonConvert.DeserializeObject<Employee>(result);
            if (customer != null)
                return customer.EmployeeName;

            return string.Empty;
        }

        public Employee GetByDeptID(string deptID)
        {
            string result = DownloadString(string.Format("Employee/deptID={0}", deptID));
            var emp = JsonConvert.DeserializeObject<Employee>(result);
            return emp;
        }

        public Employee GetByEmployeeID(int employeeID)
        {
            string result = DownloadString(string.Format("Employee/{0}", employeeID));
            var emp = JsonConvert.DeserializeObject<Employee>(result);
            return emp;
        }

        public string GetStatusFunnelByID(long ID)
        {
            string result = DownloadString(string.Format("FunnelStatus/{0}", ID));
            var customer = JsonConvert.DeserializeObject<Udc>(result);
            if (customer == null)
                return string.Empty;
            return customer.Text1;
        }

        public List<int> GetListSalesFunnelBySalesID(int ID)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy/{0}", ID));
            List<int> listBawahan = JsonConvert.DeserializeObject<List<int>>(result);
            return listBawahan;
        }

        public long GetIdBrandByName(string BrandName)
        {
            string result = DownloadString(string.Format("Brand/BrandName={0}", BrandName));
            long Brand = JsonConvert.DeserializeObject<long>(result);
            return Brand;
        }

        public long GetIdSubBrandByName(long BrandID, string SubBrandName)
        {
            string result = DownloadString(string.Format("Brand/{0}/SubBrandName={1}", BrandID, SubBrandName));
            long SubBrand = JsonConvert.DeserializeObject<long>(result);
            return SubBrand;
        }

        public List<Employee> GetListSubordinate(string email)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy/DQ?operationtype=subordinate&accountname={0}", email));
            List<Employee> listBawahan = JsonConvert.DeserializeObject<List<Employee>>(result);
            return listBawahan;
        }

        public List<Employee> GetListSubordinateAll(string email)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy/DQAll?operationtype=subordinate&accountname={0}", email));
            List<Employee> listBawahan = JsonConvert.DeserializeObject<List<Employee>>(result);
            return listBawahan;
        }

        public Udc GetPresales(string deptID)
        {
            Udc presales = new Udc();
            if (!string.IsNullOrEmpty(deptID))
            {
                string result = DownloadString(string.Format("PresalesSupport/deptID={0}", deptID));
                presales = JsonConvert.DeserializeObject<Udc>(result);
                return presales;
            }
            return presales;
        }

        public Employee GetPMOInfo(long salesID, long customerID)
        {
            string result = DownloadString(string.Format("PMO/PMOInfo?salesID={0}&customerID={1}", salesID, customerID));
            Employee pmoLead = JsonConvert.DeserializeObject<Employee>(result);
            return pmoLead;
        }

        public Employee GetSMOInfo(long salesID, string deptID)
        {
            string result = DownloadString(string.Format("SMO?salesID={0}&deptID={1}", salesID, deptID));
            Employee smoLead = JsonConvert.DeserializeObject<Employee>(result);
            return smoLead;
        }

        public Employee GetPresalesLeader(long salesID, string deptID)
        {
            string result = DownloadString(string.Format("PresalesSupport?salesID={0}&deptID={1}", salesID, deptID));
            Employee presalesLead = JsonConvert.DeserializeObject<Employee>(result);
            return presalesLead;
        }

        public Employee GetProductManager(int itemID)
        {
            string result = DownloadString(string.Format("Brand/GetProductManager?brandID={0}", itemID));
            Employee presalesLead = JsonConvert.DeserializeObject<Employee>(result);
            return presalesLead;
        }
		
		public string GetDocType(int documentTypeID)
        {
            string result = DownloadString(string.Format("DocType"));
            List<CollectionData> colData = JsonConvert.DeserializeObject<List<CollectionData>>(result);
            var res = colData.Where(item => item.ValueData == documentTypeID.ToString()).ToList();
            if (res.Count == 0)
                return null;
            else
                return res.First().TextData;
        }

        public string GetPathFunnelAttachment()
        {
            string resultObj = DownloadString(string.Format("FilePath/FunnelAttachment"));
            CollectionData colData = JsonConvert.DeserializeObject<CollectionData>(resultObj);
            if (colData != null)
                return colData.TextData;
            return string.Empty;
        }

        public Udc GetUdcByID(int UDCID)
        {
            string result = DownloadString(string.Format("Udc/{0}", UDCID));
            Udc udc = JsonConvert.DeserializeObject<Udc>(result);
            return udc;
        }

        public FileCustomerCard GetFileCustomerCard(string fileID)
        {
            string result = DownloadString(string.Format("FileCustomerCard/Id={0}", fileID));
            FileCustomerCard file = JsonConvert.DeserializeObject<FileCustomerCard>(result);
            return file;
        }

        public string GetCustomerPICName(long customerPICID)
        {
            string result = DownloadString(string.Format("CustomerPIC/customerPICID={0}", customerPICID));
            CustomerPIC file = JsonConvert.DeserializeObject<CustomerPIC>(result);
            if (file == null)
                return string.Empty;
            else
                return file.PICName;
        }

        public string GetDocumentType(long udcID)
        {
            string result = DownloadString(string.Format("Udc/{0}?entryKey=DocumentType", udcID));
            Udc file = JsonConvert.DeserializeObject<Udc>(result);
            if (file == null)
                return string.Empty;
            else
                return file.Text1;
        }

        public Employee GetByEmail(string email)
        {
            string result = DownloadString(string.Format("Employee/{0}", email));
            Employee employee = JsonConvert.DeserializeObject<Employee>(result);
            return employee;
        }
		public void UpdateCustomerPIC(CustomerPIC obj)
        { 

            var body = JsonConvert.SerializeObject(obj);
            DownloadStringUpdate(string.Format("CustomerPIC/UpdateAddFunnel"), body);
        }
		
		public List<Tr_Initiate> GetSA(string contractNumber)
        {
            string result = DownloadString(string.Format("ITDEV_SA/quotationNo={0}", Uri.EscapeDataString(contractNumber)));
            List<Tr_Initiate> file = JsonConvert.DeserializeObject<List<Tr_Initiate>>(result);
            return file;
        }

        public Udc GetPMOS(int Id)
        {
            string result = DownloadString(string.Format("Udc/GetPMOS/Id={0}", Id));
            Udc file = JsonConvert.DeserializeObject<Udc>(result);
            return file;
        }

        public List<Udc> GetListPMOS(int Id)
        {
            string result = DownloadString(string.Format("Udc/GetListPMOS/Id={0}", Id));
            List<Udc> file = JsonConvert.DeserializeObject<List<Udc>>(result);
            return file;
        }

        public Employee GetByEmployeeName(string item)
        {
            string result = DownloadString(string.Format("Employee/GetByEmployeeName/EmpName={0}", item));
            Employee employee = JsonConvert.DeserializeObject<Employee>(result);
            return employee;
        }

        public List<Employee> GetDirectSubordinate(string email)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy/Direct?operationtype=subordinate&accountname={0}", email));
            List<Employee> listBawahan = JsonConvert.DeserializeObject<List<Employee>>(result);
            return listBawahan;
        }

        public SoftwareTools GetBySoftwareID(string item)
        {
            string result = DownloadString(string.Format("Software/SoftwareToolID={0}", item));
            SoftwareTools software = JsonConvert.DeserializeObject<SoftwareTools>(result);
            return software;
        }

        public long? GetCustomerByName(string CustName)
        {
            var body = JsonConvert.SerializeObject(new { CustName = CustName });
            string result = DownloadStringPost(string.Format("Customer/SearchCustomer?"), body);
            List<Customer> customer = JsonConvert.DeserializeObject<List<Customer>>(result);
            if(customer.Count > 0)
            {
                var cust = customer.Where(c => c.CustomerName.Contains(CustName)).OrderBy(x => x.CustomerName).First();
                return cust.CustomerGenID;
            }
            else
            {
                return 0;
            }
        }

        public int? GetSalesByLastSA(string customerName, string direktorat)
        {
            string result = DownloadString(string.Format("Employee/GetSalesByLastSA?Customer={0}&Direktorat={1}", customerName, direktorat));
            Employee emp = JsonConvert.DeserializeObject<Employee>(result);
            if (emp != null) {
                if (emp.EmployeeID != 0)
                {
                    return emp.EmployeeID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public List<Udc> GetByEntryKey(string entryKey)
        {
            string result = DownloadString(string.Format("Udc/GetByEntryKey/{0}", entryKey));
            List<Udc> file = JsonConvert.DeserializeObject<List<Udc>>(result);
            return file;
        }

        public List<CollectionData> DropdownEmployee(string cust, string direktoratID)
        {
            string result = DownloadString(string.Format("Employee/DropdownEmployee?cust={0}&direktoratID={1}", cust, direktoratID));
            List<CollectionData> emp = JsonConvert.DeserializeObject<List<CollectionData>>(result);
            return emp;
        }

        public long GetDirektoratByName(string direktorat)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy/GetDirektoratIDByName?direktorat={0}", direktorat));
            long output = JsonConvert.DeserializeObject<long>(result);
            return output;
        }

        public List<EmployeeHierarcy> GetSuperior(string email)
        {
            string result = DownloadString(string.Format("EmployeeHierarcy?operationtype=superior&accountname={0}", email));
            List<EmployeeHierarcy> output = JsonConvert.DeserializeObject<List<EmployeeHierarcy>>(result);
            return output;
        }

        public Employee GetAllByEmployeeID(int value)
        {
            string result = DownloadString(string.Format("Employee/GetAllByEmployeeID/{0}", value));
            var emp = JsonConvert.DeserializeObject<Employee>(result);
            return emp;
        }
        public Customer GetCustomerByID(long customerGenID)
        {
            string result = DownloadString(string.Format("Customer/genid={0}", customerGenID));
            var customer = JsonConvert.DeserializeObject<Customer>(result);
            return customer;
        }

        public string GetWorkflowID(string appsModul, string process, string userLogin, decimal amount)
        {
            string result = DownloadString(string.Format("Worfklow/GetWorkflowID?appsModul={0}&process={1}&userLogin={2}&amount={3}", appsModul, process, userLogin, amount));
            var customer = JsonConvert.DeserializeObject<string>(result);
            return customer;
        }

        public string GenerateWorkflowDefault(long workflowHeaderGenID, string creator, string appsNumber)
        {
            var body = JsonConvert.SerializeObject(new { workflowHeaderGenID = workflowHeaderGenID, creator = creator, appsNumber = appsNumber });
            string result = DownloadStringPost(string.Format("Worfklow/GenerateWorkflowDefault"), body);
            var customer = JsonConvert.DeserializeObject<string>(result);
            return customer;
        }

        public string CheckCustomerBlacklist(string customerName)
        {
            string result = DownloadString(string.Format("Customer/CheckCustomerBlakclist?customerName={0}", customerName));
            var customer = JsonConvert.DeserializeObject<string>(result);
            return customer;
        }

        public void InsertUdc(Udc obj)
        {
            var body = JsonConvert.SerializeObject(obj);
            DownloadStringPost(string.Format("Udc"), body);
        }

        public void UpdateUdc(Udc obj)
        {
            var body = JsonConvert.SerializeObject(obj);
            DownloadStringUpdate(string.Format("Udc"), body);
        }

        public List<Employee> DropdownSalesAdvanceSearch(string employeeEmail)
        {
            string result = DownloadString(string.Format("Employee/DropdownSalesAdvanceSearch?email={0}", employeeEmail));
            List<Employee> output = JsonConvert.DeserializeObject<List<Employee>>(result);
            return output;
        }

        public Tr_Initiate GetSAByIDSA(string linkTo)
        {
            string result = DownloadString(string.Format("ITDEV_SA/idSA={0}", Uri.EscapeDataString(linkTo)));
            Tr_Initiate file = JsonConvert.DeserializeObject<Tr_Initiate>(result);
            return file;
        }

        public Employee GetAllByEmployeeName(string value)
        {
            string result = DownloadString(string.Format("Employee/GetByEmployeeName/EmpName={0}", value));
            var emp = JsonConvert.DeserializeObject<Employee>(result);
            return emp;
        }
		
		public Tr_Initiate GetSAByIDSAMSG(string linkTo)
        {
            string result = DownloadString(string.Format("ITDEV_SA/idSAMsg={0}", Uri.EscapeDataString(linkTo)));
            Tr_Initiate file = JsonConvert.DeserializeObject<Tr_Initiate>(result);
            return file;
        }

        public List<CollectionData> GetDocTypeInvoiceCategory()
        {
            string result = DownloadString(string.Format("DocType/InvoiceCategory"));
            List<CollectionData> docTypeList = JsonConvert.DeserializeObject<List<CollectionData>>(result);
            return docTypeList;
        }
    }
}
