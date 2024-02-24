using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotAnim : MonoBehaviour
{
    //Alibaba
    bool startAnim = false;
    bool animPlaying = false;

    [SerializeField] float RotationSpeed = 5f;
    [SerializeField] float fallSpeed = 1.5f;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 255, 0);
    }
    private void Update()
    {

        if (startAnim)
        {
            startAnim = false;
            animPlaying = true;
            transform.localPosition = startPos;
        }

        if (animPlaying)
        {
            if (transform.localPosition == endPos)
            {
                animPlaying = false;
                spriteRenderer.color = new Color(0, 0, 255, 0);
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, Time.deltaTime*fallSpeed);

        }

        transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
    }

    public void PlayAnim()
    {
        if (!animPlaying)
        {
            startAnim = true;
            spriteRenderer.color = new Color(0, 0, 255, 255);
        }
    }
}
