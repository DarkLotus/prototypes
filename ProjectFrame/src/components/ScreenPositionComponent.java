/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class ScreenPositionComponent extends Component {
	private float x,y;

	public ScreenPositionComponent()
	{}
	
	public ScreenPositionComponent(float x, float y){
		this.x = x;
		this.y = y;
	}
	public float getX() {
		return x;
	}

	public void setX(float x) {
		this.x = x;
	}

	public float getY() {
		return y;
	}

	public void setY(float y) {
		this.y = y;
	}
	
	
}
