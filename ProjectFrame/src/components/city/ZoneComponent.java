/**
 * 
 */
package components.city;

import Enums.ZoneDensity;

import com.artemis.Component;

/**
 * @author James
 * Stores player data, Score,Lives,Money etc
 */
public class ZoneComponent extends Component {
	public ZoneDensity Density = ZoneDensity.Low;
	public int Population = 0;
	
	//Max pop is 100 * the zones density
	public int MaxPop(){
		return 100*(Density.ordinal()+1);
	}

	//Happiness based off comercial districts in range? - industrial zones + parks?
	public int Happiness = 50;

	//public int Employeed = 0;


	public ZoneComponent()
	{}
	
	@Override
	public String toString(){
		return "P:" + Population + "H:" + Happiness;
	}
}
