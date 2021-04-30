using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] public GameObject slashEffect;
    // Start is called before the first frame update
    public void EffectGenerate(Enemy obj)
    {
        StartCoroutine(Slash(obj));
    }

    public void EnemyEffectGenerate(Player obj)
    {
        StartCoroutine(EnemySlash(obj));
    }

    public void EffectGenerateToBoss(BossManager obj)
    {
        StartCoroutine(SlashToBoss(obj));
    }


    IEnumerator Slash(Enemy obj)
    {
        Instantiate(slashEffect, new Vector3(obj.transform.position.x,obj.transform.position.y,0),Quaternion.identity);
        
        yield return new WaitForSeconds(1f);
    
    }

    IEnumerator EnemySlash(Player obj)
    {
        Instantiate(slashEffect, new Vector3(obj.transform.position.x, obj.transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SlashToBoss(BossManager obj)
    {
        Instantiate(slashEffect, new Vector3(obj.transform.position.x, obj.transform.position.y, 0), Quaternion.identity);

        yield return new WaitForSeconds(1f);

    }

}
