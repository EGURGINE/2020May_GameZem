using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPool : ObjectPool<Item>
{
    List<Tuple<ItemType, float>> itemList = new List<Tuple<ItemType, float>>();
    [SerializeField] List<Mesh> meshes = new List<Mesh>();
    [SerializeField] List<Material> materials = new List<Material>();
    protected override void Awake()
    {
        base.Awake();

        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 3));   //딸기
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 5));   //참외
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 10));   //토마토
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 10));   //바나나
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, 5));   //수박
    }

    public void SetItemRandom(Vector3 _pos)
    {
        int a = UnityEngine.Random.Range(0, 4);
        Item item = GetObj(_pos);
        item.SetItem(itemList[a].Item1, itemList[a].Item2);
        item.GetComponent<MeshFilter>().mesh = meshes[a];
        item.GetComponent<MeshRenderer>().material = materials[a];
    }
}
