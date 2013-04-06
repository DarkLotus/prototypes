/**
 * 
 */
package components;

import helpers.Persistant;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class WorldPositionComponent extends Component {
	@Persistant
	public float x;
	@Persistant
	public float y;
	

	public WorldPositionComponent()
	{
		
	}
	
	public WorldPositionComponent(float x, float y){
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
