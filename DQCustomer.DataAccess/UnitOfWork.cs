using DQCustomer.DataAccess.Interfaces;
using DQCustomer.DataAccess.Repositories;
using System;
using System.Data;

namespace DQCustomer.DataAccess
{

    public class UnitOfWork : IUnitOfWork
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;

        public UnitOfWork(IDapperContext context)
        {
            this._context = context;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new NullReferenceException("Not finished previous transaction");

            _transaction = _context.db.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new NullReferenceException("Try commit not opened transaction");

            _transaction.Commit();
        }


        /*Below is the implementation of IUnitOfWork that we created earlier*/


        private IFunnelOpportunityRepository funnelOpportunityRepository;
        public IFunnelOpportunityRepository FunnelOpportunityRepository => funnelOpportunityRepository ?? (funnelOpportunityRepository = new FunnelOpportunityRepository(_transaction, _context));
        private ICustomerSettingRepository customerSettingRepository;
        public ICustomerSettingRepository CustomerSettingRepository => customerSettingRepository ?? (customerSettingRepository = new CustomerSettingRepository(_transaction, _context));
        public IInvoicingConditionRepository invoicingConditionRepository;
        public IInvoicingConditionRepository InvoicingConditionRepository => invoicingConditionRepository ?? (invoicingConditionRepository = new InvoicingConditionRepository(_transaction, _context));
        public IInvoicingScheduleRepository invoicingScheduleRepository;
        public IInvoicingScheduleRepository InvoicingScheduleRepository => invoicingScheduleRepository ?? (invoicingScheduleRepository = new InvoicingScheduleRepository(_transaction, _context));
        public IRelatedCustomerRepository relatedCustomerRepository;
        public IRelatedCustomerRepository RelatedCustomerRepository => relatedCustomerRepository ?? (relatedCustomerRepository = new RelatedCustomerRepository(_transaction, _context));
        public IRelatedFileRepository relatedFileRepository;
        public IRelatedFileRepository RelatedFileRepository => relatedFileRepository ?? (relatedFileRepository = new RelatedFileRepository(_transaction, _context));
        public ISalesHistoryRepository salesHistoryRepository;
        public ISalesHistoryRepository SalesHistoryRepository => salesHistoryRepository ?? (salesHistoryRepository = new SalesHistoryRepository(_transaction, _context));
        public ICustomerSuccessStoryRepository customerSuccessStoryRepository;
        public ICustomerSuccessStoryRepository CustomerSuccessStoryRepository => customerSuccessStoryRepository ?? (customerSuccessStoryRepository = new CustomerSuccessStoryRepository(_transaction, _context));
        public IAddressOfficeNumberRepository addressOfficeNumberRepository;
        public IAddressOfficeNumberRepository AddressOfficeNumberRepository => addressOfficeNumberRepository ?? (addressOfficeNumberRepository = new AddressOfficeNumberRepository(_transaction, _context));
        public ICustomerPICRepository customerPICRepository;
        public ICustomerPICRepository CustomerPICRepository => customerPICRepository ?? (customerPICRepository = new CustomerPICRepository(_transaction, _context));
        public ICustomerCardFileRepository customerCardFileRepository;
        public ICustomerCardFileRepository CustomerCardFileRepository => customerCardFileRepository ?? (customerCardFileRepository = new CustomerCardFileRepository(_transaction, _context));
    }

}
