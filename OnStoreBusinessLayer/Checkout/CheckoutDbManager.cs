using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;
using OnStoreModels;
using System.Data.Entity.Validation;

namespace OnStoreBusinessLayer.Checkout
{
    public class CheckoutDbManager
    {
        public static long CreateTransaction(User userInfo, string itemData, ItemDataFormat itemDataFormat, TransactionType transactionType, TransactionStatus transactionStatus)
        {
            long transactionId = -1;

            using (var context = new OnStoreEntities())
            {
                Transaction trans = context.Transactions.Create();
                trans.TransactionId = transactionId;
                trans.UserId = userInfo.UserId;
                trans.ItemData = itemData;
                trans.ItemDataFormat = (byte)itemDataFormat;
                trans.Status = (byte)transactionStatus;
                trans.TransactionDate = DateTime.UtcNow;
                trans.TransactionType = (byte)transactionType;
                trans.CreatedDate = DateTime.UtcNow;
                context.Transactions.Add(trans);

                try
                {
                    bool created = context.SaveChanges() > 0;
                    if (created)
                    {
                        transactionId = trans.TransactionId;
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            return transactionId;
        }

        public static Transaction GetTransaction(long transactionId)
        {
            Transaction transaction = null;
            using (var context = new OnStoreEntities())
            {
                transaction = context.Transactions.SingleOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null)
                {
                    return transaction;
                }
            }
            return transaction;
        }

        public static bool UpdateTransactionStatus(long transactionId, TransactionStatus status)
        {
            bool updated = false;

            using (var context = new OnStoreEntities())
            {
                var transaction = context.Transactions.SingleOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null)
                {
                    transaction.Status = (byte)status;
                    transaction.UpdatedDate = DateTime.UtcNow;
                    updated = context.SaveChanges() > 0;
                }
            }

            return updated;
        }

        public static int CreateReceipt(Guid userId, long transactionId, string vendorTransactionId, TransactionType transactionType, TransactionProvider provider, TransactionStatus transactionStatus)
        {
            int receiptId = 0;
            using (var context = new OnStoreEntities())
            {
                PurchaseReceipt receipt = context.PurchaseReceipt.Create();
                receipt.TransactionId = transactionId;
                receipt.UserId = userId;
                receipt.VendorTransactionId = vendorTransactionId;
                receipt.TransactionStatus = (byte)transactionStatus;
                receipt.TransactionProvider = (byte)provider;
                receipt.TransactionType = (byte)transactionType;
                receipt.CreatedDate = DateTime.UtcNow;
                context.PurchaseReceipt.Add(receipt);
                if (context.SaveChanges() > 0)
                {
                    return receipt.PurchaseReceiptId;
                }
            }
            return receiptId;
        }

        public static bool CreateRawReceipt(int receiptId, string rawReceipt)
        {
            bool created = false;
            using (var context = new OnStoreEntities())
            {
                PurchaseRawReceipt receiptData = context.PurchaseRawReceipts.Create();
                receiptData.PurchaseReceiptId = receiptId;
                receiptData.RawReceipt = rawReceipt;
                receiptData.CreatedDate = DateTime.UtcNow;
                context.PurchaseRawReceipts.Add(receiptData);
                created = context.SaveChanges() > 0;
            }
            return created;
        }
    }
}
