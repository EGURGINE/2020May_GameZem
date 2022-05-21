using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPool : ObjectPool<Item>
{
    List<Tuple<ItemType, float>> itemList = new List<Tuple<ItemType, float>>();

    protected override void Awake()
    {
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 3));   //����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 5));   //����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 10));   //�丶��
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 10));   //���ξ���
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 5));   //����
    }

    public void SetItemRandom(Vector3 _pos)
    {
        int a = UnityEngine.Random.Range(0, 4);
        Item item = GetObj(_pos);
        item.SetItem(itemList[a].Item1, itemList[a].Item2);
    }
}
