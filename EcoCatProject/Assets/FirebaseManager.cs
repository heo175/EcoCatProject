using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{

    [SerializeField] string email;
    [SerializeField] string password;

    public InputField inputTextEmail;
    public InputField inputTextPassword;
    public Text loginResult;

    public string userName = null;
    public string userEmail = null;
    public string userId = null;

    FirebaseAuth auth;

    public FirebaseApp firebaseApp;
    DatabaseReference databaseReference;
    private string authCode;

    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;

        firebaseApp = FirebaseDatabase.DefaultInstance.App;
        firebaseApp.SetEditorDatabaseUrl("https://environmentkeepereco.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.SetEditorP12FileName("environmentkeepereco-620922cd5dcd.p12");

        // 아래 비밀번호에는 특별하게 설정한거 없으면 notasecret 일 겁니다.
        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
    }

    /*
    void update() {
        if (user != null)
        {
            string playerName = user.DisplayName;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
    }
    */
    // 버튼이 눌리면 실행할 함수.
    public void JoinBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        CreateUser();
    }


    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "회원가입 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "회원가입 실패";
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            loginResult.text = "회원가입 굿럭";
        });
    }

    public void LoginBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        LoginUser();
    }

    void LoginUser()
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "로그인 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "로그인 실패";
                return;
            }

             Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

        });

        FirebaseUser user;

        void Awake()
        {
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
                if (!signedIn && user != null)
                {
                    Debug.Log("Signed out " + user.UserId);
                }
                user = auth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log("Signed in " + user.UserId);
                    loginResult.text = user.UserId + " 로그인 굿럭";

                }
            }
        }

    }

    public void GoogleLoginBtnOnClick()
    {
        Debug.Log("구글버튼누름");
        GooglePlayServiceInitialize();

        Social.localUser.Authenticate(success =>
        {
            if (success == false) return;

            StartCoroutine(coFirebaseManager());

            if (success)
            {
                authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            }
        });

    }
   
    void GooglePlayServiceInitialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
  
    IEnumerator coFirebaseManager()
    {
     while (System.String.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
         yield return null;

    string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
    string accessToken = null;

        Credential credential = GoogleAuthProvider.GetCredential(idToken, accessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.Email, newUser.UserId);

            Firebase.Auth.FirebaseUser user = auth.CurrentUser;
            userName = newUser.DisplayName;
            userEmail = newUser.Email;
            userId = newUser.UserId;
            loginResult.text = "이름"+ userName + "메일"+ userEmail + "아이디"+ userId + "구글 로그인 굿럭";

        });
    
    }

    public void InitDatabase()
    {
        if (userId != null)
        {
           loginResult.text = "로그인상태";
           WriteNewUser(userId, userName, userEmail);
        }
        else if (userId == null)
        {
            loginResult.text = "로그아웃상태";
        }

    }

    public class User
    {
        public string uid;
        public string username;
        public string email;

        public User()
        {
        }

        public User(string uid, string username, string email)
        {
            this.uid = uid;
            this.username = username;
            this.email = email;
        }
    }

    private void WriteNewUser(string uid, string name, string email)
    {
        User user = new User(uid, name, email);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("users").Child(uid).SetRawJsonValueAsync(json);
        loginResult.text = "id^" + uid + "이름^" + name + "이메일^" + email;
    }


}
