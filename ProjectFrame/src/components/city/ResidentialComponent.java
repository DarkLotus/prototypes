/**
 * 
 */
package components.city;

import com.artemis.Component;

/**
 * @author James
 * Stores player data, Score,Lives,Money etc
 */
public class ResidentialComponent extends Component {
	public int Population = 0;
	public int MaxPop = 100;

	//Happiness based off comercial districts in range? - industrial zones + parks?
	public int Happiness = 50;

	//public int Employeed = 0;


	public ResidentialComponent()
	{}
}
