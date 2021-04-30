using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPC : MonoBehaviour
{
    [SerializeField] string message = "";
    public bool isTalking = false;
    private Player playerSc;
 
    Flowchart flowChart;
    void Start()
    {  
        flowChart = GetComponent<Flowchart>();
        playerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Talk());
        }
    }

    IEnumerator Talk()
    {
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
       
    }

}
