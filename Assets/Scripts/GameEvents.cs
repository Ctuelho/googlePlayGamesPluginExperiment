using System.Collections;
using System.Collections.Generic;

public class GameEvents
{

    public delegate void UserLoginAction(bool status);
    public static UserLoginAction UserLoginStatusUpdate;

    public static void UserLoginStatusUpdateFirer(bool status)
    {
        if (UserLoginStatusUpdate != null)
        {
            UserLoginStatusUpdate(status);
        }
    }
}