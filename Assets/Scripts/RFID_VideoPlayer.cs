using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public partial class RFID_VideoPlayer : MonoBehaviour
{
    [HideInInspector]
    public SerialController serialController;

    [HideInInspector]
    public VideoPlayer videoPlayer;

    public RFID_Database database;
    public List<RFID_UID2Video> table;

    private string _lastUID = string.Empty;

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach(string uid in database.UID)
        {
            if (!table.Exists(x => x.UID == uid))
            {
                table.Add(new RFID_UID2Video { UID = uid });
            }
        }
    }
#endif

    void Start()
    {
        serialController = GetComponent<SerialController>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        var uid = message.Trim();
        Debug.Log(uid);

        if (uid == SerialController.SERIAL_DEVICE_CONNECTED || uid == SerialController.SERIAL_DEVICE_DISCONNECTED)
            return;

        if (uid == _lastUID)
            return;
        _lastUID = uid;

        if (database.UID.Exists(x => x == uid))
        {
            var row = table.SingleOrDefault(x => x.UID == uid);
            if (row != null)
            {
                videoPlayer.Stop();
                videoPlayer.clip = row.Video;
                videoPlayer.Play();
            }
        }
        else
        {
            database.UID.Add(uid);
        }
    }
}
