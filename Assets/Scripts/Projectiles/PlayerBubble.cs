using UnityEngine;
using GGJ2025.Utilities;
using UnityEngine.InputSystem;
using GGJ2025.Managers;

namespace GGJ2025
{
    public class PlayerBubble : Projectile
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            SetInitialData(Constants.Projectiles.PlayerBubble);

            // Remove the direction calculation from here since we'll set it externally
            Size = Random.Range(Size * Constants.PLAYER_BUBBLE_MINIMUM_SIZE_SCALE, 
                            Size * Constants.PLAYER_BUBBLE_MAXIMUM_SIZE_SCALE);
            transform.localScale = new Vector3(Size, Size, 1);
            Speed = Random.Range(Speed * Constants.PLAYER_BUBBLE_MINIMUM_SPEED_SCALE, 
                            Speed * Constants.PLAYER_BUBBLE_MAXIMUM_SPEED_SCALE);
        }

        public void SetInitialDirection(Vector2 direction) 
        {
            Direction = direction;

            // Apply random spread after setting the base direction:
            transform.Rotate(0, 0, Random.Range(-Constants.PLAYER_BUBBLE_MAXIMUM_ANGLE_SPREAD, 
                                          Constants.PLAYER_BUBBLE_MAXIMUM_ANGLE_SPREAD));
        }

        private void OnDestroy()
        {
            AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BubbleHit);
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        protected override (bool shouldDamage, bool shouldDestroy) GetHitHandling(Collider2D other)
        {
            var shouldDestroy = !other.CompareTag("Player") && !other.isTrigger;
            var shouldDamage = shouldDestroy && !other.TryGetComponent<SoapedObject>(out _);
            return (shouldDamage, shouldDestroy);
        }

        #endregion
    }
}
