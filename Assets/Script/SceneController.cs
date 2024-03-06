using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform chestMonsterSpwnPt;

    public void InstantiateMonster()
    {
        enemyPrefab = Instantiate(enemyPrefab) as GameObject;
        enemyPrefab.transform.position = chestMonsterSpwnPt.position;
    }
}
