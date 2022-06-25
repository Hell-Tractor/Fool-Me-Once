using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Skill {
    public class RandomDeployer : SkillDeployer {
        public Sprite[] Sprites;
        public float ShowDelay;
        public Image Image;
        private int _index = 0;

        public override void DeploySkill() {
            _calculateTargets();
            _showAnimation(() => this._impactTargets());
        }

        private async void _showAnimation(Action onComplete) {
            for (float timer = 0; timer < SkillData.durationTime; timer += SkillData.atkInterval) {
                Image.sprite = Sprites[_index];
                _index = (_index + 1) % Sprites.Length;
                await Task.Delay(Mathf.RoundToInt(SkillData.atkInterval * 1000));
            }

            int randomIndex = SkillData.value % Sprites.Length;

            Image.sprite = Sprites[randomIndex];
            await Task.Delay(Mathf.RoundToInt(ShowDelay * 1000));

            onComplete?.Invoke();
        }
    }
}