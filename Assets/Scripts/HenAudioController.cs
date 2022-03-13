using UnityEngine;

public class HenAudioController : MonoBehaviour
{
    public AudioListener mainListener;
    public AudioSource audioSrc;

    public float minDistance = 1;
    public float maxDistance = 400;

    private void Update()
    {
        var listenerDistance = Vector2.Distance(mainListener.transform.position, transform.position);

        if (listenerDistance < minDistance)
        {
            audioSrc.volume = 1;
        }
        else if (listenerDistance > maxDistance)
        {
            audioSrc.volume = 0;
        }
        else
        {
            audioSrc.volume = 1 - ((listenerDistance - minDistance) / (maxDistance - minDistance));
        }
    }
}