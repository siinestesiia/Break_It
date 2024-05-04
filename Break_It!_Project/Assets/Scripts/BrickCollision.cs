using UnityEngine;


public class BrickCollision : MonoBehaviour
{
    [Header ("- Brick Properties -")]
    [Tooltip ("Amount of hits a Brick can take before being destroyed.")]
    [SerializeField] int hitPoints = 1;

    [Header ("- Brick Materials List -")]
    Renderer brickRenderer;
    [SerializeField]
    Material[] brickMaterials;


    void Start()
    {
        brickRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            hitPoints--;
            ProcessBrickDestruction(hitPoints);
            ChangeBrickColor(hitPoints);
        }
    }


    void ProcessBrickDestruction(int hitPoints)
    {
        if (hitPoints <= 0)
        {
            // Add particle system destruction.
            Destroy(gameObject);
        }
    }


    void ChangeBrickColor(int hitPoints)
    {
        switch (hitPoints)
        {
            case 1:
                brickRenderer.material = brickMaterials[0];
                break;
            case 2:
                brickRenderer.material = brickMaterials[1];
                break;
            case 3:
                brickRenderer.material = brickMaterials[2];
                break;
        }
    }
}
