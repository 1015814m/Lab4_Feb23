using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NewUser
/// </summary>
public class NewUser
{
    private string firstName;
    private string lastName;
    private string middleName;
    private string email;
    private string employeeLoginID;
    private string lastUpdatedBy;
    private string lastUpdate;


    public NewUser(string firstName, string lastName, string middleName, string email, string employeeLoginID, string lastUpdatedBy, string lastUpdate)
    {
        setFirstName(firstName);
        setLastName(lastName);
        setMiddleName(middleName);
        setEmail(email);
        setEmployeeLoginID(employeeLoginID);
        setLastUpdatedBy("Sean Marley");
        setLastUpdate(lastUpdate);

    }



    public void setFirstName(string firstName)
    {
        this.firstName = firstName;
    }
    public string getFirstName()
    {
        return this.firstName;
    }
    //------------------------------------------------

    public void setLastName(string lastName)
    {
        this.lastName = lastName;
    }
    public string getLastName()
    {
        return this.lastName;
    }
    //-------------------------------------------------

    public void setMiddleName(string middleName)
    {
        this.middleName = middleName;
    }
    public string getMiddleName()
    {
        return this.middleName;
    }


    //------------------------------------------------------
    public void setEmail(string email)
    {
        this.email = email;
    }
    public string getEmail()
    {
        return this.email;
    }
    //-------------------------------------------------

    public void setEmployeeLoginID(string empLogin)
    {
        this.employeeLoginID = empLogin;
    }
    public string getEmployeeLoginID()
    {
        return this.employeeLoginID;
    }

    //-------------------------------------------------

    public void setLastUpdatedBy(string lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }
    public string getLastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }
    //------------------------------------------------

    public void setLastUpdate(string lastUpdate)
    {
        this.lastUpdate = lastUpdate;
    }
    public string getLastUpdate()
    {
        return this.lastUpdate;
    }
}