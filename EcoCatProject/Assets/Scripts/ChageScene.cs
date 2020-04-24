using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;

public class ChageScene : MonoBehaviour
{
    public void PolarBearIntro1() {
        SceneManager.LoadScene("PolarStart");
    }

    public void app() {
        SceneManager.LoadScene("AppIntro1");
    }

    public void GotoVuforiaSceneAR()
    {
        SceneManager.LoadScene("VuforiaSceneAR");
    }
    public void GotoVuforiaSceneNormal()
    {
        SceneManager.LoadScene("VuforiaSceneNormal");
    }
    public void ChangeScene1()
    {
        SceneManager.LoadScene("SaveAnimal0");
    }
    public void gotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void gotoSearchScene()
    {
        SceneManager.LoadScene("SearchScene");
    }
    public void gotoSearchScene1()
    {
        SceneManager.LoadScene("SearchScene1");
    }
    public void gotoSearchScene2()
    {
        SceneManager.LoadScene("SearchScene2");
    }
    public void gotoWalkingScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void gotoGameList()
    {
        SceneManager.LoadScene("GameList");
    }

    public void StartScene() {
        SceneManager.LoadScene("LoginScene");
    }

    public void TutorialScene() {
        SceneManager.LoadScene("Tutorial1");
    }

    public void ClearToGameList() {
        SceneManager.LoadScene("GameList");
    }

    public void FailToGameList()
    {
        SceneManager.LoadScene("GameList");
    }

    // 분리수거
    public void GameListToGame1() {
        SceneManager.LoadScene("SeparateMenuScreen");
    }

    //  O,X 퀴즈
    public void GameListToGame2()
    {
        SceneManager.LoadScene("Persistent");
    }

    // 하천 쓰레기
    public void GameListToGame3()
    {
        SceneManager.LoadScene("PickingMenuScreen");
    }

    // 나무 키우기
    public void GameListToGame4()
    {
        SceneManager.LoadScene("GrowingMenuScreen");
    }

    // 장바구니 담기
    public void GameListToGame5()
    {
        SceneManager.LoadScene("CatchMenuScreen");
    }

    // 자전거 타기
    public void GameListToGame6()
    {
        SceneManager.LoadScene("RunMenuScreen");
    }


    public void CatchGameEasy()
    {
        SceneManager.LoadScene("EASYCatchGame");
    }
    public void CatchGameNormal()
    {
        SceneManager.LoadScene("NORMALCatchGame");
    }
    public void CatchGameHard()
    {
        SceneManager.LoadScene("HARDCatchGame");
    }

    public void RunGameEasy()
    {
        SceneManager.LoadScene("EASYRunGame");
    }
    public void RunGameNormal()
    {
        SceneManager.LoadScene("NORMALRunGame");
    }
    public void RunGameHard()
    {
        SceneManager.LoadScene("HARDRunGame");
    }

    public void PickingGameEasy()
    {
        SceneManager.LoadScene("EASYPickingRiverTrash");
    }
    public void PickingGameNormal()
    {
        SceneManager.LoadScene("NORMALPickingRiverTrash");
    }
    public void PickingGameHard()
    {
        SceneManager.LoadScene("HARDPickingRiverTrash");
    }

    public void GrowingTreeGameEasy()
    {
        SceneManager.LoadScene("EASYGrowingTree");
    }
    public void GrowingTreeGameNormal()
    {
        SceneManager.LoadScene("NORMALGrowingTree");
    }
    public void GrowingTreeGameHard()
    {
        SceneManager.LoadScene("HARDGrowingTree");
    }

    public void SeparateGameEasy()
    {
        SceneManager.LoadScene("EASYseparate");
    }
    public void SeparateGameNormal()
    {
        SceneManager.LoadScene("NORMALseparate");
    }
    public void SeparateGameHard()
    {
        SceneManager.LoadScene("HARDseparate");
    }

    // google 로그인 있는 로그인
    public void Loginscene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void VuforiaScene()
    {
        SceneManager.LoadScene("VuforiaScene");
    }

    // sqlite 로 만든 로그인
    public void LoginSqlite() {
        SceneManager.LoadScene("Login");
    }

    // sqlite 로 만든 회원가입
    public void JoinSqlite()
    {
        SceneManager.LoadScene("Join");
    }

    // 마이페이지 
    public void Mypage() {
        SceneManager.LoadScene("Mypage");
    }

    // 음향 
    public void Sound()
    {
        SceneManager.LoadScene("Sound");
    }

    // 걸음수 기록 페이지
    public void StepRecord()
    {
        SceneManager.LoadScene("StepRecord");
    }

    // 환경정보 페이지
    public void gotoInfoScene() {
        SceneManager.LoadScene("InformationScene");
    }

    // 자세한 환경 정보
    public void gotoMoreInfo()
    {
        SceneManager.LoadScene("moreInfoScene");
    }

    public void gotoMoreInfo_2()
    {
        SceneManager.LoadScene("moreInfoScene_2");
    }

    public void gotoMoreInfo_3()
    {
        SceneManager.LoadScene("moreInfoScene_3");
    }

    public void gotoMoreInfo_4()
    {
        SceneManager.LoadScene("moreInfoScene_4");
    }

    public void gotoMoreInfo_5()
    {
        SceneManager.LoadScene("moreInfoScene_5");
    }

    public void gotoMoreInfo2()
    {
        SceneManager.LoadScene("moreInfoScene2");
    }

    public void gotoMoreInfo2_2()
    {
        SceneManager.LoadScene("moreInfoScene2_2");
    }

    public void gotoMoreInfo2_3()
    {
        SceneManager.LoadScene("moreInfoScene2_3");
    }

    public void gotoMoreInfo3()
    {
        SceneManager.LoadScene("moreInfoScene3");
    }

    public void gotoMoreInfo3_2()
    {
        SceneManager.LoadScene("moreInfoScene3_2");
    }

    public void gotoMoreInfo3_3()
    {
        SceneManager.LoadScene("moreInfoScene3_3");
    }

    public void gotoMoreInfo3_4()
    {
        SceneManager.LoadScene("moreInfoScene3_4");
    }

    public void gotoMoreInfo3_5()
    {
        SceneManager.LoadScene("moreInfoScene3_5");
    }

    public void gotoMoreInfo4()
    {
        SceneManager.LoadScene("moreInfoScene4");
    }

    public void gotoMoreInfo4_2()
    {
        SceneManager.LoadScene("moreInfoScene4_2");
    }

    public void gotoMoreInfo4_3()
    {
        SceneManager.LoadScene("moreInfoScene4_3");
    }

    public void gotoMoreInfo4_4()
    {
        SceneManager.LoadScene("moreInfoScene4_4");
    }
    public void gotoMoreInfo5()
    {
        SceneManager.LoadScene("moreInfoScene5");
    }

    public void gotoMoreInfo5_2()
    {
        SceneManager.LoadScene("moreInfoScene5_2");
    }

    public void gotoMoreInfo5_3()
    {
        SceneManager.LoadScene("moreInfoScene5_3");
    }

    public void gotoMoreInfo5_4()
    {
        SceneManager.LoadScene("moreInfoScene5_4");
    }

    public void gotoMoreInfo5_5()
    {
        SceneManager.LoadScene("moreInfoScene5_5");
    }

    public void gotoMoreInfo5_6()
    {
        SceneManager.LoadScene("moreInfoScene5_6");
    }

    public void gotoMoreInfo6()
    {
        SceneManager.LoadScene("moreInfoScene6");
    }

    public void gotoMoreInfo6_2()
    {
        SceneManager.LoadScene("moreInfoScene6_2");
    }

    public void gotoMoreInfo6_3()
    {
        SceneManager.LoadScene("moreInfoScene6_3");
    }

    public void gotoMoreInfo6_4()
    {
        SceneManager.LoadScene("moreInfoScene6_4");
    }

    public void gotoMoreInfo6_5()
    {
        SceneManager.LoadScene("moreInfoScene6_5");
    }

    public void EtcScene()
    {
        SceneManager.LoadScene("EtcScene");
    }

    public void EtcScene2()
    {
        SceneManager.LoadScene("EtcScene2");
    }

    public void EtcScene3()
    {
        SceneManager.LoadScene("EtcScene3");
    }

    public void goPetScene()
    {
        SceneManager.LoadScene("PetScene");
    }

    public void goCanScene()
    {
        SceneManager.LoadScene("CanScene");
    }

    public void goEtcScene()
    {
        SceneManager.LoadScene("EtcScene");
    }

    public void goEtcScene2()
    {
        SceneManager.LoadScene("EtcScene2");
    }

    public void goEtcScene3()
    {
        SceneManager.LoadScene("EtcScene3");
    }

    public void goGlassScene()
    {
        SceneManager.LoadScene("GlassScene");
    }

    public void goPaperPackScene()
    {
        SceneManager.LoadScene("PaperPackScene");
    }

    public void goPaperScene()
    {
        SceneManager.LoadScene("PaperScene");
    }

    public void goPlasticScene()
    {
        SceneManager.LoadScene("PlasticScene");
    }

    public void goVinylScene()
    {
        SceneManager.LoadScene("VinylScene");
    }

    // 로그아웃
    public void Logout() {
        StartCoroutine(UpdateDBLogin("ecoDB.sqlite"));
        // SceneManager.LoadScene("StartScene 1");
        SceneManager.LoadScene("Login");
    }

    // 종료 버튼
    public void Quit() {
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // #else
        //  Destroy(gameObject);
          Application.Quit();
        // #endif    
    }

    // sqlite db에 login 업데이트
    IEnumerator UpdateDBLogin(string p)
    {

        string Filepath = Application.persistentDataPath + "/" + p;

        if (!File.Exists(Filepath))
        {
            Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
                Application.dataPath + "!/assets/" + p);

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            File.WriteAllBytes(Filepath, loadDB.bytes);
        }

        string connectionString = "URI=file:" + Filepath;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "UPDATE ecoCAT SET login = 0 WHERE login = 1 OR login = 2";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }
        yield return null;
    }
    


}
