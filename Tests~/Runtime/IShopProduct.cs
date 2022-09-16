using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopProduct : IObject
{
    public int Price { get; }
    public Currency Currency { get;}
}

public enum Currency
{
    Secrets,
    Nobility,
}