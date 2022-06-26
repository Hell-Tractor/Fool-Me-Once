using UnityEngine;

public class BulletManager : MonoBehaviour {
    private Animator _animator;

    private void Start() {
        _animator = this.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().Kill(null);
            this.Destory();
        }
        if (collision.gameObject.tag == "Ground") {
            this.Destory();
        }
    }

    public void Destory() {
        _animator?.SetTrigger("OnDestory");
        Destroy(this.gameObject);
    }
}