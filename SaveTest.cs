using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.Instance.UserData.userName = "クラウド";
            SaveSystem.Instance.Save();
            Debug.Log("セーブしました");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSystem.Instance.Load();
            Debug.Log(SaveSystem.Instance.UserData.userName);
          
        }
    }
}
