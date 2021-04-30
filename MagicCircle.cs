using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class MagicCircle : MonoBehaviour
{
    [SerializeField] GameObject SelectFloorButton;
    GameObject selectFloorContent;
    GameObject selectFloorImage;
    [SerializeField] LayerMask selectFloorButtonLayer;
    Menu menuSc;
    private GameObject clickedGameObject;
    private bool isClick;
    [SerializeField] private GameObject GoCanvas;
    private void Start()
    {
        Debug.Log(GameObject.Find("Canvas"));
        menuSc = GoCanvas.GetComponent<Menu>();
        selectFloorImage = menuSc.selectFloorImage;
        selectFloorContent = menuSc.selectFloorContent;
    }
    void Update()
    {
        GetClickedGameObject();
    }

    public void StageSelect()
    {
        selectFloorImage.SetActive(true);
        int generateNum = Mathf.RoundToInt(SaveSystem.Instance.UserData.stage / 10);
     
        for (int i = 1; i <= generateNum; i++)
        {
          
            GameObject clone = Instantiate(SelectFloorButton);
            clone.transform.SetParent(selectFloorContent.transform, false);
            Text cloneText = clone.GetComponentInChildren<Text>();
            cloneText.text = i * 10 + "階";
        }
    }

    public void GetClickedGameObject()
    {
        if (Input.GetMouseButtonDown(0) && !isClick)
        {
            isClick = true;
            clickedGameObject = null;

            Vector2 mousePos = Input.mousePosition;
            RaycastHit2D hit2d = Physics2D.Raycast(mousePos,Vector2.zero,0,selectFloorButtonLayer);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                SceneChange();
            }
            isClick = false;      
        }
      
    }

    public void SceneChange()
    {
        if (clickedGameObject.GetComponentInChildren<Text>() != null)
        {
            Text text = clickedGameObject.GetComponentInChildren<Text>();
            for (int i = 0; i < SaveSystem.Instance.UserData.stage; i++)
            {
                if (text.text == i + "階")
                {
                    SaveSystem.Instance.UserData.currentStage = i;
                    SaveSystem.Instance.Save();
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StageSelect();
        }
    }
}
