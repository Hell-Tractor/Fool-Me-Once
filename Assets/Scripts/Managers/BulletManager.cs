using UnityEngine;

public class BulletManager : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().Kill(null);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Ground") {
            Destroy(this.gameObject);
        }
    }
}