using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductBox : MonoBehaviour
{
    public List<GameObject> productObjectList = new List<GameObject>();     //�ڽ� �ȿ� ��� �ִ� ������Ʈ�� ���� List
    public List<Transform> productPosition = new List<Transform>();         //������Ʈ�� ��ġ�� ��ġ

    public void GenerationProduct(ProductData product)      //�ڽ��� ������ �� �ش� product�� �°� product�� ������Ʈ�� �����ϴ� �Լ�
    {
        for (int i = 0; i < productPosition.Count; i++) 
        {
            GameObject obj = Instantiate(product.ProductModel);
            obj.transform.SetParent(productPosition[i]);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

            productObjectList.Add(obj);
        }
    }

    public GameObject RemoveProduct(GameObject productObj)  //�ڽ� �ȿ� �ִ� ��ǰ���� ����� �Լ�
    {
        if (productObjectList.Count > 0)            //�ڽ� �ȿ� ��ǰ�� �ϳ��� �ִٸ�
        {
            productObjectList.Remove(productObj);   //productObjectList���� �ش� ��ǰ�� �����.
            Debug.Log("�ϳ� ���");
        }
        else
        {
            Debug.Log("�ڽ��� ������");
        }
        return null;
    }

    public GameObject InsertProduct(GameObject productObj)      //��ǰ�� �ڽ��� �ִ� �Լ�
    {
        Product newProduct = productObj.GetComponent<Product>();
        Product boxProduct = productObjectList[productObjectList.Count - 1].GetComponent<Product>();    //���� ��ǰ�� �ڽ� �ȿ� ����ִ� ��ǰ�� Product ������Ʈ�� ���� �� ��

        if (newProduct.product.Index == boxProduct.product.Index)   //�� ��ǰ�� Index�� ����
        {
            if (productObjectList.Count < productPosition.Count)    //�ڽ� ���� �ִ� ��ǰ ���� �ڽ� ĭ ������ ���ٸ�
            {
                productObjectList.Add(productObj);                  //�ش� ��ǰ�� productObjectList�� �ִ´�.
                productObj.transform.SetParent(productPosition[productObjectList.Count - 1]);
                productObj.transform.localPosition = Vector3.zero;
                productObj.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }
        } 
        return null;
    }
}
