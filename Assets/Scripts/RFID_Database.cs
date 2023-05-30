using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RFID_Database", menuName = "RFID/Database")]
public class RFID_Database : ScriptableObject
{
    public List<string> UID;
}