using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    Item item;
    SpriteRenderer spriteRenderer;
    public static ItemSpawn SpwanItem(Vector3 position,Item item)
    {
        Transform transform=Instantiate(ItemAssets.Instance.Location,position,Quaternion.identity);

        ItemSpawn itemSpawm = transform.GetComponent<ItemSpawn>();
        itemSpawm.SetItem(item);

        return itemSpawm;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    
}
