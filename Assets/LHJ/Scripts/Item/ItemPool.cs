using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPool : ObjectPool<Item>
{
    List<Tuple<ItemType, float>> itemList = new List<Tuple<ItemType, float>>();

    protected override void Awake()
    {
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 3));   //딸기
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 5));   //참외
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 10));   //토마토
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 10));   //파인애플
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 5));   //수박
    }

    public void SetItemRandom(Vector3 _pos)
    {
        int a = UnityEngine.Random.Range(0, 4);
        Item item = GetObj(_pos);
        item.SetItem(itemList[a].Item1, itemList[a].Item2);
    }
}
