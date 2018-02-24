using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public class Posts
{
    private int postID;
    private DateTime postTime;
    private int likes;
    private int achievementID;
    private int transactionID;
    /// <summary>
    /// For creating a post
    /// </summary>
    /// <param name="postID"></param>
    /// <param name="post"></param>
    /// <param name="postTime"></param>
    /// <param name="likes"></param>
    /// <param name="employeeID"></param>
    /// <param name="achievementID"></param>
    public Posts(int postID, DateTime postTime, int likes, int achievementID, int transactionID)
    {
        PostID = postID;
        PostTime = postTime;
        Likes = likes;
        AchievementID = achievementID;
        TransactionID = transactionID;
    }

    public int PostID
    {
        get
        {
            return postID;
        }
        private set
        {
            postID = value;
        }
    }

    public DateTime PostTime
    {
        get
        {
            return postTime;
        }
        private set
        {
            postTime = value;
        }
    }

    public int Likes
    {
        get
        {
            return likes;
        }
        private set
        {
            likes = value;
        }
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

}