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

        bool canShoot = false;
        int lastBulletIndex = 0;
        float timeSinceLastShot = 0;
        Bullet[] bulletsPool;
        PlayerController player;
        GameManager gameManager;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
            gameManager = FindObjectOfType<GameManager>();
            gameManager.onGameStart += GameStarted;
            gameManager.onGameOver += GameOver;
        }

        private void Start()
        {
            CreateBulletsPool();
        }

        private void Update()
        {
            if (canShoot)
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= shootCooldown)
                {
                    Shoot();
                    timeSinceLastShot = 0;
                }
            }
        }

        private void GameStarted()
        {
            canShoot = true;
        }

        private void GameOver()
        {
            canShoot = false;
        }

        private void CreateBulletsPool()
        {
            int bulletsNeeded = (int)(bulletLifespan / shootCooldown) + 1;
            bulletsPool = new Bullet[bulletsNeeded];
            for (int i = 0; i < bulletsPool.Length; i++)
            {
                bulletsPool[i] = Instantiate(bulletPrefab, bullets);
                bulletsPool[i].DeactivateIn(0);
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
            bulletsPool[lastBulletIndex].DeactivateIn(bulletLifespan);
        }
    }
}