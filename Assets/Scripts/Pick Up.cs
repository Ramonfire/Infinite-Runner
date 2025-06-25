using System;
using UnityEngine;

public enum Type
{
    coin,
    health
}


public class PickUp : MonoBehaviour
{
    [SerializeField]  Type Type;
    [SerializeField] int Ammount; 
    private void Awake()
    {
        Ammount = UnityEngine.Random.Range(1,10);
    }
    public Type GetPickUp() { return Type; }
    public int GetPickAmmount() { return Ammount; }
}
