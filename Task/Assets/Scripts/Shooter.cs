using UnityEngine;

namespace FueroGamesTask.Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] Bullet bulletPrefab = null;
        [SerializeField] float shootCooldown = 0.5f;
        [SerializeField] float bulletLifespan = 3;
        [SerializeField] Transform gun = null;
        [SerializeField] Transform bullets = null;

        int lastBulletIndex = 0;
        float timeSinceLastShot = 0;
        Bullet[] bulletsPool;
        PlayerController player;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
        }

        private void Start()
        {
            CreateBulletsPool();
        }

        private void CreateBulletsPool()
        {
            int bulletsNeeded = (int)(bulletLifespan / shootCooldown) + 1;
            bulletsPool = new Bullet[bulletsNeeded];
            for (int i = 0; i < bulletsPool.Length; i++)
            {
                bulletsPool[i] = Instantiate(bulletPrefab, bullets);
                bulletsPool[i].Deactivate(0);
            }
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootCooldown)
            {
                Shoot();
                timeSinceLastShot = 0;
            }
        }

        private void Shoot()
        {
            lastBulletIndex++;
            if (lastBulletIndex >= bulletsPool.Length)
            {
                lastBulletIndex = 0;
            }
            bulletsPool[lastBulletIndex].Shoot(gun.position, transform.rotation);
            bulletsPool[lastBulletIndex].Deactivate(bulletLifespan);
        }
    }
}