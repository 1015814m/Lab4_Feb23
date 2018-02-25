using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Achievement
/// </summary>
public class Achievement
{
    int achievementID;
    string description;
    DateTime date;
    int pointsAmount;
    int employeeID;
    int valueID;
    int recEmployee;
    int applaudID;
    /// <summary>
    /// General constructor for the achievement class
    /// </summary>
    /// <param name="achievementID"></param>
    /// <param name="description"></param>
    /// <param name="date"></param>
    /// <param name="pointsAmount"></param>
    /// <param name="employeeID"></param>
    /// <param name="valueID"></param>
    /// <param name="recEmployee"></param>
    public Achievement(int achievementID, string description, DateTime date, int pointsAmount, int employeeID, int valueID, int recEmployee, int applaudID)
    {
        AchievementID = achievementID;
        Description = description;
        Date = date;
        PointsAmount = pointsAmount;
        EmployeeID = employeeID;
        ValueID = valueID;
        RecEmployee = recEmployee;
        ApplaudID = applaudID;
    }

    public Achievement(int achievementID, string description, DateTime date, int pointsAmount, int employeeID, int valueID, int recEmployee)
    {
        AchievementID = achievementID;
        Description = description;
        Date = date;
        PointsAmount = pointsAmount;
        EmployeeID = employeeID;
        ValueID = valueID;
        RecEmployee = recEmployee;
    }

    public int AchievementID
    {
        get
        {
            return achievementID;
        }
        private set
        {
            achievementID = value;
        }
    }

    public string Description
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

    public DateTime Date
    {
        get
        {
            return date;
        }
        private set
        {
            date = value;
        }
    }

    public int PointsAmount
    {
        get
        {
            return pointsAmount;
        }
        private set
        {
            pointsAmount = value;
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

    public int ValueID
    {
        get
        {
            return valueID;
        }
        private set
        {
            valueID = value;
        }
    }

    public int RecEmployee
    {
        get
        {
            return recEmployee;
        }
        private set
        {
            recEmployee = value;
        }
    }

    public int ApplaudID
    {
        get
        {
            return applaudID;
        }
        private set
        {
            applaudID = value;
        }
    }




}