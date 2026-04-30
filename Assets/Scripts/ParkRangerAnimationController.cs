using UnityEngine;

public class ParkRangerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator rangerAnimator;

    public void PlayWave()
    {
        if (rangerAnimator != null)
        {
            rangerAnimator.SetTrigger("Wave");
        }
    }

    public void PlayTalk()
    {
        if (rangerAnimator != null)
        {
            rangerAnimator.SetTrigger("Talk");
        }
    }
}