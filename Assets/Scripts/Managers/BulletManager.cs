using UnityEngine;

public class BulletManager : MonoBehaviour {
    private Animator _animator;

    private void Start() {
        _animator = this.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().Kill(null);
            this.Destroy();
        }
        if (collision.gameObject.tag == "Ground") {
            this.Destroy();
        }
    }

    public void Destroy() {
        _animator?.SetTrigger("OnDestroy");
        Destroy(this.GetComponent<Collider2D>());
        Destroy(this.GetComponent<Rigidbody2D>());
        Destroy(this.gameObject, 0.67f);
    }
}