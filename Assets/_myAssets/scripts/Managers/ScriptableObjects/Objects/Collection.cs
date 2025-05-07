using UnityEngine;


public class Collection : MonoBehaviour 
{
    public CollectedItems score;
    public Collectible item;
	//public int scoreValue = 1;



	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			
			//Destroy(other.gameObject);
			//CollectionAndScore.Score += 1;
			// Increase the score by the enemy's score value.
			score.score += item.value;
            Destroy(gameObject);
            }
	}

}
