using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {HP, Bullet, Score};

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public float amount;

    public void SetItem(ItemType _type, float _amount)
    {
        itemType = _type;
        amount = _amount;
    }

}
