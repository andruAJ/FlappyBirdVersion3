using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private float timeToReloadScene;

        [Space, SerializeField]
        private UnityEvent onGameOver;
        [SerializeField]
        private UnityEvent onIncreaseScore;

        public bool isGameOver { get; private set; }
        public int scoreCount { get; private set; }

        public enum BirdColor { Yellow, Red, Blue }
        public enum TimeOfDay { Day, Night }
        

        public static GameManager Instance
        {
            get; private set;
        }

        private void Awake()
        {
            if (Instance != null)
                DestroyImmediate(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            private GameObject currentBirdInstance;
            selectedColor = (BirdColor)Random.Range(0, System.Enum.GetValues(typeof(BirdColor)).Length);
            selectedTime = (TimeOfDay)Random.Range(0, System.Enum.GetValues(typeof(TimeOfDay)).Length);
            
            ChangeBirdColor();
            ChangeTimeOfDay();
        }

        public void GameOver()
        {
            Debug.Log("GameManager :: GameOver()");

            isGameOver = true;

            if (onGameOver != null)
                onGameOver.Invoke();

            StartCoroutine(ReloadScene());
        }

        public void IncreaseScore()
        {
            scoreCount++;

            if (onIncreaseScore != null)
                onIncreaseScore.Invoke();
        }
        private IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(timeToReloadScene);

            SceneManager.LoadScene(0);
        }

        private void ChangeBirdColor()
        {
            GameObject birdPrefab = null;
 
                switch (selectedColor)
                {
                    case BirdColor.Yellow:
                    birdPrefab = yellowBirdPrefab;
                    break;
                    case BirdColor.Red:
                    birdPrefab = redBirdPrefab;
                    break;
                    case BirdColor.Blue:
                    birdPrefab = blueBirdPrefab;
                    break;
                }

   
                if (birdPrefab != null)
                {
                    Destroy(currentBirdInstance);
                    currentBirdInstance = Instantiate(birdPrefab, birdSpawnPoint.position, Quaternion.identity);
                }
        }

        private void ChangeTimeOfDay()
            {
                switch (selectedTime)
                {
                    case TimeOfDay.Day:
            
                        backgroundSpriteRenderer.sprite = dayBackgroundSprite; 
                        break;
                    case TimeOfDay.Night:
                    
                        backgroundSpriteRenderer.sprite = nightBackgroundSprite;
                        break;
                }
            }

    }
}
