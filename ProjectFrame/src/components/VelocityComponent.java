/**
 * 
 */
package components;

import helpers.Persistant;

import com.artemis.Component;

/**
 * @author James
 * Provides Velocity for movable entities
 */
public class VelocityComponent extends Component {
	@Persistant
	public float x;
	@Persistant
	public float y;

	public VelocityComponent()
	{}

	public VelocityComponent(float x, float y){
		this.x = x;
		this.y = y;
	}

}
