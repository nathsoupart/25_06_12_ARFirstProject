using UnityEngine;

public class creationliste : MonoBehaviour 



{

    public GameObject[] prefabArray;
    public ObjectData[] objectData;

   
    [System.Serializable]
    public class ObjectData {
        public string objectName;
        public string description;
        public int value;
    }

    void Start()
    {
        
        for (int i = 0; i < prefabArray.Length; i++) 
        {
            
            GameObject newObject = Instantiate(prefabArray[i], transform.position, Quaternion.identity);

            
            if (objectData != null && i < objectData.Length) 
            {
                
                newObject.name = objectData[i].objectName;
                
                
            }
        }
    }
    
}
   
    

