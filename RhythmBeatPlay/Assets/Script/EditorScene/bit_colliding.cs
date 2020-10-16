using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bit_colliding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bit_Hitpoint")
        {
            Debug.Log("엥");
            StartCoroutine(BitHit());
            Destroy(collision.gameObject, 1.0f);
        }
    }

    IEnumerator BitHit()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0.76f, 0.82f, 1);
        yield return new WaitForSeconds(0.02f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1);
    }


}
