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
        Ammount = 100;
    }
    public Type GetPickUp() { return Type; }
    public int GetPickAmmount() { return Ammount; }
}
