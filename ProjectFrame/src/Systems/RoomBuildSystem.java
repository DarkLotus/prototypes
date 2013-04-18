/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.Entity;
import com.artemis.systems.EntityProcessingSystem;
import components.RoomComponent;

/**
 * @author James
 *
 */
public class RoomBuildSystem extends EntityProcessingSystem {

	/**
	 * @param aspect
	 */
	public RoomBuildSystem(Aspect aspect) {
		super(aspect);
		// TODO Auto-generated constructor stub
	}

	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		// TODO Auto-generated method stub

	}

	/**
	 * @param roomComponent
	 */
	public void StartBuild(RoomComponent roomComponent) {
		// TODO Auto-generated method stub

	}

}
