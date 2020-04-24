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
using UnityEngine.SceneManagement;

public class GooglePlayLoginStart : MonoBehaviour
{
    public string userName = null;
    public string userEmail = null;
    public string userId = null;
    FirebaseAuth auth;
    public FirebaseApp firebaseApp;
    DatabaseReference databaseReference;
    private string authCode;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Googlelogintry() {
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
            //          loginResult.text = "이름" + userName + "메일" + userEmail + "아이디" + userId + "구글 로그인 굿럭";
            
        });
        if (userId != null)
        {
            SceneManager.LoadScene("GoogleSuccessSignup");
        }
    }
    public void ChangeLoginScene()
    {
        SceneManager.LoadScene("GoogleSuccessSignup");
    }
}
