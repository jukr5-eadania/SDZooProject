using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveSO : ScriptableObject
{
    
    public Dictionary<string, bool> saveData = new Dictionary<string, bool>();
}
