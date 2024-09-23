using System.Collections.Generic;
using UnityEngine;

public class TestPlayerCtrl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("Look")]
    public float mouseSpeed;
    public float yRotation;
    public float xRotation;
    public Camera cam;

    public GameObject playerHand;

    [HideInInspector]
    public bool canLook = true;

    public ProductBox productBox;
    private ShelfCtrl shelf;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ȭ�� �ȿ��� ����
        Cursor.visible = false;                     //���콺 Ŀ���� ������ �ʵ��� ����

        cam = Camera.main;
    }

    void Update()
    {
        PlayerMove();
        CameraLook();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))                                //��Ŭ�� ���� ��
            {
                if (hit.collider.CompareTag("ProductBox"))                  //ray�� ���� �ݶ��̴��� ���� �ִ� �±װ� "ProductBox"���
                {
                    productBox = hit.collider.GetComponent<ProductBox>();
                    hit.collider.gameObject.transform.parent = playerHand.transform;    //ray�� ���� "ProductBox" �±׸� ���� ������Ʈ�� playerHand �ڽĿ� �ִ´�.
                    hit.collider.gameObject.transform.localPosition = Vector3.zero;
                    hit.collider.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);   //ray�� ���� "ProductBox" �±׸� ���� ������Ʈ�� ũ�⸦ x:0.3, y:0.3, z:0.3���� �ٲ۴�.
                }

                if (hit.collider.CompareTag("Shelf"))                       //���� �ݶ��̴��� ���� �ִ� �±װ� "Shelf"�� ��
                {
                    if (productBox != null)                                 //productBox�� ��� �ִٸ�
                    {
                        ShelfCtrl shelfCtrl = hit.collider.GetComponent<ShelfCtrl>(); ; //ray�� ���� ������Ʈ���Լ� ShelfCtrl ������Ʈ�� ���� �´�.
                        if (shelfCtrl != null)                                          //ShelfCtrl�� null�� �ƴ� ��
                        {
                            GameObject productObj = productBox.productObjectList[productBox.productObjectList.Count -1];    //productObj�� productBox.productObjectList�� ������ �ε��� ������Ʈ�̴�.
                            bool isDisplayed = shelfCtrl.DisplayProduct(productObj);    //�ߺ� üũ�� ���� DisplayProduct�� ��ȯ���� bool�� �߱� ������ ������ �Ǿ��ٸ� true�� ��ȯ�Ѵ�.
                            if (isDisplayed)    //������ �Ǿ��� ��
                            {
                                productBox.RemoveProduct(productObj);       //productBox�� RemoveProduct �Լ��� productObj ���ڸ� �����Ѵ�.
                            }
                            if (productBox.productObjectList.Count == 0)    
                            {
                                Debug.Log("���ڰ� ������~");
                            }
                        }
                    }
                }
                if (hit.collider.CompareTag("TrashCan"))                    //ray�� ���� ������Ʈ�� "TrashCan" �±׸� ������ ������
                {
                    if (productBox.productObjectList.Count == 0)            //��� �ִ� productBox�� ��ǰ�� �ϳ��� ���ٸ�
                    {
                        Destroy(productBox.gameObject);                     //��� �ִ� productBox�� ���ش�.
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))                                        //��Ŭ���� ���� ��
            {
                if (hit.collider.CompareTag("Shelf"))                               //ray�� ���� ������Ʈ�� "Shelf" �±׸� ������ �ִٸ�
                {
                    ShelfCtrl hitShelf = hit.collider.GetComponent<ShelfCtrl>();    //ray�� ���� ������Ʈ���Լ� ShelfCtrl ������Ʈ�� ���� �´�.
                    if (hitShelf.productList.Count != 0)                            //hitShelf�� ��ǰ�� �ϳ��� ���� �ִٸ�
                    {
                        GameObject productObj = hitShelf.productList.Peek();        //productObj�� hitShelf�� ���� �ִ� ��ǰ ����� ���� ���� �ִ� ������Ʈ �����͸� ������.
                        productBox.InsertProduct(productObj);
                        hitShelf.MoveProductToBox(productObj);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))                                                    //F�� ������ ��
            {
                if (productBox != null)                                                         //productBox�� ��� �ִٸ�
                {
                    productBox.transform.position = hit.point + new Vector3( 0f, 0.5f, 0f);     //��� �ִ� productBox�� hit�� ����Ʈ���� y�� 0.5f ���� ������ �̵��Ѵ�.
                    productBox.transform.localScale = Vector3.one;
                    productBox.transform.SetParent(null);
                }
            }
        }
    }

    void CameraLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void PlayerMove()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 moveVec = transform.forward * Vertical + transform.right * Horizontal;

        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}
