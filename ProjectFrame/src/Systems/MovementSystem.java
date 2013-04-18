/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.IntervalEntityProcessingSystem;
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
		super(Aspect.getAspectForAll(WorldPositionComponent.class,VelocityComponent.class), interval);
		// TODO Auto-generated constructor stub
	}

	/* (non-Javadoc)
	 * @see com.artemis.systems.IntervalEntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		WorldPositionComponent w = this.wc.getSafe(e);
		VelocityComponent v = this.vc.getSafe(e);
		//Logger.Log("set X to " + w.x + "" + System.currentTimeMillis());
		w.x += v.x;
		w.y += v.y;
		if(v.x != 0 || v.y != 0 )
		{
			v.x *= 1.1f;
			v.y *= 1.1f;

		}
		v.x = Math.max(Math.min(v.x, 10f), -10f);
		v.y = Math.max(Math.min(v.y, 10f), -10f);
	}

}
