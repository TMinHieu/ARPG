using UnityEngine;
using System.Collections.Generic;

public class LootOrb : MonoBehaviour
{
    public List<Equipment> items = new List<Equipment>();
    public SpriteRenderer glowRenderer; // ánh sáng phát ra
    public Color commonColor = Color.white;
    public Color uncommonColor = Color.green;
    public Color rareColor = Color.blue;
    public Color epicColor = new Color(1f, 0.5f, 0f); // cam
    public Color legendaryColor = Color.red;
    public Color mythicColor = new Color(0.6f, 0f, 0.8f); // tím

    public void Setup(List<Equipment> droppedItems)
    {
        items = droppedItems;
        glowRenderer.color = GetGlowColor(GetHighestRarity());
    }

    Rarity GetHighestRarity()
    {
        Rarity highest = Rarity.Common;
        foreach (var item in items)
        {
            if ((int)item.rarity > (int)highest)
                highest = item.rarity;
        }
        return highest;
    }

    Color GetGlowColor(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Uncommon => uncommonColor,
            Rarity.Rare => rareColor,
            Rarity.Epic => epicColor,
            Rarity.Legendary => legendaryColor,
            Rarity.Mythic => mythicColor,
            _ => commonColor,
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var item in items)
            {
                Debug.Log($"Player nhận: {item.itemName}");
                // TODO: Add to inventory
            }

            Destroy(gameObject);
        }
    }
}
