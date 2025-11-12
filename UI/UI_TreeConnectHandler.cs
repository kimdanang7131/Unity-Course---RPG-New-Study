using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UI_TreeConnectDetails // 자식에 맞춰서 세팅할 값
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
    [Range(-50f, 50f)] public float rotation;
}

[ExecuteAlways]
public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails; // 자식에 맞춰서 세팅될 값
    [SerializeField] private UI_TreeConnection[] connections; // 자식 - width, rotation을 해서 화살표 길이,회전 조절하는 스크립트

    private Image connectionImage;
    private Color originalColor;

    void Awake()
    {
        if (connectionImage != null)
            originalColor = connectionImage.color;
    }
    
    private void OnValidate()
    {
        if (connectionDetails.Length <= 0)
            return;

        if (connectionDetails.Length != connections.Length)
        {
            Debug.Log("Amount of detalis should be same as amount of connection" + gameObject.name);
            return;
        }

        UpdateAllConnections();
    }

    public void UpdateConnections()
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            UI_TreeConnectDetails detail = connectionDetails[i];
            UI_TreeConnection connection = connections[i];

            Vector2 targetPosition = connection.GetConnectionPoint(rect);
            Image connectionImage = connection.GetConnectionImage();

            connection.DirectConnection(detail.direction, detail.length, detail.rotation);

            if (detail.childNode == null)
                continue;

            detail.childNode.SetPosition(targetPosition);
            detail.childNode.SetConnectionImage(connectionImage);
            detail.childNode.transform.SetAsLastSibling();
        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnections();

        foreach (var node in connectionDetails)
        {
            if (node.childNode == null) 
                continue;

            node.childNode.UpdateConnections();
        }
    }

    public void UnlockConnectionImage(bool unlocked)
    {
        if (connectionImage == null)
            return;

        connectionImage.color = unlocked ? Color.white : originalColor;
    }

    public void SetConnectionImage(Image image) => connectionImage = image;
    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;
}
