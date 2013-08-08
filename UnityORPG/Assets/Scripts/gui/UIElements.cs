using System;
using UnityEngine;

namespace Assets.Scripts.gui
{
	
		public class UIElement{
            public bool Enabled = true;
            public object Tag;
		    public virtual void Draw(){}
	    }
	
	
		public class TextBox : UIElement {
		public string Text;
		public Rect Rectangle;
		
		public TextBox(String text, Rect rect){
			this.Text = text;
			this.Rectangle = rect;
			DarkGUI.Instance.AddUIElement(this);//TODO Move to function?
		}
		
		public override void Draw () {
            if (Enabled)
			this.Text = GUI.TextField(Rectangle,Text);
		}
		
	}

        public class CheckBox : UIElement
        {

            public bool Checked;
            public string Text;
            public Rect Rectangle;

            public CheckBox(String text, Rect rect) {
                this.Text = text;
                this.Rectangle = rect;
                Checked = false;
                DarkGUI.Instance.AddUIElement(this);//TODO Move to function?
            }

            public override void Draw() {
                if (Enabled)
                    GUI.Toggle(Rectangle, Checked, Text);
            }

        }

	
	public class Label : UIElement {
        

		public string Text;
		public Rect Rectangle;
		
		public Label(String text, Rect rect){
			this.Text = text;
			this.Rectangle = rect;
			DarkGUI.Instance.AddUIElement(this);//TODO Move to function?
		}
		
		public override void Draw () {
            if (Enabled)
			GUI.Label(Rectangle,Text);
		}
		
	}
	
	public class Button : UIElement {
		public delegate void ClickEventHandler(Button sender);
		public event ClickEventHandler OnClick;
		
		public Rect Rectangle;
		public string Text;
		
				
		
		
		public Button(String text, int x = 0,int y = 0,int width = 100,int height = 50){
			
			this.Text = text;
			this.Rectangle = new Rect(x,y,width,height);
			Logger.Log("Button constructor called");
			DarkGUI.Instance.AddUIElement(this);//TODO Move to function?
			}
        public Button setTag(object value) { Tag = value; return this; }
		public override void Draw ()
		{
			if(Enabled)
				if(GUI.Button(this.Rectangle,this.Text))
					if(OnClick != null){
					OnClick(this);
				}
					
		}			
	}
		
}

