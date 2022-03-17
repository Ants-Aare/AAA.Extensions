using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAA.Utility.Audio
{
    [System.Serializable]
    public class SoundList
    {
        [SerializeField][ListDrawerSettings()] private List<AudioClip> sounds;
        private int lastClip = -1;

        public AudioClip GetRandom()
        {
            if(sounds.Count > 1)
            {
                int returnIndex = Random.Range(0, sounds.Count);

                if(returnIndex == lastClip)
                    return GetNext();

                lastClip = returnIndex;
                return sounds[returnIndex];
            }
            return sounds[0];
        }
       
        public AudioClip GetNext()
        {
            if(sounds.Count > 1)
            {
                lastClip ++;
                if(lastClip >= sounds.Count)
                    lastClip = 0;
                return sounds[lastClip];
            }
            return sounds[0];
        }
        
        public AudioClip GetByIndex(int index)
        {
            if(sounds.Count > 1)
            {
                if(index >= sounds.Count)
                    return sounds[sounds.Count - 1];
                if(index < 0)
                    return sounds[0];
                return sounds[index];
            }
            return sounds[0];
        }
        public List<AudioClip> GetAll()
        {
            return sounds;
        }
    }
}