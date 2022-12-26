using UnityEngine;
using TMPro; // работа с текстом

public class Spawner : MonoBehaviour
{
    public float minX, maxX; // min-max координаты создания платформ по X
    public float YrangeMin, YrangeMax; // min-max расстояния м-у платформами по Y
    public float cameraDistance; // начало создания платформ перед камерой
    public Transform platformPrefab, platformMovePrefab; // ссылки на прф.platform, прф.platformMove
    public double percentSpawn; // вероятность создания прф.platform
    private Transform cam; // ссылка на камеру
    private float lastSpawnY; // последнее место создания платформы по Y
    private float rangeIncreaser; // увеличение расстояния м-у платформами по Y
    public TextMeshProUGUI ScoreTxt; // ссылка на кмп.TextMeshPro-Text(UI) об.Canvas.Text(TMP)
    private int score; // очки персонажа
	private int count;
	public Transform coinPrefab;

    void Start()
    {
		cam = Camera.main.transform; // ссылка на об.MainCamera
		lastSpawnY = 0;
		count = 20;
    }

    void Update()
    {
		ScoreTxt.text = "Очки: " + score;

		if (lastSpawnY < 200) // условие для увеличения расстояния м-у платформами по Y
		{
			rangeIncreaser = Mathf.Floor (lastSpawnY / 50); // округляем до меньшего целого
		}

		if (cam.position.y + cameraDistance > lastSpawnY)
		{
			Transform platform;
			Transform coin;

			if (Random.value < percentSpawn) // Random.value - случайное число 0..1
				platform = Instantiate (platformPrefab); // создаём платформу из префаба "platformPrefab"
			else
				platform = Instantiate (platformMovePrefab); // создаём платформу из префаба "platformMovePrefab"

			platform.position = new Vector3 ( // устанавливаем координаты платформы
			Random.Range(minX, maxX), // координата X
			lastSpawnY + Random.Range(YrangeMin+(rangeIncreaser*0.9f), YrangeMax+(rangeIncreaser*1.1f)), // координата Y
			0); // координата Z
			
			platform.transform.localScale = new Vector3(platform.transform.localScale.x * (Random.value) * + 3, platform.transform.localScale.y, platform.transform.localScale.z * (Random.value) + 3);
			
			if (Random.value < 0.1)
				coin = Instantiate (coinPrefab);
				coinPrefab.position = new Vector3 (Random.Range(minX, maxX), lastSpawnY + Random.Range(YrangeMin+(rangeIncreaser*0.9f), YrangeMax+(rangeIncreaser*1.1f)), 0);

			lastSpawnY = platform.position.y;

			if (lastSpawnY-12 > 0) // чтобы очков было меньше, примерно 0 за нулевое положение дудлера
				score = Mathf.CeilToInt(lastSpawnY-12); // Mathf.CeilToInt() - округление float в int
			
			if (score > count && percentSpawn != 0)
			{
				percentSpawn = percentSpawn - 0.1;
				count = score + 20;
			}
		}
	}
}