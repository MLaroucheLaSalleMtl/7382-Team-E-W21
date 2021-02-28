using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
    Inventory inventory;
    Transform itemSlotContainer, itemSlotTemplate;
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        int x = 0, y = 0;
        float itemSlotCellsize = 100f;
        foreach (Item item in inventory.GetItemList())
        {
            
            RectTransform itemSlotRectTransform= Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellsize,y*itemSlotCellsize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();


            x++;
            if(x>5)
            {
                x = 0;
                y++;
            }

        }
    }
}
