using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill {
    public class TianQiZhiZiDeployer : SkillDeployer {
        public float GenerateInterval;
        public int GenerateCount;
        public GameObject BulletPrefab;
        public Vector2 InitBulletSpeed;

        private int _maxCount;
        private float _timer;
        
        public override void DeploySkill() {
            _init();
            StartCoroutine(_update());
        }

        private void _init() {
            _maxCount = Mathf.FloorToInt(Screen.width / BulletPrefab.transform.localScale.x);
            _timer = 0f;
        }

        private IEnumerator _update() {
            while (_timer < SkillData.durationTime) {
                int[] positionId = Utility.GetRandomArray(0, _maxCount - 1, Mathf.Min(GenerateCount, _maxCount));
                foreach (int id in positionId) {
                    Vector3 position = new Vector3((id + 0.5f) * BulletPrefab.transform.localScale.x, Screen.height);
                    this._generateOneBullet(Camera.main.ScreenToWorldPoint(position));
                }
                _timer += GenerateInterval;
                yield return new WaitForSeconds(GenerateInterval);
            }
            this.Destroy();
        }

        private void _generateOneBullet(Vector3 position) {
            position.z = 0;
            GameObject bullet = Instantiate(BulletPrefab, position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = InitBulletSpeed;
            bullet.transform.parent = this.transform;
        }
    }
}