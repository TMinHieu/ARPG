using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<Equipment> equipmentList = new List<Equipment>();

    /// <summary>
    /// Tạo và thêm trang bị mới vào danh sách.
    /// Chỉ thêm nếu ít nhất 1 chỉ số được nhập (khác 0).
    /// </summary>
    public bool CreateEquipment(string name, EquipmentType type, Rarity rarity, int requiredLevel,
        float atk = 0, float def = 0, float hp = 0, float mp = 0,
        float crit = 0, float critDame = 0)
    {
        Equipment newItem = new Equipment(name, type, rarity, requiredLevel, atk, def, hp, mp, crit, critDame);
        if (!newItem.IsValid())
        {
            Debug.LogWarning("Thiếu chỉ số! Trang bị phải có ít nhất 1 chỉ số khác 0.");
            return false;
        }

        equipmentList.Add(newItem);
        Debug.Log($"Đã tạo: {name} [{type}] - Cấp yêu cầu: {requiredLevel}, Phẩm chất: {rarity}");
        return true;
    }

    /// <summary>
    /// In toàn bộ danh sách trang bị ra log.
    /// </summary>
    public void PrintAllEquipment()
    {
        foreach (var item in equipmentList)
        {
            Debug.Log(
                $"{item.itemName} ({item.type}, {item.rarity}) [Lv{item.requiredLevel}] - " +
                $"ATK: {item.atk}, DEF: {item.def}, HP: {item.hp}, MP: {item.mp}, Crit: {item.crit}, CritDame: {item.critDame}"
            );
        }
    }
}
