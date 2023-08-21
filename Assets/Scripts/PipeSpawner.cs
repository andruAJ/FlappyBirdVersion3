using System.Collections;
using UnityEngine;

//NO SE POR QUÉ MI COMPIUTADOR YA NO ABRE EL PROYECTO DESDE UNITY, ASÍ QUE TUVE QUE HACER LOS CAMBIOS DE UN REPOSITORIO A OTRO DESDE GITHUB DIRECTAMENTE. No tengo manera de comprobar si los cambios siguen funcionando bien en unity y no pude meter los prefabs a los scripts porque unity ya no abre el proyecto, incluso si lo borro y lo vuelvo a clonar
namespace FlappyBird
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipe;
        [SerializeField]
        private Transform spawnPoint;

        [Space, SerializeField, Range(-1, 1)]
        private float minHeight;
        [SerializeField, Range(-1, 1)]
        private float maxHeight;

        [Space, SerializeField]
        private float timeToSpawnFirstPipe;
        [SerializeField]
        private float timeToSpawnPipe;

        private GameManager gameManager;

         private List<GameObject> PoolPipes = new List<GameObject>();

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(SpawnPipes());
        }

        private GameObject GetPooledPipes()
        {
            foreach (GameObject pipe in PoolPipes)
            {
                if (!pipe.activeInHierarchy)
                {
                    pipe.SetActive(true);
                    ChangePipeColor(newPipe);
                    return pipe;
                }
            }

            GameObject newPipe = Instantiate(pipe);
            pipePool.Add(newPipe);
            return newPipe;
        }

         private void ChangePipeColor(GameObject pipe)
         {
             SpriteRenderer pipeRenderer = pipe.GetComponent<SpriteRenderer>();

    
                switch (gameManager.selectedColor)
                {
                    case BirdColor.Yellow:
                      pipeRenderer.sprite = yellowPipe;
                        break;
                    case BirdColor.Red:
                        pipeRenderer.sprite = redPipe;
                        break;
                    case BirdColor.Blue:
                        pipeRenderer.sprite = bluePipe;
                        break;
                }
         }

        private Vector3 GetSpawnPosition()
        {
            return new Vector3(spawnPoint.position.x, Random.Range(minHeight, maxHeight), spawnPoint.position.z);
        }

        private IEnumerator SpawnPipes()
        {
            yield return new WaitForSeconds(timeToSpawnFirstPipe);

            while (true)
            {
                GameObject newPipe = GetPooledPipe();
                newPipe.transform.position = GetSpawnPosition();

                yield return new WaitForSeconds(timeToSpawnPipe);
            }
        }

        public void Stop()
        {
            StopAllCoroutines();
        }
    }
}
