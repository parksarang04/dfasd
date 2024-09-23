using System.Collections.Generic;
using UnityEngine;

public class ShelfCtrl : MonoBehaviour
{
    public List<Transform> productPosList = new List<Transform>();      //��ǰ ��ġ�� ��ġ�� ���
    public Stack<GameObject> productList = new Stack<GameObject>();     //������ �ȿ� ��� �ִ� ��ǰ ���
    private ProductBox productBox;

    public bool DisplayProduct(GameObject productobj)
    {
        Transform nullPos = null;
        foreach (Transform pos in productPosList)
        {
            if (pos.childCount == 0)    //productPosList�� ��� �ִ� Transforom �� ��ǰ�� �ȵ� Transform�� �ִٸ� break
            {
                nullPos = pos;
                break;
            }
        }
        if (nullPos != null)                                //��� �ִ� Transform�� ������
        {
            if (productList.Count < productPosList.Count)   //������ ���� �ִ� ��ǰ ���� ������ ĭ ������ ����
            {
                if (productList.Count == 0)                 //���� ���� ������Ʈ�� productList�� ù ������Ʈ���
                {
                    Transform availablePosition = productPosList[productList.Count];
                    productobj.transform.SetParent(availablePosition);
                    productobj.transform.localPosition = Vector3.zero;
                    productobj.transform.localScale = Vector3.one;

                    productList.Push(productobj);           //productList�� �ش� ������Ʈ�� Push �Ѵ�.

                    Debug.Log("�����뿡 ��ǰ ����");
                    return true;

                }
                else if (productList.Count != 0)            //���� ���� ������Ʈ�� productList�� ù ������Ʈ�� �ƴϸ�
                {
                    Product shelfProduct = productList.Peek().GetComponent<Product>();
                    Product newProduct = productobj.GetComponent<Product>();            //productList�� ���� ���� �ִ� ������Ʈ�� ���� ������Ʈ�� Product ������Ʈ�� ���� �� ��
                    if (shelfProduct.product.Index == newProduct.product.Index)         //���� product Index�� ���Ͽ� Index�� ���� ���
                    {
                        Transform availablePosition = productPosList[productList.Count];
                        productobj.transform.SetParent(availablePosition);
                        productobj.transform.localPosition = Vector3.zero;
                        productobj.transform.localScale = Vector3.one;

                        productList.Push(productobj);                                   //productList�� �ش� ������Ʈ�� Push �Ѵ�.

                        Debug.Log("�����뿡 ��ǰ ����");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void MoveProductToBox(GameObject productObj)
    {
        if (productList.Count != 0)     //������ ���� ����ִ� ��ǰ�� �ִٸ�
        {
            productList.Pop();          //productList�� ���� ���� �ִ� ��ǰ�� �����.
        }
    }

    public void PickUpProduct(int count)
    {
        Debug.Log($"{gameObject.name}�� ���� ���� ������ �� : {productList.Count}");
    }
}
