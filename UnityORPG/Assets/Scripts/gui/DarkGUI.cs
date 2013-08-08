using System;
using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts.gui
{
	public class DarkGUI : MonoBehaviour
	{
		public static DarkGUI Instance;
		public List<UIElement> UIElements = new List<UIElement>();

        List<UIElement> toAdd = new List<UIElement>();
        private bool bClearNextRefresh;
		
		
		public DarkGUI ()
		{
		}
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        for(int i = 0; i < UIElements.Count;i++)
            UIElements[i].Draw();

            if (bClearNextRefresh) {
                UIElements.Clear();
                bClearNextRefresh = false;
            }
            if (toAdd.Count != 0)
                UIElements.AddRange(toAdd);
            toAdd.Clear();

		}
		
		public void AddUIElement(UIElement ui){
            this.toAdd.Add(ui);
			Logger.Log("added Ui Element");
		}

        public void ClearAllUI() { bClearNextRefresh = true;  }
		
	}

	
	
}

