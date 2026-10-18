using UnityEngine;

[CreateAssetMenu(fileName = "PlaySoundBehaviour", menuName = "Scriptable Objects/Sound/PlaySound")]
public class PlaySoundBehaviour : FE_Behaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource player;
    public AudioClip audioClip;

    public override void Init(GameObject owner)
    {
        base.Init(owner);

        player = Camera.main.GetComponent<AudioSource>();
        

        player.clip = audioClip; 
    }

    public override void OnActivate()
    {
        base.OnActivate();
        player.Play();
    }
}
