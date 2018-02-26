using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RewardItem
/// </summary>
public class RewardItem
{
    private int rewardID;
    private String name;
    private String description;
    private Decimal price;
    private DateTime startDate;
    private DateTime endDate;
    private int quantity;
    private DateTime lastUpdated;
    private String lastUpdatedBy;
    private int providerID;
    private int categoryID;

    public RewardItem(String name, String description, Decimal price, DateTime startDate, DateTime endDate, int quantity, DateTime lastUpdated, String lastUpdatedBy)
    {
        Name = name;
        Description = description;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        Quantity = quantity;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
    }

    public RewardItem(int rewardID,String name, String description, Decimal price, DateTime startDate, DateTime endDate, int quantity, DateTime lastUpdated, String lastUpdatedBy)
    {
        RewardID = rewardID;
        Name = name;
        Description = description;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        Quantity = quantity;
        LastUpdated = lastUpdated;
        LastUpdatedBy = lastUpdatedBy;
    }

    public RewardItem(int rewardID, String name, String description, Decimal price, DateTime startDate, DateTime endDate, int quantity, int providerID, int categoryID)
    {
        RewardID = rewardID;
        Name = name;
        Description = description;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        Quantity = quantity;
        ProviderID = providerID;
        CategoryID = categoryID;
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
    public String Name
    {
        get
        {
            return name;
        }
        private set
        {
            name = value;
        }
    }
    public String Description
    {
        get
        {
            return description;
        }
        private set
        {
            description = value;
        }
    }
    public Decimal Price
    {
        get
        {
            return price;
        }
        private set
        {
            price = value;
        }
    }
    public DateTime StartDate
    {
        get
        {
            return startDate;
        }
        private set
        {
            startDate = value;
        }
    }
    public DateTime EndDate
    {
        get
        {
            return endDate;
        }
        private set
        {
            endDate = value;
        }
    }
    public int Quantity
    {
        get
        {
            return quantity;
        }
        private set
        {
            quantity = value;
        }
    }
    public DateTime LastUpdated
    {
        get
        {
            return lastUpdated;
        }
        private set
        {
            lastUpdated = value;
        }
    }
    public String LastUpdatedBy
    {
        get
        {
            return lastUpdatedBy;
        }
        private set
        {
            lastUpdatedBy = value;
        }
    }

    public int ProviderID
    {
        get
        {
            return providerID;
        }
        private set
        {
            providerID = value;
        }
    }
    public int CategoryID
    {
        get
        {
            return categoryID;
        }
        private set
        {
            categoryID = value;
        }
    }


}