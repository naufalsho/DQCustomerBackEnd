using System;

namespace DQCustomer.BusinessObject
{
    public class CpCustomerSuccessStory
    {
        public long StoryID { get; set; }
        public long FunnelID { get; set; }
        public string Story { get; set; }
        public long CreateUserID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
