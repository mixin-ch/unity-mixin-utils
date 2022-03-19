using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProduct : MonoBehaviour
{
    public ShopProduct shopProduct;
    public string Features;
    

    /*public int Price { get => _price; }
    public Sprite Sprite { get => _sprite; }
    public string Description { get => _description; }
    public string Name { get => _name; }
    public Currency Currency { get => _currency; }*/

}

[Serializable]
public class BaseObject : IObject
{
    [Header("Base Information")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    public string Name { get => _name; }
    public string Description { get => _description; }
    public Sprite Sprite { get => _sprite; }
}

[Serializable]
public class ShopProduct : IShopProduct
{
    [SerializeField] private BaseObject _baseObject;

    [Header("Shop Product")]
    [SerializeField] private int _price;
    [SerializeField] private Currency _currency;

    public string Name { get => _baseObject.Name; }
    public string Description { get => _baseObject.Description; }
    public Sprite Sprite { get => _baseObject.Sprite; }
    public int Price { get => _price; }
    public Currency Currency { get => _currency; }

}
