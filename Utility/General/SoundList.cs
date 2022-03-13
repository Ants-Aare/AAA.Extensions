using UnityEngine;
[System.Serializable]
public class SoundList
{
    #region Values
    // [SerializeField]
    // private string sound = "";
    [SerializeField]
    private AudioClip[] audioClips = null;
    private int lastClip = -1;
    #endregion

    public AudioClip GetRandom()
    {
        if(audioClips.Length > 1)
        {
            int returnIndex = Random.Range(0, audioClips.Length);

            while (returnIndex == lastClip)
            {
                returnIndex = Random.Range(0, audioClips.Length);
            }

            lastClip = returnIndex;
            return audioClips[returnIndex];
        }
        return audioClips[0];
    }
    public AudioClip GetNext()
    {
        if(audioClips.Length > 1)
        {
            lastClip ++;
            if(lastClip >= audioClips.Length)
                lastClip = 0;
            return audioClips[lastClip];
        }
        return audioClips[0];
    }
}