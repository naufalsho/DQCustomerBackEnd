using DQCustomer.DataAccess.Repositories;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();


        /*
         * The Unit of Work pattern is used to group one or more operations (usually database operations) into a single transaction
         * or 'unit of work', so that all operations either pass or fail as one.
         */

        IFunnelOpportunityRepository FunnelOpportunityRepository { get; }
        ICustomerSettingRepository CustomerSettingRepository { get; }
        IInvoicingConditionRepository InvoicingConditionRepository { get; }
        IInvoicingScheduleRepository InvoicingScheduleRepository { get; }
        IRelatedCustomerRepository RelatedCustomerRepository { get; }
        IRelatedFileRepository RelatedFileRepository { get; }
        ISalesHistoryRepository SalesHistoryRepository { get; }
        ICustomerSuccessStoryRepository CustomerSuccessStoryRepository { get; }
        IWebsiteSocialMediaRepository WebsiteSocialMediaRepository {get;}
        IAddressOfficeNumberRepository AddressOfficeNumberRepository { get; }
        ICustomerPICRepository CustomerPICRepository { get; }
        ICustomerCardFileRepository CustomerCardFileRepository { get; }

    }
}
