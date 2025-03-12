using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefaps;
    void Start()
    {
        StartCoroutine(instantiateCorutine());
    }

    void Update()
    {
        
    }
    IEnumerator instantiateCorutine()
    {
        while(true)
        {
            ZombieSpawn();
            yield return new WaitForSeconds(1f);
        }
    }

    void ZombieSpawn()
    {
        GameObject game = Instantiate(zombiePrefaps, gameObject.transform.position, Quaternion.identity);
        int a = Random.Range(0, 3);
        ZombieMove move = game.GetComponent<ZombieMove>();

        move.moveSpeed = Random.Range(1.0f, 1.3f);

        switch(a)
        {
            case 0:
                move.minY = -2.8f;
                game.layer = LayerMask.NameToLayer("Floor1");
                game.GetComponent<SortingGroup>().sortingOrder = 1;
                move.normalLayer = "Floor1";
                break;
            case 1:
                move.minY = -3.2f;
                game.layer = LayerMask.NameToLayer("Floor2");
                game.GetComponent<SortingGroup>().sortingOrder = 2;
                move.normalLayer = "Floor2";
                break;
            case 2:
                move.minY = -3.6f;
                game.layer = LayerMask.NameToLayer("Floor3");
                game.GetComponent<SortingGroup>().sortingOrder = 3;
                move.normalLayer = "Floor3";
                break;
        }
    }
}
