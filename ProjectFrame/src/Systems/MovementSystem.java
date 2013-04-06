/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.IntervalEntityProcessingSystem;
import com.mythiksoftware.ProjectFrame.Logger;

import components.VelocityComponent;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class MovementSystem extends IntervalEntityProcessingSystem {
	@Mapper
	ComponentMapper<WorldPositionComponent> wc;
	@Mapper
	ComponentMapper<VelocityComponent> vc;
	/**
	 * @param aspect
	 * @param interval
	 */
	public MovementSystem(float interval) {
		super(Aspect.getAspectForAll(WorldPositionComponent.class), interval);
		// TODO Auto-generated constructor stub
	}

	/* (non-Javadoc)
	 * @see com.artemis.systems.IntervalEntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		WorldPositionComponent w = wc.getSafe(e);
		VelocityComponent v = vc.getSafe(e);
		Logger.Log("set X to " + w.getX());
		
	}

}
