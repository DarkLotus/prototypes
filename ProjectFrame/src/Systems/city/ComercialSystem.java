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
import components.city.ComercialComponent;
import components.city.ResidentialComponent;

/**
 * @author James
 *
 */
public class ComercialSystem extends IntervalEntityProcessingSystem {
	@Mapper
	ComponentMapper<ComercialComponent> cc;
	@Mapper
	ComponentMapper<ResidentialComponent> rc;
	@Mapper
	ComponentMapper<OnCursorComponent> oc;
	Random rand = new Random();
	/**
	 * @param aspect
	 * @param interval
	 */
	public ComercialSystem(float interval) {
		super(Aspect.getAspectForAll(ComercialComponent.class), interval);
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
		ComercialComponent c = this.cc.getSafe(e);
		OnCursorComponent o = this.oc.getSafe(e);
		if(o != null)
			return; // skip if item is on cursor.
		c.Customers = 0;
		c.Employees = 0;
		ImmutableBag<Entity> residental = ZoneManager.GetZonesInRange(e,ZoneTypes.Residential,300);
		for (int _i = 0; _i < residental.size(); _i++) {
			Entity _entity = residental.get(_i);;
			ResidentialComponent residentialComponent = this.rc.get(_entity);
			c.Customers += residentialComponent.Population;
			c.Employees += (residentialComponent.Population / 5);
			
		}
		if(c.Employees == 0) {
			return;
		}
		if(c.Employees > c.MaxEmployees)
			c.Employees = c.MaxEmployees;
		c.Tax = (c.Customers - c.Employees) * 10;
		Logger.Log("Comercial zone had :" + c.Customers + " customers, " + c.Employees + " Employees " + c.Tax + " Paid");

	}

}
