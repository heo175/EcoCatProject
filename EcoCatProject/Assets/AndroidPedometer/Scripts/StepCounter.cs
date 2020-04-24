using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class StepCounter : MonoBehaviour {

    public Text stepCountText;
    public Text state;
    public Text energyNum;
    public GameObject okPanel;
    public GameObject firstPanel;
    public Text testTxt;
    public Text testTxt2;

    string id;

    int energy;

    int step;
    int dbStep;
    int totalStep = 0;
    int dbTotalStep;

    string date;
    string date_1; //하루전날
    string date_2; //이틀전날
    string date_3; //삼일전날
    string date_4;
    string date_5;
    string date_6; 

    int n; // n 이 0이면 걸음수 3000 넘었을 때 에너지 받을 수 있음. 받고나면 n = 1 로 바뀜
    int s = 0; // db 저장에 한 번만 하게 하기 위해
    int ok = 0; // okPanel 보이게 안보이게
    int first; // 첫 접속시 전날꺼 저장하기 위해

    void Start()
    {
        StartService();
        SelectId();
        SelectEnergy();
        SelectStep();
        
        SelectDate(); // stepDB에 date 있는지
        if (date == null) // 오늘 날짜 없으면 넣어요
        {
            first = 1;
            SaveStep2();
        }
        else {
            first = 0;
        }

        if (date_1 == null)
        {
            SaveStep2_date_1();
        }

        if (date_2 == null) {
            SaveStep2_date_2();
        }

        if (date_3 == null)
        {
            SaveStep2_date_3();
        }

        if (date_4 == null)
        {
            SaveStep2_date_4();
        }

        if (date_5 == null)
        {
            SaveStep2_date_5();
        }

        if (date_6 == null)
        {
            SaveStep2_date_6();
        }


    }

	// Update is called once per frame
	void Update ()
    {
        var currentStepCount = StepCounterPlugin.GetStepCount();

        // 오늘 첫 접속시 지금까지 걸음수를 어제꺼로 저장하고, 오늘은 다시 0으로 보이게 만들기 위해
        if (first == 1)
        {
            // 지금까지의 step을 전날에다가 저장하는 코드
            SaveStep3_date_1();

            // totalStep 을 db에 저장해서 하루에 한 번만 total을 저장하게 해야함 db 코드 만들자
            totalStep = currentStepCount;
            UpdateTotal();

            s = 1;

            if (s == 1)
            {
                UpdateWalkingInit(); // 다시 걸음수 3000 넘으면 에너지 받을 수 있게       
                s = 0; // 밤되면 또 업데이트하게
            }

           // first = 0;
        }


         if (dbStep > currentStepCount) // 핸드폰 껐다 켰을 때
         {
                step = dbStep + currentStepCount;
                totalStep = 0;
         }
         else { // 평상시
        step = currentStepCount - dbTotalStep;
        }

        stepCountText.text = "걸음수 : " + step.ToString();



        if (ok == 0)
        {
            okPanel.SetActive(false);
        }
        else if (ok == 1 && n==0) {
            okPanel.SetActive(true);
        }

        if (n==0 && step>=3000) { // 3000 걸음 넘으면 에너지 +200
            ok = 1;
        }

        testTxt.text = "current : " + currentStepCount.ToString();

        /*
        if (DateTime.Now.ToString("HH:mm:ss") == "23:59:00") // 밤 11시 59분이 되면 각 디비에 걸음수 저장
        {
            if (s == 0)
            {
                SaveStep3(); // 오늘 날짜에 오늘의 걸음수를 step 에 업데이트

                totalStep = currentStepCount; // 기기 걸음수를 total에 저장

                s = 1; // 12시되면 밑 코드 실행할 수 있게                
            }
        }
        if (DateTime.Now.ToString("HH:mm:ss") == "00:00:05") // 밤 12시에 초기화
        {
            if (s == 1)
            {
                SaveStep2(); // 오늘 날짜에 step 을 0으로 삽입
                UpdateWalkingInit(); // 다시 걸음수 3000 넘으면 에너지 받을 수 있게       
                s = 0; // 밤되면 또 업데이트하게
            }

        }

        
        */

    }
    

    public void ClickTestBtn() {
        testTxt2.text = "step : " + step.ToString() + " , total : " + totalStep.ToString()
            + " , dbStep : " + dbStep.ToString() + ", dbTotalStep : "+ dbTotalStep.ToString();
    }


    public void ClickokBtn() {
        SetUpdateEnergy();
        UpdateWalkingGet();
        energy = energy + 2000;
        energyNum.text = energy.ToString();
        n = 1;
        ok = 0;
    }

    public void StartService()
    {
        StepCounterPlugin.StartStepCounterService();
        state.text = "걸음수를 세고 있어요";
    }

    public void StopService()
    {
        StepCounterPlugin.StopStepCounterService();
        state.text = "걸음수를 세기를 멈췄어요";
    }

    // sqlite db에서 login=1 인 id, n 받아오기 - 함수
    public void SelectId()
    {
        StartCoroutine(SelectDBId("ecoDB.sqlite"));
    }

    // sqlite db에서 login=1 인 id, n 받아오기 
    IEnumerator SelectDBId(string p)
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
                string sqlQuery = "SELECT id, walking, total FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        id = reader.GetString(0);
                        n = Int32.Parse(reader.GetString(1));
                        dbTotalStep = Int32.Parse(reader.GetString(2));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log(id + ", n : "+n);

        yield return null;
    }

    // sqlite db에서 energy 받아오기 - 함수
    public void SelectEnergy()
    {
        StartCoroutine(SelectDBEnergy("ecoDB.sqlite"));
    }

    // sqlite db에서 energy 받아오기
    IEnumerator SelectDBEnergy(string p)
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
                string sqlQuery = "SELECT energy FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        energy = Int32.Parse(reader.GetString(0));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        yield return null;
    }

    public void SetUpdateEnergy()
    {
        StartCoroutine(StepUpdateEnergy("ecoDB.sqlite"));
    }

    // 걸음수 3000 이상 되면 에너지 +200
    IEnumerator StepUpdateEnergy(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET energy =" +
                        (energy + 2000) +
                        " WHERE login = 1 OR login = 2";

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

    public void SaveStep()
    {
        StartCoroutine(SaveStepDB("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB(string p)
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

        using (IDbConnection dbConnection2 = new SqliteConnection(connectionString))
        {
            dbConnection2.Open();

            using (IDbCommand dbCmd2 = dbConnection2.CreateCommand())
            {
                string sqlQuery = "UPDATE ecoCAT SET walking =" +
                        totalStep.ToString() +
                        " WHERE login = 1 OR login = 2";
                dbCmd2.CommandText = sqlQuery;

                using (IDataReader reader2 = dbCmd2.ExecuteReader())
                {

                    while (reader2.Read())
                    {

                    }
                    dbConnection2.Close();
                    reader2.Close();
                }
            }
        }
        Debug.Log("ecoCAT 완료");
        yield return null;
    }

    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2() 
    {
        StartCoroutine(SaveStepDB2("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2(string p)
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
               
               string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('"+id+"', '"+
                    DateTime.Now.ToString("yyyy-MM-dd")+"', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    // stepDB 에 걸음수 업데이트
    public void SaveStep3()
    {
        StartCoroutine(SaveStepDB3("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB3(string p)
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
                string sqlQuery = "UPDATE stepDB SET step = '" +
                        step.ToString() +
                        "' WHERE id = '"+id+"' and date = '"+ DateTime.Now.ToString("yyyy-MM-dd")+"'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB update 완료");
        yield return null;
    }

    // stepDB 에 걸음수 업데이트
    public void SaveStep3_date_1()
    {
        StartCoroutine(SaveStepDB3_date_1("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB3_date_1(string p)
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
                string sqlQuery = "UPDATE stepDB SET step = '" +
                        step.ToString() +
                        "' WHERE id = '" + id + "' and date = '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB update 완료");
        yield return null;
    }

    // StepDb에서 step 받아오기 - 함수
    public void SelectStep()
    {
        StartCoroutine(SelectDBStep("ecoDB.sqlite"));
    }

    // StepDb에서 step 받아오기
    IEnumerator SelectDBStep(string p)
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
                string sqlQuery = "SELECT step FROM stepDB WHERE id = '"+id+"' AND date = '"+ DateTime.Now.ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        dbStep = Int32.Parse(reader.GetString(0));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        yield return null;
    }

    // ecoCat 에 걸음수 3000 넘어서 에너지 받았다는 표시 업데이트
    public void UpdateWalkingGet()
    {
        StartCoroutine(UpdateWalkingGetDB("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator UpdateWalkingGetDB(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET walking = '1' WHERE id = '" + id + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("ecoCAT 걸음수 3000 넘어서 에너지 받았다고 표시 update 완료");
        yield return null;
    }

    // ecoCat 에 (walking)n = 0 만들기 - 함수
    public void UpdateWalkingInit()
    {
        StartCoroutine(UpdateWalkingInitDB("ecoDB.sqlite"));
    }

    // ecoCat 에 (walking)n = 0 만들기
    IEnumerator UpdateWalkingInitDB(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET walking = '0' WHERE id = '" + id + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("ecoCAT 걸음수 walking(n) = 0 update 완료");
        yield return null;
    }

    // StepDb에서 date 받아오기 - 함수
    public void SelectDate()
    {
        StartCoroutine(SelectDBDate("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_1("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_2("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_3("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_4("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_5("ecoDB.sqlite"));
        StartCoroutine(SelectDBDate_6("ecoDB.sqlite"));
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '" 
                   + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : "+date);

        yield return null;
    }

    // StepDb에서 date_1 받아오기
    IEnumerator SelectDBDate_1(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_1 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_1);

        yield return null;
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate_2(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_2 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_2);

        yield return null;
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate_3(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_3 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_3);

        yield return null;
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate_4(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_4 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_4);

        yield return null;
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate_5(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_5 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_5);

        yield return null;
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate_6(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date_6 = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date_6);

        yield return null;
    }

    // stepDB 에 전날 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_1()
    {
        StartCoroutine(SaveStepDB2_date_1("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_1(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }



    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_2()
    {
        StartCoroutine(SaveStepDB2_date_2("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_2(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_3()
    {
        StartCoroutine(SaveStepDB2_date_3("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_3(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_4()
    {
        StartCoroutine(SaveStepDB2_date_4("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_4(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_5()
    {
        StartCoroutine(SaveStepDB2_date_5("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_5(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    // stepDB 에 걸음수 삽입 (걸음수 0으로)
    public void SaveStep2_date_6()
    {
        StartCoroutine(SaveStepDB2_date_6("ecoDB.sqlite"));
    }

    // 걸음수 저장
    IEnumerator SaveStepDB2_date_6(string p)
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

                string sqlQuery = "INSERT INTO stepDB (id, date, step) VALUES('" + id + "', '" +
                     DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + "', '0')";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        Debug.Log("stepDB insert 완료");
        yield return null;
    }

    public void UpdateTotal()
    {
        StartCoroutine(UpdateDBTotal("ecoDB.sqlite"));
    }

    // 걸음수 3000 이상 되면 에너지 +200
    IEnumerator UpdateDBTotal(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET total = '" +
                       totalStep +
                        "' WHERE login = 1 OR login = 2";
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
