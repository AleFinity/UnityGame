using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Heroe_pr;
    public GameObject Canvas_pr;

    public static bool GameIsPaused = true;

    private string datapath;
    private string last_save;

    public GameObject PauseMenuUI;

    public GameObject save_state;

    private bool IsGameStarted=false;

    void Start()
    {
        Paused();       

        PauseMenuUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        PauseMenuUI.gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if (CheckSaves()) { 
            PauseMenuUI.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //Debug.Log(CheckSaves());
        }
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (CheckSaves()) { 
                PauseMenuUI.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                //Debug.Log(CheckSaves());
            }
            if (IsGameStarted)
                PauseMenuUI.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (!GameIsPaused)
            {
                Paused();
            }
        }
    }

    public void Resumed()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Paused() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ContinueGame()
    {
        //Debug.Log("ContinueGame");
        Resumed();
    }

    public void DownloadLastGame()
    {
        IsGameStarted = true;
        string scense = last_save.Substring(9);
        scense = scense.Replace(".xml", "");
        //Debug.Log("ContinueGame");
        List<string> items = Manager.Inventory.GetItemList();
        foreach (string item in items)
            Manager.Inventory.DelItem(item);

        GameObject heroe = GameObject.Find("Heroe");
        heroe.gameObject.GetComponent<Inventory_save>().Generate();

        //Debug.Log(scense);
        Resumed();
        SceneManager.LoadScene(int.Parse(scense), LoadSceneMode.Single);
    }
    
    public void StartGame()
    {
        IsGameStarted = true;
        PlayerPrefs.DeleteAll();
        DirectoryInfo dirInfo = new DirectoryInfo(datapath);
        foreach (FileInfo file in dirInfo.GetFiles())
            file.Delete();        
        
        //Debug.Log("StartGame");
        PauseMenuUI.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Resumed();
        Destroy(GameObject.Find("Heroe"));
        SceneManager.LoadScene("Underground", LoadSceneMode.Single);
    }

    public void ExitGame() {
        GameObject.Find("Save_state").SendMessageUpwards("Dump");
        GameObject.Find("Heroe").SendMessageUpwards("DumpInv");
        //Debug.Log("ExitGame");
        Application.Quit();
    }

    public bool CheckSaves() {
        DateTime dt = new DateTime(1990, 1, 1);
        datapath = Application.dataPath + "/Saves";
        FileSystemInfo[] fileSystemInfo = new DirectoryInfo(datapath).GetFileSystemInfos();        
        if (fileSystemInfo.Length != 0)
        {
            foreach (FileSystemInfo fileSI in fileSystemInfo)
            {
                if (fileSI.Name.IndexOf("SavedData")==0 && fileSI.Name.EndsWith(".xml") && dt < Convert.ToDateTime(fileSI.LastWriteTime))
                {
                    dt = Convert.ToDateTime(fileSI.LastWriteTime);
                    last_save = fileSI.Name;
                }
            }
        }
        else
            return false;
        return true;
    }
}
