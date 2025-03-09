using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    public List<int> modifiers = new List<int>();  // Khởi tạo danh sách tránh lỗi NullReference

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        if (modifiers.Contains(_modifier))
        {
            modifiers.Remove(_modifier); 
        }
        else
        {
            Debug.LogWarning("Modifier không tồn tại, không thể xóa.");
        }
    }
}
