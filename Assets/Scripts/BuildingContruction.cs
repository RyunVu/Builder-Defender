using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContruction : MonoBehaviour{

    public static BuildingContruction Create(Vector3 position, BuildingTypeSO buildingType) {
        Transform pfBuildingContruction = Resources.Load<Transform>("pfBuildingContruction");
        Transform BuildingContructionTransform = Instantiate(pfBuildingContruction, position, Quaternion.identity);

        BuildingContruction buildingContruction = BuildingContructionTransform.GetComponent<BuildingContruction>();
        buildingContruction.SetBuildingType(buildingType);

        return buildingContruction;
    }

    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material contructionMaterial;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        contructionMaterial = spriteRenderer.material;
    }

    private void Update() {
        constructionTimer -= Time.deltaTime;

        contructionMaterial.SetFloat("_Progress", GetContructionTimerNormalized());

        if (constructionTimer <= 0f ) {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType( BuildingTypeSO buildingType) {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.contructionTimerMax;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetContructionTimerNormalized() {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
