/**
 * 
 */
package components;

import helpers.Persistant;

import com.artemis.Component;

/**
 * @author James
 * provides a world position for drawable entities
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


}
