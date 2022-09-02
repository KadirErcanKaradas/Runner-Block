using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed=1f;
    [SerializeField] private GameObject target;
    [SerializeField] private ParticleSystem confeettiPs;
    void Update()
    {
        if (GameManager.Instance.GameStage == GameStage.Started)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            anim.SetBool("walking", true);        
        }
        else if (GameManager.Instance.GameStage == GameStage.Fail)
        {
            anim.SetBool("walking",false);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            anim.SetBool("fail",true);
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            GameManager.Instance.SetGameStage(GameStage.FailFall);

        }
        if (other.gameObject.CompareTag("Finish"))
        {
            anim.SetBool("win", true);
            confeettiPs.Play();
            GameManager.Instance.SetGameStage(GameStage.Win);
        }
    }
}
