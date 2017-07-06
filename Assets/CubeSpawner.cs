using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public ObjectPooler ObjectPooler;
    public float spawnTimer;
    public int size;

    private void Start()
    {
        ObjectPooler = new ObjectPooler(cubePrefab, size);
        spawnTimer = Time.time;
    }

    private void Update()
    {
        if(Time.time > spawnTimer + .1f)
        {
            spawnTimer = Time.time;

            GameObject temp = ObjectPooler.Instance.GetObjectFromPool();
            temp.SetActive(true);
            temp.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));

            StartCoroutine(DestroyCube(temp));
        }
    }

    private IEnumerator DestroyCube(GameObject _temp)
    {
        yield return new WaitForSeconds(Random.Range(1, 10));
        ObjectPooler.Instance.ReturnObjectToPool(_temp);
    }
}
