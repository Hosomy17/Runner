using UnityEngine;
using System.Collections.Generic;
namespace ManagerFrameWork
{

    public class SoundManager
    {
	    private static SoundManager soundManager;
	    private Dictionary<string, AudioClip> audios;

	    private SoundManager ()
	    {
		    audios = new Dictionary<string, AudioClip>();
	    }

	    public static SoundManager Instance
	    {
		    get
		    {
			    if (soundManager == null)
			    {
				    soundManager = new SoundManager();
			    }
			    return soundManager;
		    }
	    }

	    public void Play(string audio)
	    {
		    AudioClip src;
		    if (!audios.ContainsKey(audio))
		    {
			    src = Resources.Load("Sounds/" + audio) as AudioClip;
                audios.Add(audio,src);
		    }
		    src = audios[audio];
            
		    AudioSource.PlayClipAtPoint(src, Camera.main.transform.position);
	    }
    }
}