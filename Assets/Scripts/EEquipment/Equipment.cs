using UnityEngine;

public enum EquipmentType { Weapon, Helmet, Armor, Boots, Gloves, Ring }
public enum Rarity { Common, Uncommon, Rare, Epic, Legendary, Mythic }

[System.Serializable]
public class Equipment
{
    public string itemName;
    public EquipmentType type;
    public Rarity rarity;
    public int requiredLevel;

    public float atk;
    public float def;
    public float hp;
    public float mp;
    public float crit;
    public float critDame;

    public Sprite icon; // ✅ Thêm dòng này

    public Equipment(string name, EquipmentType type, Rarity rarity, int requiredLevel,
        float atk = 0, float def = 0, float hp = 0, float mp = 0,
        float crit = 0, float critDame = 0)
    {
        this.itemName = name;
        this.type = type;
        this.rarity = rarity;
        this.requiredLevel = requiredLevel;

        this.atk = atk;
        this.def = def;
        this.hp = hp;
        this.mp = mp;
        this.crit = crit;
        this.critDame = critDame;
        this.icon = null; // Mặc định rỗng
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(itemName) &&
               requiredLevel > 0 &&
               (atk != 0 || def != 0 || hp != 0 || mp != 0 || crit != 0 || critDame != 0);
    }
}
