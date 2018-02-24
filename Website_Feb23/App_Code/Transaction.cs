using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Transaction
/// </summary>
public class Transaction
{
    int transactionID;
    Decimal cost;
    DateTime purchaseTime;
    int employeeID;
    int rewardID;
    public Transaction(int transactionID, Decimal cost, DateTime purchaseTime, int employeeID, int rewardID)
    {
        TransactionID = transactionID;
        Cost = cost;
        PurchaseTime = purchaseTime;
        EmployeeID = employeeID;
        RewardID = rewardID;
    }

    public int TransactionID
    {
        get
        {
            return transactionID;
        }
        private set
        {
            transactionID = value;
        }
    }

    public Decimal Cost
    {
        get
        {
            return cost;
        }
        private set
        {
            cost = value;
        }
    }

    public DateTime PurchaseTime
    {
        get
        {
            return purchaseTime;
        }
        private set
        {
            purchaseTime = value;
        }
    }

    public int EmployeeID
    {
        get
        {
            return employeeID;
        }
        private set
        {
            employeeID = value;
        }
    }

    public int RewardID
    {
        get
        {
            return rewardID;
        }
        private set
        {
            rewardID = value;
        }
    }


}