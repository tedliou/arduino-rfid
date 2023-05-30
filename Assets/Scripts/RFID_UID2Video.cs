using Sirenix.OdinInspector;
using UnityEngine.Video;

public partial class RFID_VideoPlayer
{
    [System.Serializable]
    public class RFID_UID2Video
    {
        [ReadOnly]
        public string UID;

        [AssetsOnly]
        public VideoClip Video;
    }
}
