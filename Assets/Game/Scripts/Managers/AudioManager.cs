using UnityEngine;

public class AudioManager : MonoBehaviour, IService, IGameFinishListener
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _startSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _jumpSound;


    public void OnFinishGame() => _audioSource.PlayOneShot(_loseSound);
    
    public void PlayStart() => _audioSource.PlayOneShot(_startSound);
    public void PlayJump() => _audioSource.PlayOneShot(_jumpSound);
}
