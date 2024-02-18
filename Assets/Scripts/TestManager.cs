using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    [field: SerializeField] public uint AmountOfCheeseToSpawn { get; set; }
    [field: SerializeField] public Spawner CheeseSpawner { get; private set; }
    [field: SerializeField] public Spawner RatSpawner { get; private set; }
    [field: SerializeField] public Cheese cheesePrefab;
    public GameObject CheesePrefab => cheesePrefab.gameObject;
    [field: SerializeField] public Rat ratPrefab;
    public GameObject RatPrefab => ratPrefab.gameObject;

    public float initialDelay = 5f; // Initial delay in seconds before spawning starts
    public float delayDecreaseIncrementPercentage = 0.90f;
    public float minimumDelay = 0.5f;

    private readonly List<GameObject> cheeseGOs = new();
    private List<Cheese> cheeses => cheeseGOs.Select(go => go.GetComponent<Cheese>()).ToList();
    private readonly List<GameObject> ratGOs = new();
    private List<Rat> rats => ratGOs.Select(go => go.GetComponent<Rat>()).ToList();
    
    private void Start()
    {
        // Start of game
        cheeseGOs.AddRange(CheeseSpawner.Spawn(CheesePrefab, AmountOfCheeseToSpawn));
        StartCoroutine(SpawnRatsOverTime());
    }

    private void Update()
    {
        foreach (var rat in rats)
        {
            if (rat.Movement.IsWaitingForNewDestination)
            {
                Debug.Log($"{rat.name} reached destination!");
                if (rat.Controller.HasItem)
                    GoToExit(rat);
                else
                    GoToCheese(rat);
            }
        }
    }

    private int spawnedRatCount = 0;
    private IEnumerator SpawnRatsOverTime()
    {
        float delay = initialDelay;
        while (true) // TODO: use a boolean from something (if we lose or win stop spawning rats?)
        {
            var newRatGO = RatSpawner.Spawn(RatPrefab);
            newRatGO.name = $"Spawned Rat {spawnedRatCount++}";
            ratGOs.Add(newRatGO);
            var newRat = newRatGO.GetComponent<Rat>();
            GoToCheese(newRat);
            yield return new WaitForSeconds(delay);
            delay = Mathf.Max(minimumDelay, delay * delayDecreaseIncrementPercentage);
        }
    }

    private void GoToExit(Rat rat)
    {
        var exitPos = RatSpawner.GetRandomPosition();
        rat.Movement.SetTarget(exitPos);
        Debug.Log($"{rat.name} go to exit!");
    }
    private void GoToCheese(Rat rat)
    {
        var validCheeses = cheeses.Where(c => !c.IsPickedUp).ToList();
        Vector3 targetPos;
        if (validCheeses.Count > 0)
            targetPos = validCheeses[Random.Range(0, validCheeses.Count)].transform.position;
        else
            targetPos = RatSpawner.GetRandomPosition();
        rat.Movement.SetTarget(targetPos);
        Debug.Log($"{rat.name} go to cheese!");
    }
}
