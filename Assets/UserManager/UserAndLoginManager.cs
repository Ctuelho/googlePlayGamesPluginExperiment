using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserAndLoginManager : MonoBehaviour {

    public static UserAndLoginManager Instance;

    private FirebaseAuth auth;
    private DatabaseReference db;

    private bool hasUser = false;

    public Text loadingText;

    // Use this for initialization
    void Start () {

        loadingText.text = "user and login manager initialized";
        //Debug.Log("user and login manager initialized");

        if (Instance == null) Instance = this;
        loadingText.text = "instance selected";

        PlayGamesPlatform.DebugLogEnabled = true;

        loadingText.text = "playgames debug enabled";

        PlayGamesPlatform.Activate();

        loadingText.text = "playgames activate";

        Instance.AuthUser();

        //SceneManager.LoadScene(1);

        //GameEvents.UserLoginStatusUpdateFirer(false);
    }

    public bool HasUser
    {
        set
        {
            if(Instance != null) Instance.hasUser = value;
        }
        get
        {
            return Instance != null ? Instance.hasUser : false;
        }
    }

    bool GetFireBaseAuthInstance()
    {
        if (Instance.auth == null)
        {
            if (FirebaseAuth.DefaultInstance != null)
            {
                Instance.auth = FirebaseAuth.DefaultInstance;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    bool AuthPlayGamesInstance()
    {
        bool authed = false;
        PlayGamesPlatform.Instance.Authenticate((bool success) => {
            authed = success;
            if (success)
            { //this works just fine.
                if (loadingText != null) loadingText.text = "Google Play logged in!";
                Debug.Log("Google Play logged in!");
            }
            else
            {
                if (loadingText != null) loadingText.text = "Google Play login Failed!";
                Debug.Log("Google Play login Failed!");
            }
        });
        return authed;
    }

    bool SignInInFirebaseWithGoogleCredential(string googleIdToken)
    {
        bool authed = false;
        if (googleIdToken != "")
        {
            if (loadingText != null) loadingText.text = "local user authed, now trying the credential";
            Debug.Log("local user authed, now trying the credential");

            Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                authed = task.IsCompleted;
                if (task.IsCanceled)
                {
                    if (loadingText != null) loadingText.text = "task was canceled";
                    Debug.Log("task was canceled");
                    authed = false;
                    //GameEvents.UserLoginStatusUpdateFirer(false);
                }
                else if (task.IsFaulted)
                {
                    if (loadingText != null) loadingText.text = "task is faulted";
                    Debug.Log("task is faulted");
                    //GameEvents.UserLoginStatusUpdateFirer(false);
                }
                else
                {
                    if (loadingText != null) loadingText.text = "allright";
                    Debug.Log("allright");
                }
            });
        }
        return authed;
    }

    bool AuthSocialLocalUser()
    {
        bool authed = false;
        Social.localUser.Authenticate((bool success) =>
        {
            authed = success;
            if (success)
            {
                if (loadingText != null) loadingText.text = "Playservices user authed";
                Debug.Log("Playservices user authed");
            }
            else
            {
                if (loadingText != null) loadingText.text = "Failed to log in";
                Debug.Log("Failed to log in");
            }
        });
        return authed;
    }

    // Update is called once per frame
    public void AuthUser()
    {
        Instance.hasUser = false;
        string googleIdToken = "";

        if (Social.localUser.authenticated)
        {
            if (GetFireBaseAuthInstance())
            {
                googleIdToken = PlayGamesPlatform.Instance.GetIdToken();
                if (SignInInFirebaseWithGoogleCredential(googleIdToken))
                {
                    LoadPlayerDB();
                    if (AuthSocialLocalUser())
                    {
                        Instance.hasUser = true;
                        Debug.Log("1");
                    }
                }
            }
        }
        else if (GetFireBaseAuthInstance())
        {
            if (AuthPlayGamesInstance())
            {
                googleIdToken = PlayGamesPlatform.Instance.GetIdToken();
                if (SignInInFirebaseWithGoogleCredential(googleIdToken))
                {
                    LoadPlayerDB();
                    if (AuthSocialLocalUser())
                    {
                        Instance.hasUser = true;
                        Debug.Log("1");
                    }
                }
            }
        }
        
        Debug.Log("2");
        GameEvents.UserLoginStatusUpdateFirer(Instance.hasUser);
    }

    public void SignOut()
    {
        //auth.SignOut();
        PlayGamesPlatform.Instance.SignOut();
        GameEvents.UserLoginStatusUpdateFirer(false);
    }

    private void LoadPlayerDB()
    {
        Instance.db = FirebaseDatabase.DefaultInstance.GetReference("Skins");
    }
}
