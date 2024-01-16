using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelStaticScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float staticSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(staticSpeed * Time.deltaTime, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneController.instance.ResetScene();
        }
    }
}
