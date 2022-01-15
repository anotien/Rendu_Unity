using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [Tooltip("Prefab des IA")]
    public Transform prefabAI;
    [Tooltip("Point de spawn des IA")]
    public Transform spawnPoint;
    void Start()
    {
        Transform ai = SpawnAI();
        AddPichenette(ai,ai.forward*5);
    }

    Transform SpawnAI()
    {
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        ai.position = spawnPoint.position;
        ai.rotation = spawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai, Vector3 pichenette)
    {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
        rb.AddForce(pichenette,ForceMode.Impulse);

    }

    private float time = 0;
    private float timeMax = 6;

    private Vector3 lastPichenette;
    void Update()
    {
        time = time + Time.deltaTime;
        
        if(time >= timeMax)
        {
            Transform ai = SpawnAI();
            Vector3 pichenette = ai.forward*5;
            pichenette.x += Random.Range(-2.0f, 2.0f);
            pichenette.y += Random.Range(0.0f, 2.0f);
            AddPichenette(ai,ai.forward*5);

            lastPichenette = pichenette;
            time = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint.position,spawnPoint.position + lastPichenette);
    }
}
