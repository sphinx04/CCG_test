using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomizeHand : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Button button;

    private List<Action<Card>> StatRandomizers;

    private void Start()
    {
        StatRandomizers = new List<Action<Card>>
        {
            card => card.ManaPoints += Random.Range(-2, 10),
            card => card.AttackPoints += Random.Range(-2, 10),
            card => card.HealthPoints += Random.Range(-2, 10)
        };
        button.onClick.AddListener(() => StartCoroutine(Randomize()));
    }

    private IEnumerator Randomize()
    {
        for (int i = 0; i < Hand.Get().Count; i++)
        {
            StatRandomizers[Random.Range(0, StatRandomizers.Count)](Hand.Get()[i]);
            yield return new WaitForSeconds(delay);
        }
    }
}
