using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RewardItem
/// </summary>
public class RewardItem
{
    private String rewardID;
    private String name;
    private String description;
    private String price;
    private DateTime startDate;
    private DateTime endDate;
    private String quantity;
    private DateTime lastUpdated;
    private String lastUpdatedBy;

    public RewardItem(String name, String description, String price, DateTime startDate, DateTime endDate, String quantity, DateTime lastUpdated, String lastUpdatedBy)
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
    public String RewardID
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
    public String Price
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
    public String Quantity
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
}