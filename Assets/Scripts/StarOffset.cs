using UnityEngine;

public class StarOffset : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        anim.StartPlayback();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        anim.speed = Random.Range(0.8f, 1.2f);
    }
}
