using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Renderer[] renderers;

    public Material[] materials;

    public float changeTime = 2f;
    public bool needRandomMaterial = false;

    private float timer;
    private int currentIndex;


    void Start()
    {
        ChangeAllMaterials();
    }

    void Update()
    {



        timer += Time.deltaTime;

        if (timer >= changeTime)
        {
            timer = 0f;

            currentIndex++;

            if (currentIndex >= materials.Length)
            {
                currentIndex = 0;
            }

            if (needRandomMaterial)
            {
                RandomizeMaterials();
            }
            else
            {
                ChangeAllMaterials();
            }

        }
    }

    void ChangeNeedRandomMaterial()
    {
        needRandomMaterial = !needRandomMaterial;
    }

    void ChangeAllMaterials()
    {
        foreach (Renderer rend in renderers)
        {
            rend.material = materials[currentIndex];
        }
    }

    public void RandomizeMaterials()
    {
        foreach (Renderer rend in renderers)
        {
            int randomIndex = Random.Range(0, materials.Length);

            rend.sharedMaterial = materials[randomIndex];
        }
    }
}
