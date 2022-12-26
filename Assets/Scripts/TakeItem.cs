using UnityEngine;
using TMPro;

public class TakeItem : MonoBehaviour
{
	public TextMeshProUGUI CoinTxt;
	private int coin;
	
	void OnTriggerEnter2D(Collider2D collision) 
	{
		if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coin++;
            CoinTxt.text = "Монеты: " + coin;
        }
	}
}
