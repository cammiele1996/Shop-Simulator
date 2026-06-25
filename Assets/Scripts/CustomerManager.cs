using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Customer> customersToSpawn = new List<Customer>();

    public float timeBetweenCustomers;
    private float spawnCounter;

    public List<NavPoint> entryPointsLeft, entryPointsRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            SpawnCustomer();
        }
        
    }

    public void SpawnCustomer()
    {
        Instantiate(customersToSpawn[Random.Range(0,16)]);

        spawnCounter = timeBetweenCustomers * Random.Range(.75f, 1.25f);
    }

    public List<NavPoint> GetEntryPoints()
    {
        List<NavPoint> points = new List<NavPoint>();

        if (Random.value < .5f)
        {
            points.AddRange(entryPointsLeft);
        }
        else
        {
            points.AddRange(entryPointsRight);
        }


        return points;

    }
    
    public List<NavPoint> GetExitPoints()
    {
        List<NavPoint> points = new List<NavPoint>();

        List<NavPoint> temp = new List<NavPoint>();

        if (Random.value < .5f)
        {
            temp.AddRange(entryPointsLeft);
        }
        else
        {
            temp.AddRange(entryPointsRight);
        }

        for(int i = temp.Count -1; i >= 0; i--)
        {
            points.Add(temp[i]);
        }

        return points;

    }
}
