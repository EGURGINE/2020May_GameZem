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

        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 3));   //����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 5));   //����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Score, 300));   //�丶��
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, -10));   //�ٳ���
        itemList.Add(new Tuple<ItemType, float>(ItemType.HP, -5));   //����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, -1));   //�� ���� ����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Bullet, 3));   //�� ���� ����
        itemList.Add(new Tuple<ItemType, float>(ItemType.Score, -100));   //�� ���� �丶��
    }

    public void SetItemRandom(Vector3 _pos)
    {
        int a = UnityEngine.Random.Range(0, 8);
        Item item = GetObj(_pos);
        item.SetItem(itemList[a].Item1, itemList[a].Item2);
        item.GetComponent<MeshFilter>().mesh = meshes[a];
        item.GetComponent<MeshRenderer>().material = materials[a];
    }
}
