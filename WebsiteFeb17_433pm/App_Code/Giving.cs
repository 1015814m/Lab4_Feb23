using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GivePoints
{
    public string name;
    public DateTime activityDate;
    public string activityName;
    public string activityDescription;
    public int pointsGiven;
    public string companyValue;

    public GivePoints(string name, DateTime date, string activityName, DateTime activityDate, string activityDescription, int pointsGiven, string companyValue)
    {
        Name = name;
        Date = activityDate;
        ActivityName = activityName;
        ActivityDescription = activityDescription;
        PointsGiven = pointsGiven;
        Value = companyValue;

    }

    public DateTime Date
    {
        get { return activityDate; }
        private set { activityDate = value; }
    }
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public string ActivityName
    {
        get { return activityName; }
        private set { activityName = value; }
    }

    public string ActivityDescription
    {
        get { return activityDescription; }
        private set { activityDescription = value; }
    }

    public int PointsGiven
    {
        get { return pointsGiven; }
        private set { pointsGiven = value; }
    }

    public string Value
    {
        get { return companyValue; }
        private set { companyValue = value; }
    }
}