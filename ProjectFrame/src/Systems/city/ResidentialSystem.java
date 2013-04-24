/**
 * 
 */
package Systems.city;

import java.util.Random;


import Managers.ZoneManager;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.IntervalEntityProcessingSystem;
import com.artemis.utils.ImmutableBag;
import com.mythiksoftware.ProjectFrame.Logger;

import components.OnCursorComponent;
import components.city.ResidentialComponent;

/**
 * @author James
 *
 */
public class ResidentialSystem extends IntervalEntityProcessingSystem {
	@Mapper
	ComponentMapper<ResidentialComponent> rc;

	@Mapper
	ComponentMapper<OnCursorComponent> oc;
	Random rand = new Random();
	/**
	 * @param aspect
	 * @param interval
	 */
	public ResidentialSystem(float interval) {
		super(Aspect.getAspectForAll(ResidentialComponent.class), interval);
		// TODO Auto-generated constructor stub
	}

	@Override
	protected void begin() {

	}



	/* (non-Javadoc)
	 * @see com.artemis.systems.IntervalEntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		
		ResidentialComponent r = this.rc.getSafe(e);
		OnCursorComponent o = this.oc.getSafe(e);
		if(o != null)
			return; // skip if item is on cursor.
		
		ImmutableBag<Entity> shops = ZoneManager.GetZonesInRange(e,ZoneTypes.Commercial,300);
		ImmutableBag<Entity> industrial = ZoneManager.GetZonesInRange(e,ZoneTypes.Industrial,10);
		r.Happiness = 50 + (shops.size() * 10) - (industrial.size() * 10);

		int rand = this.rand.nextInt(r.Happiness);
		if(rand > 40) {
			r.Population++;
		}
		else if(rand < 20){
			r.Population--;
		}
		
		
		if(r.Population > r.MaxPop){
			r.Population = r.MaxPop;
		}
		if(r.Population < 0){ r.Population = 0;}
		Logger.Log("Residential zone reached :" + r.Population);

	}

}
